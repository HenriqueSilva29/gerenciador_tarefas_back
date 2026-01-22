using API.Middlewares;
using Application.Interfaces;
using Application.Services.ServLembretes;
using Application.Services.ServSubTarefas;
using Application.Services.ServToDoItems;
using Application.Services.ToDoItemServices;
using Application.Utils.Transacao;
using Hangfire;
using Hangfire.SqlServer;
using Infra.Jobs.Hangfire.Dashboard;
using Infra.Jobs.Hangfire.JobDeAgendamentos;
using Infra.Jobs.Hangfire.JobDeLembretes;
using Infra.Mensageria.RabbitMQ;
using Infra.Mensageria.RabbitMQ.Publicadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.ContextEFs;
using Repository.Repositorys;
using Repository.Repositorys.LembreteRep;
using Repository.ToDoItemRep;

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
builder.Services.AddHangfireServer();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepToDoItem, RepToDoItem>();
builder.Services.AddScoped<IRepLembrete, RepLembrete>();

builder.Services.AddScoped<IServToDoItem, ServToDoItem>();
builder.Services.AddScoped<IServSubtarefa, ServSubtarefa>();
builder.Services.AddScoped<IServLembrete, ServLembrete>();

builder.Services.AddScoped<IPublicadorDeMensagens, PublicadorDeMensagens>();
builder.Services.AddScoped<IJobDeLembrete, JobDeLembrete>();
builder.Services.AddScoped<IJobScheduler, JobScheduler>();

builder.Services.AddScoped<IRabbitConnection, RabbitConnection>();
builder.Services.AddScoped<IRabbitChannelFactory, RabbitChannelFactory>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 32;
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
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
