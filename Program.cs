using EmployeeControl;
using EmployeeControl.Core.Bussiness;
using EmployeeControl.Core.Interfaces;
using EmployeeControl.Repositorio;
using EmployeeControl.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
      opciones.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeesBusiness, EmployeesBusiness>();
// builder.Services.AddScoped<ITimeEntrancesBusiness, TimeEntrancesBusiness>();      add 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
