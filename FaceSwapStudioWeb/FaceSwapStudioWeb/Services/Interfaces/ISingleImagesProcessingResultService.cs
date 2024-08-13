using FaceSwapStudioWeb.Models;

namespace FaceSwapStudioWeb.Services.Interfaces;

public interface ISingleImagesProcessingResultService
{
    SingleImagesProcessingResultModel Create(SingleImagesProcessingResultModel model);
    
    SingleImagesProcessingResultModel Update(SingleImagesProcessingResultModel model);
    
    SingleImagesProcessingResultModel Get(long id);
    
    List<SingleImagesProcessingResultModel> Get();
    
    bool Delete(long id);
}