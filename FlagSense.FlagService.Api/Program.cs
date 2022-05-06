using FlagSense.FlagService.Api.Options;
using FlagSense.FlagService.Domain.Data;
using FlagSense.FlagService.Domain.Entities;
using FlagService.Domain.Aggregates.Environment;
using FlagService.Domain.Aggregates.Flag;
using FlagService.Domain.Aggregates.RuleGroup;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

//Debugger.Break();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
var databaseSection = builder.Configuration.GetSection(DatabaseOptions.OptionName);

// Configure database context
var connectionString = $@"
    Server={databaseSection[nameof(DatabaseOptions.Server)]},{databaseSection[nameof(DatabaseOptions.Port)]};
    Database={databaseSection[nameof(DatabaseOptions.Name)]};
    User Id={databaseSection[nameof(DatabaseOptions.Username)]};
    Password={databaseSection[nameof(DatabaseOptions.Password)]};";
builder.Services.AddDbContext<FsSqlServerContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
