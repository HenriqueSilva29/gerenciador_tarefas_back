using API.Middlewares;
using Application.Interfaces.Messaging;
using Application.Services.ServTarefas;
using Application.Services.TarefaServices;
using Application.Utils.Transacao;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.ContextEFs;
using Repository.Repositorys.LembreteRep;
using Repository.TarefaRep;
using Infra.Mensageria.RabbitMQ.Publicadores;
using Infra.Mensageria.RabbitMQ.Connections;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Jobs.Hangfire.Dashboard;
using Infra.Mensageria.RabbitMQ.Topology;
using Application.Emails;
using Application.Interfaces.Email;
using Serilog;
using Infra.Messaging.RabbitMQ.Publicadores;
using Application.UseCase.Lembretes;
using Repository.Repositorys.UsuarioRep;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Interfaces.UseCases.Lembretes;
using Application.Services.ServAutenticacaos;
using Application.Interfaces.UseCases.Autenticacaos;
using Application.UseCase.Autenticacaos;
using Infra.Autenticacao;
using Application.Interfaces.UseCases.Usuarios;
using Application.UseCase.Usuarios;
using Application.Interfaces.UseCases.Tarefas;
using Application.UseCase.Tarefas;
using Infra.Messaging.RabbitMQ.Topology;
using Application.UseCase.ToDoItems;
using Repository.Repositorys.ParamGeralRep;
using Application.UseCase.Tarefas.Subtarefa;
using Application.Interfaces.UseCases.Tarefas.Subtarefas;
using Repository.Repositorys.AuditoriaRep;
using Infra.Notifications;
using Infra.BackgroundJobs.Hangfire.Jobs.Lembretes;
using Application.Services.ServParamGerals;
using Application.Interfaces.UseCases.ParamGerals;
using Application.UseCase.ParamGerals;
using Application.Services.ServNotificacoes;
using API.Context;
using Application.Interfaces.Context;
using API.Hubs;
using API.SignalR;
using Application.Interfaces.Notificacoes;
using Application.Interfaces.UseCases.Notificacoes;
using Application.UseCase.Notificacoes;
using Microsoft.AspNetCore.SignalR;
using Repository.Repositorys.NotificacaoRep;
using Application.Services.ServUsuarios;
using Application.Interfaces.UseCases.UsuarioAutenticados;
using Application.UseCase.UsuarioAutenticados;
using Application.Services.ServUsuarioAutenticados;
using Application.Services.UsuarioAutenticados;
using Application.Events.Notificacoes;
using Application.Messaging.MessageHandlers;
using Infra.Messaging.RabbitMQ.Consumidores;
using Infra.Messaging.RabbitMQ.Consumidores.Notificacoes;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Notificacoes;
using Infra.Messaging.RabbitMQ.Topology.Topologies.Tarefas;

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

//Repositorios
builder.Services.AddScoped<IRepTarefa, RepTarefa>();
builder.Services.AddScoped<IRepLembrete, RepLembrete>();
builder.Services.AddScoped<IRepUsuario, RepUsuario>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepParamGeral, RepParamGeral>();
builder.Services.AddScoped<IRepAuditoria, RepAuditoria>();
builder.Services.AddScoped<IRepNotificacao, RepNotificacao>();

//Servicos
builder.Services.AddScoped<IServTarefa, ServTarefa>();
builder.Services.AddScoped<IServAutenticacao, ServAutenticacao>();
builder.Services.AddScoped<IServParamGeral, ServParamGeral>();
builder.Services.AddScoped<IServUsuario, ServUsuario>();
builder.Services.AddScoped<IServNotificacao, ServNotificacao>();

//Mensageria
builder.Services.AddScoped<IRabbitEventPublisher, RabbitEventPublisher>();
builder.Services.AddSingleton<IRabbitConnection, RabbitConnection>();
builder.Services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();
builder.Services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
builder.Services.AddScoped<IMessageDispatcher, MessageDispatcher>();
builder.Services.AddSingleton<IMessageConsumer, NotificacaoCriadaConsumer>();
builder.Services.AddScoped<IMessageHandler<NotificacaoCriadaEvent>, NotificacaoCriadaMessageHandler>();
builder.Services.AddScoped<IRabbitTopology, GerarLembreteTopology>();
builder.Services.AddScoped<IRabbitTopology, NotificacaoCriadaTopology>();
builder.Services.AddScoped<LembreteEmailCompose>();

//Infra
builder.Services.AddScoped<IEmail,Email>();
builder.Services.AddScoped<IGerarTokenUseCase, GerarToken>();
builder.Services.AddScoped<IVerificarSenhaUseCase, VerificarSenha>();
builder.Services.AddScoped<INotificacaoTempoReal, NotificacaoTempoReal>();

//Casos de uso
builder.Services.AddScoped<IAdicionarTarefaUseCase, AdicionarTarefaUseCase>();
builder.Services.AddScoped<IAtualizarPrioridadeTarefaUseCase, AtualizarPrioridadeTarefaUseCase>();
builder.Services.AddScoped<IAtualizarTarefaUseCase, AtualizarTarefaUseCase>();
builder.Services.AddScoped<IListarTarefasUseCase, ListarTarefas>();
builder.Services.AddScoped<IRemoverTarefaUseCase, RemoverTarefaUseCase>();
builder.Services.AddScoped<IGerarLembreteUseCase, TarefaCriadaGerarLembreteUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
builder.Services.AddScoped<IHashSenhaUseCase, HashSenha>();
builder.Services.AddScoped<IRecuperarTarefaPorIdUseCase, RecuperarTarefaPorIdUseCase>();
builder.Services.AddScoped<IAdicionarSubtarefaUseCase, AdicionarSubtarefaUseCase>();
builder.Services.AddScoped<IAtualizarStatusTarefaUseCase, AtualizarStatusTarefaUseCase>();
builder.Services.AddScoped<IRecuperarHistoricoTarefaUseCase, RecuperarHistoricoTarefaUseCase>();
builder.Services.AddScoped<IAgendarLembreteJobScheduler, AgendarLembreteJobScheduler>();
builder.Services.AddScoped<IAgendarLembreteUseCase, AgendarLembreteUseCase>();
builder.Services.AddScoped<IDispararLembreteUseCase, DispararLembreteUseCase>();
builder.Services.AddScoped<IEnviarLembretePorEmailUseCase, EnviarLembretePorEmailUseCase>();
builder.Services.AddScoped<IListarParamGeralUseCase, ListarParamGeralUseCase>();
builder.Services.AddScoped<IAtualizarParamGeralUseCase, AtualizarParamGeralUseCase>();
builder.Services.AddScoped<ICriarNotificacaoUseCase, CriarNotificacaoUseCase>();
builder.Services.AddScoped<IListarNotificacoesUseCase, ListarNotificacoesUseCase>();
builder.Services.AddScoped<IContarNotificacoesNaoLidasUseCase, ContarNotificacoesNaoLidasUseCase>();
builder.Services.AddScoped<IMarcarNotificacaoComoLidaUseCase, MarcarNotificacaoComoLidaUseCase>();
builder.Services.AddScoped<IMarcarTodasNotificacoesComoLidasUseCase, MarcarTodasNotificacoesComoLidasUseCase>();
builder.Services.AddScoped<IExcluirNotificacaoUseCase, ExcluirNotificacaoUseCase>();
builder.Services.AddScoped<IExcluirTodasNotificacoesUseCase, ExcluirTodasNotificacoesUseCase>();
builder.Services.AddScoped<IAtualizarNomeUsuarioUseCase, AtualizarNomeUsuarioUseCase>();
builder.Services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
builder.Services.AddScoped<IObterIdUsuarioUseCase, ObterIdUsuarioUseCase>();
builder.Services.AddScoped<IServUsuarioAutenticado, ServUsuarioAutenticado>();
builder.Services.AddScoped<INotificarUsuarioUseCase, NotificarUsuarioUseCase>();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/hubs/notificacoes"))
                {
                    context.Token = accessToken;
                }

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
    Require("Jwt:Key");
    Require("Jwt:Issuer");
    Require("Jwt:Audience");
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
