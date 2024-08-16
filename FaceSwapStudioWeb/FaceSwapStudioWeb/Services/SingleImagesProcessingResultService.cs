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
        if (model.StartCalculationDateTime == DateTime.MinValue)
            model.StartCalculationDateTime = DateTime.UtcNow;
        
        if (model.EndCalculationDateTime == DateTime.MinValue)
            model.EndCalculationDateTime = DateTime.UtcNow;
        
        var lastModel = _processingResultDataContext.ProcessingResultModels.LastOrDefault();
        var newId = lastModel is null ? 1 : lastModel.Id + 1;

        model.Id = newId;
        
        _processingResultDataContext.ProcessingResultModels.Add(model);
        
        SingleImagesProcessingResultModelSaveProxy saveProxy = new(model);
        saveProxy.Create();

        return model;
    }

    public SingleImagesProcessingResultModel? Update(SingleImagesProcessingResultModel model)
    {
        var modelToUpdate = _processingResultDataContext.ProcessingResultModels.FirstOrDefault(x => x.Id == model.Id);

        // if (modelToUpdate != null)
        // {
            SingleImagesProcessingResultModel.CopyDataFields(model, modelToUpdate);
            return model;
        // }

        // return null;
    }

    public SingleImagesProcessingResultModel Get(long id)
    {
        return _processingResultDataContext.ProcessingResultModels.FirstOrDefault(x => x.Id == id);
    }
    
    public List<SingleImagesProcessingResultModel> Get()
    {
        return _processingResultDataContext.ProcessingResultModels;
    }

    public bool Delete(long id)
    {
        var modelToDelete = _processingResultDataContext.ProcessingResultModels.FirstOrDefault(x => x.Id == id);
        return _processingResultDataContext.ProcessingResultModels.Remove(modelToDelete);
    }
}