namespace FaceSwapStudioWeb.Models;

public class SingleImagesProcessingResultModel
{
    public long Id { get; set; }
    
    public string? BodyImage { get; set; }
    public string? FaceImage { get; set; }
    public string? SwapImage { get; set; }
    public string? EnchancedSwapImage { get; set; }

    public static void CopyDataFields(SingleImagesProcessingResultModel dataSourceModel, SingleImagesProcessingResultModel dataDestinationModel)
    {
        dataDestinationModel.BodyImage = dataSourceModel.BodyImage;
        dataDestinationModel.FaceImage = dataSourceModel.FaceImage;
        dataDestinationModel.SwapImage = dataSourceModel.SwapImage;
        dataDestinationModel.EnchancedSwapImage = dataSourceModel.EnchancedSwapImage;
    }
}