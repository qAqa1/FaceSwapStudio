using FaceSwapStudioWeb.Models;

namespace FaceSwapStudioWeb.Services.Interfaces;

public interface ISingleImagesProcessingResultService
{
    SingleImagesProcessingResultModel Create(SingleImagesProcessingResultModel model);
    
    SingleImagesProcessingResultModel Update(SingleImagesProcessingResultModel model);
    
    SingleImagesProcessingResultModel Get(long id);
    
    bool Delete();
}