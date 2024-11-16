using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RpgStats.Repo;
using RpgStats.Services;
using RpgStats.Services.Abstractions;
using RpgStats.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RpgStats", Version = "v1" });
});
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IGameStatService, GameStatService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IPlatformGameService, PlatformGameService>();
builder.Services.AddTransient<IStatService, StatService>();
builder.Services.AddTransient<IStatValueService, StatValueService>();
builder.Services.AddDbContextPool<RpgStatsContext>(options =>
{
    var connectionString = GetConnectionString();

    if (connectionString != null) options.UseNpgsql(connectionString);
});

var app = builder.Build();

await ApplyMigration(app.Services);

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
return;

static string? GetConnectionString()
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