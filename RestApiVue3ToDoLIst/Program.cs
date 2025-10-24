using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using RestApiVue3ToDoLIst.Data.AppContext;
using RestApiVue3ToDoLIst.Data.Interfaces;
using RestApiVue3ToDoLIst.Data.Models.Entities;
using RestApiVue3ToDoLIst.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseFirebird(connection));

builder.Services.AddScoped<IJobRepository<Job>, JobService>();
builder.Services.AddScoped<IUserRepository<User>, UserService>();

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:5174", "http://localhost:5173")
.AllowAnyHeader()
.AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
