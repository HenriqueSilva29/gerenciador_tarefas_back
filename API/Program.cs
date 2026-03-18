using API.Middlewares;
using Application.Interfaces.Messaging;
using Application.Services.ServSubTarefas;
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
using Infra.Emails;
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

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddHangfireServer( options => 
    { 
    options.ServerName = "API-Hangfire-Server";
    });

//Repositorios
builder.Services.AddScoped<IRepTarefa, RepTarefa>();
builder.Services.AddScoped<IRepLembrete, RepLembrete>();
builder.Services.AddScoped<IRepUsuario, RepUsuario>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Servicos
builder.Services.AddScoped<IServTarefa, ServTarefa>();
builder.Services.AddScoped<IServSubtarefa, ServSubtarefa>();
builder.Services.AddScoped<IServAutenticacao, ServAutenticacao>();

//Mensageria
builder.Services.AddScoped<IRabbitEventPublisher, RabbitEventPublisher>();
builder.Services.AddScoped<IRabbitConnection, RabbitConnection>();
builder.Services.AddScoped<IRabbitChannelFactory, RabbitChannelFactory>();
builder.Services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();
builder.Services.AddScoped<IMessageDispatcher, MessageDispatcher>();
builder.Services.AddScoped<LembreteEmailCompose>();

//Infra
builder.Services.AddScoped<IEmail,Email>();
builder.Services.AddScoped<IGerarTokenUseCase, GerarToken>();
builder.Services.AddScoped<IVerificarSenhaUseCase, VerificarSenha>();

//Casos de uso
builder.Services.AddScoped<IAdicionarTarefaUseCase, AdicionarTarefaUseCase>();
builder.Services.AddScoped<IAtualizarPrioridadeTarefaUseCase, AtualizarPrioridadeTarefaUseCase>();
builder.Services.AddScoped<IAtualizarTarefaUseCase, AtualizarTarefaUseCase>();
builder.Services.AddScoped<IListarTarefaUseCase, ListarTarefa>();
builder.Services.AddScoped<IListarTarefasVencidasUseCase, ListarTarefasVencidas>();
builder.Services.AddScoped<IRemoverTarefaUseCase, RemoverTarefaUseCase>();
builder.Services.AddScoped<ICriarLembreteUseCase, CriarLembreteUseCase>();
builder.Services.AddScoped<IVerificarLembretesPertoDoVencimentoUseCase, VerificarLembretesPertoDoVencimentoUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
builder.Services.AddScoped<IHashSenhaUseCase, HashSenha>();
builder.Services.AddScoped<IRecuperarTarefaPorIdUseCase, RecuperarTarefaPorIdUseCase>();

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

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();


app.Run();
