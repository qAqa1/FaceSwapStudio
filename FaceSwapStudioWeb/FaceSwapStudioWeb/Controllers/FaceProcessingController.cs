using FaceSwapApiCaller.FaceProcessingApi;
using FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;
using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace FaceSwapStudioWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FaceProcessingController : ControllerBase
{
    private readonly FaceProcessingService _faceProcessingService;
    private readonly CustomConvert _customConvert;
    
    public FaceProcessingController(FaceProcessingService faceProcessingService, CustomConvert customConvert)
    {
        _faceProcessingService = faceProcessingService;
        _customConvert = customConvert;
    }
    
    // // http://localhost:5018/api/FaceProcessing
    // [HttpGet]
    // public async Task<ActionResult<string>> Test()
    // {
    //     return "gggeeet";
    // }
    //
    // // http://localhost:5018/api/FaceProcessing
    // [HttpPost]
    // public async Task<ActionResult<string>> Test3()
    // {
    //     return "pposttt";
    // }
    //
    // // http://localhost:5018/api/FaceProcessing/Test2
    // [HttpGet]
    // [Route("Test2")]
    // public async Task<ActionResult<string>> Test2()
    // {
    //     return "ttestt";
    // }
    
    // http://localhost:5018/api/FaceProcessing/SwapFace
    [HttpPost]
    [Route("SwapFace")]
    public async Task<ActionResult<FaceSwapResultModel>> SwapFace(FaceSwapRequestModel request)
    {
        if (ModelState.IsValid && request.BodyImage != null && request.FaceImage != null)
        {
            var base64BodyImage =
                _customConvert.FileToBase64(request.BodyImage);
    
            var base64FaceImage =
                _customConvert.FileToBase64(request.FaceImage);
    
            var targetFaceIndex = request.BodyImageFaceIndex;
    
            var swapFaceRequest = new SwapFaceRequest(base64BodyImage, base64FaceImage, targetFaceIndex);
            var swapFaceResult = await _faceProcessingService.SwapFaceApi.SwapFace(swapFaceRequest);
            
            var resultModel = new FaceSwapResultModel();
            resultModel.ProcessingStatus = (swapFaceResult != null && swapFaceResult.Status.HasValue) ? swapFaceResult.Status.Value : FaceProcessingStatus.Unknown;
            
            if (swapFaceResult is { Status: FaceProcessingStatus.Success })
            {
                // HttpContext.Response.ContentType = "application/json";
                // FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes("YOUR PATH TO PDF"), "application/pdf")
                // {
                //     FileDownloadName = "test.pdf"
                // };
                
                // var f1 = _customConvert.Base64ToFile(swapFaceResult.Image);
                // var f2 = request.FaceImage;
                
                resultModel.SwapImage =  new FileContentResult(Convert.FromBase64String(swapFaceResult.Image), "application/json");
            
            var enchanceFacesRequest = new EnchanceFacesRequest(1, swapFaceResult.Image);
            var enchanceFacesResult = await _faceProcessingService.EnchanceFaceApi.EnchanceFaces(enchanceFacesRequest);
            
            // if (enchanceFacesResult is { Image: not null }) resultModel.EnchancedSwapImage = _customConvert.Base64ToFile(enchanceFacesResult.Image);
            if (enchanceFacesResult is { Image: not null }) resultModel.EnchancedSwapImage = new FileContentResult(Convert.FromBase64String(enchanceFacesResult.Image), "application/json");

                return resultModel;
            }
        }
    
        return new FaceSwapResultModel();
    }
    
    // [HttpPost]
    // [Route("SwapFace2")]
    // public async Task<ActionResult<FaceSwapRequest2Model>> SwapFace2(FaceSwapRequest2Model request)
    // {
    //     
    //     // var str = request.Content.ReadAsStringAsync().Result;
    //     
    //     var result = new FaceSwapRequest2Model();
    //     
    //     result.BodyImage = request.BodyImage * 2;
    //     result.FaceImage = request.FaceImage * 2;
    //     result.BodyImageFaceIndex = request.BodyImageFaceIndex * 2;
    //     
    //     return result;
    // }
}
