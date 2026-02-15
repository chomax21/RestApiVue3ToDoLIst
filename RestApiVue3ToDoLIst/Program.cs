using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using RestApiVue3ToDoLIst.Data.AppContext;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.DTO.Requests;
using RestApiVue3ToDoLIst.Data.Models.Entities;
using RestApiVue3ToDoLIst.Middleware;
using RestApiVue3ToDoLIst.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseFirebird(connection));

builder.Services.AddScoped<IJobRepository<Job, JobRequest>, JobService>();
builder.Services.AddScoped<IUserRepository<User>, UserService>();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.EnsureCreated();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "API is running!");
app.MapGet("/health", () => new { status = "OK" });

app.Run();
