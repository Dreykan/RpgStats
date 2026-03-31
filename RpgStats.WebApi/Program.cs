using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RpgStats.Repo;
using RpgStats.Services;
using RpgStats.Services.Abstractions;
using RpgStats.WebApi;
using FluentValidation;
using RpgStats.BizLogic.Validations.StatValue;
using RpgStats.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RpgStats", Version = "v1" });
});
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameStatService, GameStatService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IPlatformGameService, PlatformGameService>();
builder.Services.AddScoped<IStatService, StatService>();
builder.Services.AddScoped<IStatValueService, StatValueService>();

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<StatValueForCreationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<StatValueForCreationListValidator>();

builder.Services.AddDbContextPool<RpgStatsContext>(options =>
{
    var connectionString = builder.Environment.IsDevelopment() 
    ? GetConnectionStringFromJson()
    : builder.Configuration.GetConnectionString("RpgStatsPostgresql");

    if (connectionString != null) options.UseNpgsql(connectionString);
});

var app = builder.Build();

await ApplyMigration(app.Services);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
return;

static string? GetConnectionStringFromJson()
{
    var connectionString = string.Empty;

    var streamReader = new StreamReader("dbConnection.json");
    var jsonValues = streamReader.ReadToEnd();
    var dbConnectionJson = JsonConvert.DeserializeObject<DbConnectionJson>(jsonValues);
    if (dbConnectionJson?.ConnectionStrings != null)
        connectionString = dbConnectionJson.ConnectionStrings.RpgStatsPostgresql;

    return connectionString;
}

static async Task ApplyMigration(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    await using var dbContext = scope.ServiceProvider.GetRequiredService<RpgStatsContext>();

    await dbContext.Database.MigrateAsync();
}