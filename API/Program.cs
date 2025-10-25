using Application.Services.ServToDoItems;
using Application.Services.ToDoItemServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.ContextEFs;
using Repository.Repositorys;
using Repository.ToDoItemRep;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextEF>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         x => x.MigrationsAssembly("Repository")));


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IServToDoItem, ServToDoItem>();
builder.Services.AddScoped<IRepToDoItem, RepToDoItem>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo API", Version = "v1" });
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz (opcional)
    });

    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
