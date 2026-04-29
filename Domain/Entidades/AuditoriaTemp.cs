using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Entidades
{
    public class AuditoriaTemp
    {
        public Auditoria Auditoria { get; set; }
        public EntityEntry Entry { get; set; }
    }
}

