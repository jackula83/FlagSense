using FlagService.Api.Entity.Flags;
using FlagService.Api.Options;
using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Flags;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Domain.Aggregates.Users;
using FlagService.Infra.Data.Abstracts;
using FlagService.Infra.Data.Repositories;
using Framework2.Infra.MQ.Core;
using Framework2.Infra.MQ.RabbitMQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

//Debugger.Break();

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();

// Configure database context
var databaseSection = builder.Configuration.GetSection(DatabaseOptions.OptionName);
var connectionString = $@"
    Server={databaseSection[nameof(DatabaseOptions.Server)]},{databaseSection[nameof(DatabaseOptions.Port)]};
    Database={databaseSection[nameof(DatabaseOptions.Name)]};
    User Id={databaseSection[nameof(DatabaseOptions.Username)]};
    Password={databaseSection[nameof(DatabaseOptions.Password)]};";
services.AddDbContext<FsSqlServerContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

// Configure DI
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddLogging();
services.AddSingleton<IEventQueue, RabbitQueue>();
services.AddTransient<FlagEntityQueryHandler>();
services.AddTransient<IFlagRepository, FlagRepository>();

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FsSqlServerContext>();

    dbContext.Set<Env>();
    dbContext.Set<Segment>();
    dbContext.Set<Flag>();
    dbContext.Set<Rule>();
    dbContext.Set<RuleGroup>();
    dbContext.Set<User>();
    dbContext.Set<UserProperty>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
