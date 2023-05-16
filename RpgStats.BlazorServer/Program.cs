using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;
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
builder.Services.AddControllers().AddApplicationPart(typeof(RpgStats.Controllers.AssemblyReference).Assembly);
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "RpgStats", Version = "v1"});
});
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

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
