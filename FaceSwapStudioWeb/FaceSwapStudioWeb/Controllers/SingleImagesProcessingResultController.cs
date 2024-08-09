using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FaceSwapStudioWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SingleImagesProcessingResultController : ControllerBase
{
    private ISingleImagesProcessingResultService _processingResultService;

    public SingleImagesProcessingResultController(ISingleImagesProcessingResultService processingResultService)
    {
        _processingResultService = processingResultService;
    }
    
    [HttpPost]
    public SingleImagesProcessingResultModel Create(SingleImagesProcessingResultModel model)
    {
        return _processingResultService.Create(model);
    }

    [HttpPatch]
    public SingleImagesProcessingResultModel Update(SingleImagesProcessingResultModel model)
    {
        return _processingResultService.Update(model);
    }
    
    [HttpGet("{id}")]
    public SingleImagesProcessingResultModel Get(long id)
    {
        return _processingResultService.Get(id);
    }
    
    [HttpGet]
    public IEnumerable<SingleImagesProcessingResultModel> GetAll()
    {
        return _processingResultService.Get();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        if (_processingResultService.Delete(id))
            return Ok();
        else
            return BadRequest(new { message = "Unable to delete this item" });
    }
}