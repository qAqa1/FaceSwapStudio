using FaceSwapStudioWeb.Models;

namespace FaceSwapStudioWeb.Data;

public class SingleImagesProcessingResultDataContext
{
    public List<SingleImagesProcessingResultModel> ProcessingResultModels { get; set; }
    
    public SingleImagesProcessingResultDataContext()
    {
        ProcessingResultModels = new List<SingleImagesProcessingResultModel>();
        
        for (int i = 0; i < 6; i++)
        {
            ProcessingResultModels.Add(new SingleImagesProcessingResultModel
            {
                Id = i,
                StartCalculationDateTime = DateTime.UtcNow,
                EndCalculationDateTime = DateTime.UtcNow,
                BodyImage = "BodyImage " + i,
                FaceImage = "FaceImage " + i,
                SwapImage = "SwapImage " + i,
                EnchancedSwapImage = "EnchancedSwapImage " + i,
            });
        }
    }
}