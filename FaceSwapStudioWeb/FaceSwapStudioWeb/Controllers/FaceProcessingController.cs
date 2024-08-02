using FaceSwapApiCaller.FaceProcessingApi;
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
    
    // http://localhost:5018/api/FaceProcessing
    [HttpGet]
    public async Task<ActionResult<string>> Test()
    {
        return "gggeeet";
    }
    
    // http://localhost:5018/api/FaceProcessing
    [HttpPost]
    public async Task<ActionResult<string>> Test3()
    {
        return "pposttt";
    }
    
    // http://localhost:5018/api/FaceProcessing/Test2
    [HttpGet]
    [Route("Test2")]
    public async Task<ActionResult<string>> Test2()
    {
        return "ttestt";
    }
}