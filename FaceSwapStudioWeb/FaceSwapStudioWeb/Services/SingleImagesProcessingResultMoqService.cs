using FaceSwapStudioWeb.Data;
using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services.Interfaces;

namespace FaceSwapStudioWeb.Services;

public class SingleImagesProcessingResultMoqService : ISingleImagesProcessingResultService
{
    private SingleImagesProcessingResultMoqContext _context;

    public SingleImagesProcessingResultMoqService(SingleImagesProcessingResultMoqContext context)
    {
        _context = context;
    }

    public SingleImagesProcessingResultModel Create(SingleImagesProcessingResultModel model)
    {
        if (model.StartCalculationDateTime == DateTime.MinValue)
            model.StartCalculationDateTime = DateTime.UtcNow;

        if (model.EndCalculationDateTime == DateTime.MinValue)
            model.EndCalculationDateTime = DateTime.UtcNow;

        var lastModel = _context.ProcessingResultModels.LastOrDefault();
        var newId = lastModel is null ? 1 : lastModel.Id + 1;

        model.Id = newId;

        _context.ProcessingResultModels.Add(model);

        return model;
    }

    public SingleImagesProcessingResultModel? Update(SingleImagesProcessingResultModel model)
    {
        var modelToUpdate = _context.ProcessingResultModels.FirstOrDefault(x => x.Id == model.Id);

        SingleImagesProcessingResultModel.CopyDataFields(model, modelToUpdate);
        return model;
    }

    public SingleImagesProcessingResultModel Get(long id)
    {
        return _context.ProcessingResultModels.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<SingleImagesProcessingResultModel> Get()
    {
        return _context.ProcessingResultModels;
    }

    public bool Delete(long id)
    {
        var modelToDelete = _context.ProcessingResultModels.FirstOrDefault(x => x.Id == id);
        return _context.ProcessingResultModels.Remove(modelToDelete);
    }
}