using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;
using Microsoft.AspNetCore.Mvc;

namespace FaceSwapStudioWeb.Models;

public class FaceSwapResultModel
{
    public FaceProcessingStatus ProcessingStatus { get; set; } = FaceProcessingStatus.UnknownError;
    public string? SwapImage { get; set; }
    public string? EnchancedSwapImage { get; set; }
}