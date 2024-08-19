using FaceSwapApiCaller.FaceProcessingApi;
using FaceSwapApiCaller.FaceProcessingApi.Configuration;
using FaceSwapStudioWeb.Data;
using FaceSwapStudioWeb.Services;
using FaceSwapStudioWeb.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var customSettingsDir = "CustomSettings" + Path.DirectorySeparatorChar;

var externalApiConfig = new ConfigurationBuilder().AddJsonFile(customSettingsDir + "ExternalApiSettings.json").Build();
var faceSwapApiUrl = externalApiConfig.GetValue<string>("ApiUrl:FaceSwapApi");
var stableDiffusionApiUrl = externalApiConfig.GetValue<string>("ApiUrl:StableDiffusionApiUrl");
FaceProcessingApiConfiguration externalApiConfiguration = new(faceSwapApiUrl, stableDiffusionApiUrl);

builder.Services.AddSingleton(new FaceProcessingService(externalApiConfiguration));

var databaseConfig = new ConfigurationBuilder().AddJsonFile(customSettingsDir + "DatabaseSettings.json").Build();
var useMoq = databaseConfig.GetValue<bool>("UseMoq");


var connectionType = databaseConfig.GetValue<string>("ConnectionType") ??
                     throw new InvalidOperationException("Connection type not found");

var connectionString = databaseConfig.GetValue<string>($"ConnectionStrings:{connectionType}Connection") ??
                       throw new InvalidOperationException("Connection string not found");

if (useMoq)
{
    builder.Services.AddTransient<ISingleImagesProcessingResultService, SingleImagesProcessingResultMoqService>();
    builder.Services.AddSingleton<SingleImagesProcessingResultMoqContext>();
}
else
{
    if (connectionType == "SqlLite")
    {
        builder.Services.AddDbContext<SingleImagesProcessingResultDbContext>(options =>
            options.UseSqlite(connectionString));
    }
    else if (connectionType == "SqlServer")
    {
        builder.Services.AddDbContext<SingleImagesProcessingResultDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
    else
        throw new InvalidOperationException("Unexpected connection type");
    
    builder.Services.AddTransient<ISingleImagesProcessingResultService, SingleImagesProcessingResultDbService>();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();