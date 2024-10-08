using BloodConnect.Application;
using BloodConnect.Application.Commands.CreateDonor;
using BloodConnect.Domain.Repositories;
using BloodConnect.Domain.UnitOfWork;
using BloodConnect.Infrastructure;
using BloodConnect.Infrastructure.Persistence;
using BloodConnect.Infrastructure.Persistence.Repositories;
using BloodConnect.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services
    .AddAplication()
    .AddInfrastructure();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<BloodConnectContext>(x => x.UseInMemoryDatabase("BloodConnect"));
var connectionString = builder.Configuration.GetConnectionString("DbContextCs");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<BloodConnectContext>(options => options.UseMySql(connectionString, serverVersion));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
