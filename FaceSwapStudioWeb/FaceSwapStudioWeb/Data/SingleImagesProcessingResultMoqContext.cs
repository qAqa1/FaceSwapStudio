using FaceSwapStudioWeb.Models;

namespace FaceSwapStudioWeb.Data;

public class SingleImagesProcessingResultMoqContext
{
    public List<SingleImagesProcessingResultModel> ProcessingResultModels { get; set; }
    
    public SingleImagesProcessingResultMoqContext()
    {
        ProcessingResultModels = new List<SingleImagesProcessingResultModel>();
    }
}