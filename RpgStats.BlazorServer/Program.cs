using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// read appsettings for "api" section
builder.Configuration.GetSection("Api").Bind(builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddHttpClient("RpgStatsApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"] ?? string.Empty);
});

var app = builder.Build();

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
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints => endpoints.MapControllers());
#pragma warning restore ASP0014

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();