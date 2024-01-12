using Dapper;
using Microsoft.Data.SqlClient;
using Web.API.Endpoints;
using Web.API.Models;
using Web.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
    throw new ApplicationException("The connection string is null");

    return new SqlConnectionFactory(connectionString);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCustomerEndpoints();

app.Run();
