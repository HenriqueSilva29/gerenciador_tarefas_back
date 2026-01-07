using API.Middlewares;
using Application.Services.ServSubTarefas;
using Application.Services.ServToDoItems;
using Application.Services.ToDoItemServices;
using Hangfire;
using Infra.Jobs.Hangfire.JobDeLembretes;
using Infra.Mensageria.RabbitMQ.Publicadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.ContextEFs;
using Repository.Repositorys;
using Repository.Repositorys.LembreteRep;
using Repository.ToDoItemRep;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Ambiente atual: " + builder.Environment.EnvironmentName);
Console.WriteLine("Connection string usada: " + builder.Configuration.GetConnectionString("DefaultConnection"));


builder.Services.AddDbContext<ContextEF>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly("Repository")));

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepToDoItem, RepToDoItem>();
builder.Services.AddScoped<IRepLembrete, RepLembrete>();

builder.Services.AddScoped<IServToDoItem, ServToDoItem>();
builder.Services.AddScoped<IServSubtarefa, ServSubtarefa>();

builder.Services.AddScoped<IPublicadorDeMensagens, PublicadorDeMensagens>();

builder.Services.AddScoped<IJobDeLembrete, JobDeLembrete>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
