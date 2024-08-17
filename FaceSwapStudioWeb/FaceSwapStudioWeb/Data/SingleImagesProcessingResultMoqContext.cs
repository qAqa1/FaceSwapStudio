using FaceSwapStudioWeb.Models;

namespace FaceSwapStudioWeb.Data;

public class SingleImagesProcessingResultMoqContext
{
    public List<SingleImagesProcessingResultModel> ProcessingResultModels { get; set; }
    
    public SingleImagesProcessingResultMoqContext()
    {
        ProcessingResultModels = new List<SingleImagesProcessingResultModel>();
        
        // var base64TestImage =
        //     Convert.ToBase64String(File.ReadAllBytes("../../faceSwapApi/test_images/body/body1.png"));
        //
        // for (int i = 0; i < 6; i++)
        // {
        //     ProcessingResultModels.Add(new SingleImagesProcessingResultModel
        //     {
        //         Id = i,
        //         StartCalculationDateTime = DateTime.UtcNow,
        //         EndCalculationDateTime = DateTime.UtcNow,
        //         BodyImage = base64TestImage,
        //         FaceImage = base64TestImage,
        //         SwapImage = base64TestImage,
        //         EnchancedSwapImage = base64TestImage,
        //     });
        //     
        //     // ProcessingResultModels.Add(new SingleImagesProcessingResultModel
        //     // {
        //     //     Id = i,
        //     //     StartCalculationDateTime = DateTime.UtcNow,
        //     //     EndCalculationDateTime = DateTime.UtcNow,
        //     //     BodyImage = "BodyImage " + i,
        //     //     FaceImage = "FaceImage " + i,
        //     //     SwapImage = "SwapImage " + i,
        //     //     EnchancedSwapImage = "EnchancedSwapImage " + i,
        //     // });
        // }
    }
}