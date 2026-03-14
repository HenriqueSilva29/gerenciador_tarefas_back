using Domain.Common.ValueObjects;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.ConfigEF;
using System.Text.Json;

namespace Repository.ContextEFs
{
    public class ContextEF : DbContext
    {
        public ContextEF(DbContextOptions<ContextEF> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TarefaConfig());
            modelBuilder.ApplyConfiguration(new LembreteConfig());
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new AuditoriaConfig());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextEF).Assembly); -- Esse codigo aplica todas as config acimas de uma só vez.
        }

        public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
        {
            try
            {
                var auditoriasTemp = PrepararAuditoria();

                var result = await base.SaveChangesAsync(cancellationToken);

                if (auditoriasTemp.Any())
                {
                    foreach (var temp in auditoriasTemp)
                    {
                        var pk = temp.Entry
                            .Properties
                            .First(p => p.Metadata.IsPrimaryKey());

                        temp.Auditoria.IdEntidade = (int)pk.CurrentValue;
                    }

                    Set<Auditoria>().AddRange(auditoriasTemp.Select(a => a.Auditoria));

                    await base.SaveChangesAsync(cancellationToken);
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

        }

        private List<AuditoriaTemp> PrepararAuditoria()
        {
            ChangeTracker.DetectChanges();

            var auditorias = new List<AuditoriaTemp>();

            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                  e.Entity is not Auditoria &&
                  (e.State == EntityState.Modified ||
                   e.State == EntityState.Added ||
                   e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var auditoria = new Auditoria
                {
                    Entidade = entry.Entity.GetType().Name,
                    Acao = entry.State.ToString(),
                    IdUsuario = "TEMP", // depois pegamos do contexto
                    Data = UtcDateTime.Now(),
                    Alteracoes = ObterAlteracoes(entry)
                };

                auditorias.Add(new AuditoriaTemp
                {
                    Auditoria = auditoria,
                    Entry = entry
                });
            }

            return auditorias;
        }

        private string ObterAlteracoes(EntityEntry entry)
        {
            var alteracoes = new Dictionary<string, object?>();

            foreach (var property in entry.Properties)
            {
                var antes = property.OriginalValue;
                var depois = property.CurrentValue;

                if (entry.State == EntityState.Added)
                {
                    alteracoes[property.Metadata.Name] = depois;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    alteracoes[property.Metadata.Name] = antes;
                }
                else if (property.IsModified && !Equals(antes, depois))
                {
                    alteracoes[property.Metadata.Name] = new
                    {
                        Antes = antes,
                        Depois = depois
                    };
                }
            }

            return JsonSerializer.Serialize(alteracoes);
        }
    }
}
