using FaceSwapStudioWeb.Models;

namespace FaceSwapStudioWeb.Data;

public class SingleImagesProcessingResultDataContext
{
    public List<SingleImagesProcessingResultModel> ProcessingResultModels { get; set; }

    public SingleImagesProcessingResultDataContext()
    {
        ProcessingResultModels = new List<SingleImagesProcessingResultModel>();
    }
}