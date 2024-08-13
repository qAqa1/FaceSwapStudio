using System.Diagnostics;
using FaceSwapApiCaller.FaceProcessingApi;
using FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;
using Microsoft.AspNetCore.Mvc;
using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services;

namespace FaceSwapStudioWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly FaceProcessingService _faceProcessingService;

    public HomeController(ILogger<HomeController> logger, FaceProcessingService faceProcessingService)
    {
        _logger = logger;
        _faceProcessingService = faceProcessingService;
    }

    public IActionResult Index()
    {
        return View("SingleImagesProcessing");
        // return View();
    }
    
    public IActionResult SingleImagesProcessingResult()
    {
        return View("SingleImagesProcessingResult");
        // return View();
    }

    public IActionResult License()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}