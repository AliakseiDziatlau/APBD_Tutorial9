using System.Data;
using APBD_Tutorial9.Domain.Interfaces;
using APBD_Tutorial9.Infrastructure.Repositories;
using APBD_Tutorial9.Infrastructure.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection"));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<ProductToWarehouseValidator>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

