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
    private readonly CustomConvert _customConvert;

    private readonly FaceSwapModel _faceSwapModel;

    public HomeController(ILogger<HomeController> logger, FaceProcessingService faceProcessingService,
        CustomConvert customConvert)
    {
        _logger = logger;
        _faceProcessingService = faceProcessingService;
        _customConvert = customConvert;

        _faceSwapModel = new FaceSwapModel(_customConvert);
    }

    public IActionResult Index()
    {
        return View("SingleImagesProcessing", _faceSwapModel);
        // return View();
    }

    public IActionResult License()
    {
        return View();
    }

    public IActionResult SingleImagesProcessing()
    {
        return View(_faceSwapModel);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImagesForSwap(IFormFile body, IFormFile face)
    {
        // var bodyInput = (body != null) ? body : _faceSwapModel.BodyImage;
        // var faceInput = (face != null) ? body : _faceSwapModel.FaceImage;
        
        if (body != null && face != null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _faceSwapModel.BodyImage = body;
            _faceSwapModel.FaceImage = face;

            var base64BodyImage =
                _customConvert.FileToBase64(_faceSwapModel.BodyImage);

            var base64FaceImage =
                _customConvert.FileToBase64(_faceSwapModel.FaceImage);

            var targetFaceIndex = 0;

            var swapFaceRequest = new SwapFaceRequest(base64BodyImage, base64FaceImage, targetFaceIndex);
            var swapFaceResult = await _faceProcessingService.SwapFaceApi.SwapFace(swapFaceRequest);

            if (swapFaceResult is { Status: FaceProcessingStatus.Success })
            {
                _faceSwapModel.SwapImage = _customConvert.Base64ToFile(swapFaceResult.Image);

                var enchanceFacesRequest = new EnchanceFacesRequest(1, swapFaceResult.Image);
                var enchanceFacesResult = await _faceProcessingService.EnchanceFaceApi.EnchanceFaces(enchanceFacesRequest);

                if (enchanceFacesResult != null && enchanceFacesResult.Image != null)
                    _faceSwapModel.EnchancedSwapImage = _customConvert.Base64ToFile(enchanceFacesResult.Image);
            }
        }

        return View("SingleImagesProcessing", _faceSwapModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}