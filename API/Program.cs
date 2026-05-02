using API.Middlewares;
using Application.Interfaces.Messaging;
using Application.Funcionalidades.Tarefas.Servicos;
using Application.Utils.Transacao;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.ContextosEF;
using Repository.Repositorios.Lembretes;
using Repository.Repositorios.Tarefas;
using Infra.Messaging.RabbitMQ.Publicadores;
using Infra.Messaging.RabbitMQ.Connections;
using Infra.Messaging.RabbitMQ.Channels;
using Infra.BackgroundJobs.Hangfire.Dashboard;
using Infra.Messaging.RabbitMQ.Topology;
using Application.Emails;
using Application.Interfaces.Email;
using Serilog;
using Application.Funcionalidades.Lembretes.CasosDeUso;
using Repository.Repositorios.Usuarios;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Application.Funcionalidades.Autenticacao.Servicos;
using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;
using Application.Funcionalidades.Autenticacao.CasosDeUso;
using Infra.Autenticacao;
using Application.Funcionalidades.Usuarios.Contratos.CasosDeUso;
using Application.Funcionalidades.Usuarios.CasosDeUso;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.Tarefas.CasosDeUso;
using Repository.Repositorios.ParamGerais;
using Application.Funcionalidades.Tarefas.CasosDeUso.Subtarefa;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso.Subtarefas;
using Repository.Repositorios.Auditorias;
using Infra.Notificacoes;
using Infra.BackgroundJobs.Hangfire.Jobs.Lembretes;
using Application.Funcionalidades.ParamGerais.Servicos;
using Application.Funcionalidades.ParamGerais.Contratos.CasosDeUso;
using Application.Funcionalidades.ParamGerais.CasosDeUso;
using Application.Funcionalidades.Notificacoes.Servicos;
using API.Context;
using Application.Interfaces.Context;
using API.Hubs;
using API.SignalR;
using Application.Funcionalidades.Notificacoes.Contratos.TempoReal;
using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Application.Funcionalidades.Notificacoes.CasosDeUso;
using Microsoft.AspNetCore.SignalR;
using Repository.Repositorios.Notificacoes;
using Application.Funcionalidades.Usuarios.Servicos;
using Application.Funcionalidades.UsuarioAutenticado.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Funcionalidades.Notificacoes.Eventos;
using Application.Messaging.MessageHandlers;
using Infra.Messaging.RabbitMQ.Consumidores;
using Infra.Messaging.RabbitMQ.Consumidores.Notificacoes;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Notificacoes;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Tarefas;
using API.Autenticacao;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{builder.Environment.EnvironmentName}.Local.json",
        optional: true,
        reloadOnChange: true);

ValidateRequiredConfiguration(builder.Configuration);

builder.Services.AddDbContext<ContextEF>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly("Repository")));

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new SqlServerStorageOptions
        {
            PrepareSchemaIfNecessary = true,
            QueuePollInterval = TimeSpan.FromSeconds(60)
        });
});
//API
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioContexto, UsuarioContexto>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
builder.Services.AddHostedService<RabbitInitializerHostedService>();
builder.Services.AddHostedService<RabbitConsumerHostedService>();
builder.Services.AddScoped<IGeradorClaimsUsuario, GeradorClaimsUsuario>();


//Repositorios
builder.Services.AddScoped<IRepTarefa, RepTarefa>();
builder.Services.AddScoped<IRepLembrete, RepLembrete>();
builder.Services.AddScoped<IRepUsuario, RepUsuario>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepParamGeral, RepParamGeral>();
builder.Services.AddScoped<IRepAuditoria, RepAuditoria>();
builder.Services.AddScoped<IRepNotificacao, RepNotificacao>();

//Servicos
builder.Services.AddScoped<IServicoTarefa, ServicoTarefa>();
builder.Services.AddScoped<IServicoAutenticacao, ServicoAutenticacao>();
builder.Services.AddScoped<IServicoParamGeral, ServicoParamGeral>();
builder.Services.AddScoped<IServicoUsuario, ServicoUsuario>();
builder.Services.AddScoped<IServicoNotificacao, ServicoNotificacao>();

//Mensageria
builder.Services.AddScoped<IRabbitEventPublisher, RabbitEventPublisher>();
builder.Services.AddSingleton<IRabbitConnection, RabbitConnection>();
builder.Services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();
builder.Services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
builder.Services.AddScoped<IMessageDispatcher, MessageDispatcher>();
builder.Services.AddSingleton<IMessageConsumer, NotificacaoCriadaConsumer>();
builder.Services.AddScoped<IMessageHandler<NotificacaoCriadaEvento>, NotificacaoCriadaMessageHandler>();
builder.Services.AddScoped<IRabbitTopology, GerarLembreteTopology>();
builder.Services.AddScoped<IRabbitTopology, NotificacaoCriadaTopology>();
builder.Services.AddScoped<LembreteEmailCompose>();

//Infra
builder.Services.AddScoped<IEmail,Email>();
builder.Services.AddScoped<IVerificarSenhaCasoDeUso, VerificarSenha>();
builder.Services.AddScoped<INotificacaoTempoReal, NotificacaoTempoReal>();

//Casos de uso
builder.Services.AddScoped<IAdicionarTarefaCasoDeUso, AdicionarTarefaCasoDeUso>();
builder.Services.AddScoped<IAtualizarPrioridadeTarefaCasoDeUso, AtualizarPrioridadeTarefaCasoDeUso>();
builder.Services.AddScoped<IAtualizarTarefaCasoDeUso, AtualizarTarefaCasoDeUso>();
builder.Services.AddScoped<IListarTarefasCasoDeUso, ListarTarefas>();
builder.Services.AddScoped<IRemoverTarefaCasoDeUso, RemoverTarefaCasoDeUso>();
builder.Services.AddScoped<IGerarLembreteCasoDeUso, TarefaCriadaGerarLembreteCasoDeUso>();
builder.Services.AddScoped<ILoginCasoDeUso, LoginCasoDeUso>();
builder.Services.AddScoped<IRegistrarUsuarioCasoDeUso, RegistrarUsuarioCasoDeUso>();
builder.Services.AddScoped<IHashSenhaCasoDeUso, HashSenha>();
builder.Services.AddScoped<IRecuperarTarefaPorIdCasoDeUso, RecuperarTarefaPorIdCasoDeUso>();
builder.Services.AddScoped<IAdicionarSubtarefaCasoDeUso, AdicionarSubtarefaCasoDeUso>();
builder.Services.AddScoped<IAtualizarStatusTarefaCasoDeUso, AtualizarStatusTarefaCasoDeUso>();
builder.Services.AddScoped<IRecuperarHistoricoTarefaCasoDeUso, RecuperarHistoricoTarefaCasoDeUso>();
builder.Services.AddScoped<IAgendadorJobLembrete, AgendarLembreteJobScheduler>();
builder.Services.AddScoped<IAgendarLembreteCasoDeUso, AgendarLembreteCasoDeUso>();
builder.Services.AddScoped<IDispararLembreteCasoDeUso, DispararLembreteCasoDeUso>();
builder.Services.AddScoped<IEnviarLembretePorEmailCasoDeUso, EnviarLembretePorEmailCasoDeUso>();
builder.Services.AddScoped<IListarParamGeralCasoDeUso, ListarParamGeralCasoDeUso>();
builder.Services.AddScoped<IAtualizarParamGeralCasoDeUso, AtualizarParamGeralCasoDeUso>();
builder.Services.AddScoped<ICriarNotificacaoCasoDeUso, CriarNotificacaoCasoDeUso>();
builder.Services.AddScoped<IListarNotificacoesCasoDeUso, ListarNotificacoesCasoDeUso>();
builder.Services.AddScoped<IContarNotificacoesNaoLidasCasoDeUso, ContarNotificacoesNaoLidasCasoDeUso>();
builder.Services.AddScoped<IMarcarNotificacaoComoLidaCasoDeUso, MarcarNotificacaoComoLidaCasoDeUso>();
builder.Services.AddScoped<IMarcarTodasNotificacoesComoLidasCasoDeUso, MarcarTodasNotificacoesComoLidasCasoDeUso>();
builder.Services.AddScoped<IExcluirNotificacaoCasoDeUso, ExcluirNotificacaoCasoDeUso>();
builder.Services.AddScoped<IExcluirTodasNotificacoesCasoDeUso, ExcluirTodasNotificacoesCasoDeUso>();
builder.Services.AddScoped<IAtualizarNomeUsuarioCasoDeUso, AtualizarNomeUsuarioCasoDeUso>();
builder.Services.AddScoped<IRegistrarUsuarioCasoDeUso, RegistrarUsuarioCasoDeUso>();
builder.Services.AddScoped<IObterIdUsuarioCasoDeUso, ObterIdUsuarioCasoDeUso>();
builder.Services.AddScoped<IServicoUsuarioAutenticado, ServicoUsuarioAutenticado>();
builder.Services.AddScoped<INotificarUsuarioCasoDeUso, NotificarUsuarioCasoDeUso>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

builder.Services.AddRazorPages();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 32;
    });

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "organizaai_auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;

        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;

        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            },

            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/rabbit-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
logger.LogInformation("TESTE SERILOG");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API v1");
        c.RoutePrefix = string.Empty; // Para a UI do Swagger ficar na raiz
    });

    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}
app.UseHangfireDashboard("/hangfire", 
    new DashboardOptions 
    { 
        Authorization = new[] { new HangfireAuthorizationFilter()} 
    });
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowFrontend");

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificacaoHub>("/hubs/notificacoes");
app.MapRazorPages();


app.Run();

static void ValidateRequiredConfiguration(IConfiguration configuration)
{
    var missingKeys = new List<string>();

    Require("ConnectionStrings:DefaultConnection");
    Require("RabbitMQ:Uri");
    Require("Email:Host");
    Require("Email:Port");
    Require("Email:UserName");
    Require("Email:Password");
    Require("Email:FromEmail");
    Require("Email:FromName");

    if (missingKeys.Count > 0)
    {
        var message = new StringBuilder()
            .AppendLine("Configuracao obrigatoria ausente para iniciar a API.")
            .AppendLine("Revise os arquivos appsettings.Local.json / appsettings.{Environment}.Local.json.")
            .AppendLine("Chaves ausentes:");

        foreach (var key in missingKeys)
        {
            message.AppendLine($"- {key}");
        }

        throw new InvalidOperationException(message.ToString());
    }

    void Require(string key)
    {
        if (string.IsNullOrWhiteSpace(configuration[key]))
        {
            missingKeys.Add(key);
        }
    }
}




