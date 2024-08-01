using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;
using Microsoft.AspNetCore.Mvc;

namespace FaceSwapStudioWeb.Models;

public class FaceSwapResultModel
{
    public FaceProcessingStatus ProcessingStatus { get; set; } = FaceProcessingStatus.Unknown;
    public FileResult? SwapImage { get; set; }
    public FileResult? EnchancedSwapImage { get; set; }
}