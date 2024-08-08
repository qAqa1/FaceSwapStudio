using FaceSwapStudioWeb.Data;
using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services.Interfaces;

namespace FaceSwapStudioWeb.Services;

public class SingleImagesProcessingResultService : ISingleImagesProcessingResultService
{
    private SingleImagesProcessingResultDataContext _processingResultDataContext;
    
    public SingleImagesProcessingResultService(SingleImagesProcessingResultDataContext processingResultDataContext)
    {
        _processingResultDataContext = processingResultDataContext;
    }
    
    public SingleImagesProcessingResultModel Create(SingleImagesProcessingResultModel model)
    {
        var lastModel = _processingResultDataContext.ProcessingResultModels.LastOrDefault();
        var newId = lastModel is null ? 1 : lastModel.Id + 1;

        model.Id = newId;
        _processingResultDataContext.ProcessingResultModels.Add(model);

        return model;
    }

    public SingleImagesProcessingResultModel Update(SingleImagesProcessingResultModel model)
    {
        var modelToUpdate = _processingResultDataContext.ProcessingResultModels.LastOrDefault(x => x.Id == model.Id);
        
        throw new NotImplementedException();
    }

    public SingleImagesProcessingResultModel Get(long id)
    {
        throw new NotImplementedException();
    }

    public bool Delete()
    {
        throw new NotImplementedException();
    }
}