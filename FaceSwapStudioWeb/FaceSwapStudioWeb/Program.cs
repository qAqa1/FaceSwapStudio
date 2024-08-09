using FaceSwapApiCaller.FaceProcessingApi;
using FaceSwapApiCaller.FaceProcessingApi.Configuration;
using FaceSwapStudioWeb.Data;
using FaceSwapStudioWeb.Services;
using FaceSwapStudioWeb.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = new ConfigurationBuilder().AddJsonFile("ExternalApiSettings.json").Build();
var faceSwapApiUrl = config.GetValue<string>("ApiUrl:FaceSwapApi");
var stableDiffusionApiUrl = config.GetValue<string>("ApiUrl:StableDiffusionApiUrl");
FaceProcessingApiConfiguration externalApiConfiguration = new(faceSwapApiUrl, stableDiffusionApiUrl);

builder.Services.AddSingleton(new FaceProcessingService(externalApiConfiguration));
builder.Services.AddTransient<ISingleImagesProcessingResultService, SingleImagesProcessingResultService>();
builder.Services.AddSingleton<SingleImagesProcessingResultDataContext>();

var app = builder.Build();

// var srv2 = app.Services.GetService<FaceProcessing>();

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