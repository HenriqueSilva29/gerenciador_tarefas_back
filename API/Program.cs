using API.Middlewares;
using Application.Interfaces.Messaging;
using Application.Services.ServSubTarefas;
using Application.Services.ServToDoItems;
using Application.Services.ToDoItemServices;
using Application.Utils.Transacao;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.ContextEFs;
using Repository.Repositorys;
using Repository.Repositorys.LembreteRep;
using Repository.ToDoItemRep;
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
using Application.Interfaces.UseCases;
using Application.UseCase.Lembretes;

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

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepToDoItem, RepToDoItem>();
builder.Services.AddScoped<IRepLembrete, RepLembrete>();

builder.Services.AddScoped<IServToDoItem, ServToDoItem>();
builder.Services.AddScoped<IServSubtarefa, ServSubtarefa>();

builder.Services.AddScoped<IRabbitEventPublisher, RabbitEventPublisher>();

builder.Services.AddScoped<IRabbitConnection, RabbitConnection>();
builder.Services.AddScoped<IRabbitChannelFactory, RabbitChannelFactory>();
builder.Services.AddScoped<IRabbitTopologyInitializer, RabbitTopologyInitializer>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<LembreteEmailCompose>();
builder.Services.AddScoped<IEmail,Email>();

builder.Services.AddScoped<IMessageDispatcher, MessageDispatcher>();

builder.Services.AddScoped<ICriarLembreteUseCase, CriarLembreteUseCase>();
builder.Services.AddScoped<IVerificarLembretesVencendoUseCase, VerificarLembretesVencendoUseCase>();

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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/rabbit-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

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
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();


app.Run();
