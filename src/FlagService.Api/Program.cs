using FlagService.Api.Entity.Identity;
using FlagService.Api.Options;
using FlagService.Domain.Aggregates.Flags;
using FlagService.Domain.Auditing;
using FlagService.Domain.Contexts;
using FlagService.Infra.Data.Repositories;
using Framework2.Domain.Core.Identity;
using Framework2.Infra.MQ.Core;
using Framework2.Infra.MQ.DaprMQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
var databaseSection = (DatabaseOptions)builder.Configuration!;
var rabbitSection = (RabbitOptions)builder.Configuration!;

#if DEBUG
Console.WriteLine($"RabbitMQ ({rabbitSection.HostName}) initialised for: {rabbitSection.UserName}/{rabbitSection.Password}");
Console.WriteLine($"Database ({databaseSection.Server},{databaseSection.Port}, {databaseSection.Name}) initialised");
#endif

// Configure database context
services.AddDbContext<FsSqlServerContext>(
    options => options.UseSqlServer(databaseSection.ConnectionString), 
    ServiceLifetime.Scoped);

// Configure DI
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddLogging();
services.AddSingleton<IPublishQueue, DaprQueue>();
services.AddTransient<AuditOperations>();
services.AddTransient<IUserIdentity, UserIdentity>();
services.AddTransient<IFlagRepository, FlagRepository>();

services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
