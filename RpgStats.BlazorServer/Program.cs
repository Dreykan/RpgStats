using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RpgStats.BlazorServer;
using RpgStats.BlazorServer.Data;
using RpgStats.Repo;
using RpgStats.Services;
using RpgStats.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IPlatformGameService, PlatformGameService>();
builder.Services.AddScoped<IStatService, StatService>();
builder.Services.AddScoped<IStatValueService, StatValueService>();
builder.Services.AddDbContextPool<RpgStatsContext>(options =>
{
    var connectionString = GetConnectionString();

    if (connectionString != null) options.UseNpgsql(connectionString);
});

var app = builder.Build();

await ApplyMigration(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


static string? GetConnectionString()
{
    var connectionString = string.Empty;

    var streamReader = new StreamReader("dbConnection.json");
    var jsonValues = streamReader.ReadToEnd();
    var dbConnectionJson = JsonConvert.DeserializeObject<DbConnectionJson>(jsonValues);
    if (dbConnectionJson?.ConnectionStrings != null)
    {
        connectionString = dbConnectionJson.ConnectionStrings.RpgStatsPostgresql;
    }

    return connectionString;
}

static async Task ApplyMigration(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    await using RpgStatsContext dbContext = scope.ServiceProvider.GetRequiredService<RpgStatsContext>();

    await dbContext.Database.MigrateAsync();
}
