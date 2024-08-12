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

    public FaceProcessingController(FaceProcessingService faceProcessingService)
    {
        _faceProcessingService = faceProcessingService;
    }
    
    // http://localhost:5018/api/FaceProcessing/SwapFace
    [HttpPost]
    [Route("SwapFace")]
    public async Task<ActionResult<FaceSwapResultModel>> SwapFace(FaceSwapRequestModel request)
    {
        if (ModelState.IsValid && request.BodyImage != null && request.FaceImage != null)
        {
            var targetFaceIndex = request.BodyImageFaceIndex;

            var resultModel = new FaceSwapResultModel { StartCalculationDateTime = DateTime.Now };
            
            var swapFaceRequest = new SwapFaceRequest(request.BodyImage, request.FaceImage, targetFaceIndex);
            var swapFaceResult = await _faceProcessingService.SwapFaceApi.SwapFace(swapFaceRequest);

            resultModel.ProcessingStatus = (swapFaceResult != null && swapFaceResult.Status.HasValue)
                ? swapFaceResult.Status.Value
                : FaceProcessingStatus.UnknownError;

            if (swapFaceResult is { Status: FaceProcessingStatus.Success })
            {
                resultModel.SwapImage = swapFaceResult.Image;

                var enchanceFacesRequest = new EnchanceFacesRequest(1, swapFaceResult.Image);
                var enchanceFacesResult =
                    await _faceProcessingService.EnchanceFaceApi.EnchanceFaces(enchanceFacesRequest);

                if (enchanceFacesResult is { Image: not null })
                    resultModel.EnchancedSwapImage = enchanceFacesResult.Image;

                resultModel.EndCalculationDateTime = DateTime.Now;
                return resultModel;
            }
        }

        return new FaceSwapResultModel { EndCalculationDateTime = DateTime.Now };
    }
}