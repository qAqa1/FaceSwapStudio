using FaceSwapStudioWeb.Data;
using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services.Interfaces;

namespace FaceSwapStudioWeb.Services;

public class SingleImagesProcessingResultDbService : ISingleImagesProcessingResultService
{
    private SingleImagesProcessingResultDbContext _context;

    public SingleImagesProcessingResultDbService(SingleImagesProcessingResultDbContext context)
    {
        _context = context;
    }
    
    public SingleImagesProcessingResultModel Create(SingleImagesProcessingResultModel model)
    {
        var addedModel = _context.ProcessingResultModels.Add(model);
        _context.SaveChanges();

        return addedModel.Entity;
    }

    public SingleImagesProcessingResultModel Update(SingleImagesProcessingResultModel model)
    {
        var modelToUpdate = Get(model.Id);
        SingleImagesProcessingResultModel.CopyDataFields(model, modelToUpdate);
        
        _context.ProcessingResultModels.Update(modelToUpdate);
        _context.SaveChanges();

        return modelToUpdate;
    }

    public SingleImagesProcessingResultModel Get(long id)
    {
        return _context.ProcessingResultModels.Find(id);
    }

    public IEnumerable<SingleImagesProcessingResultModel> Get()
    {
        return _context.ProcessingResultModels;
    }

    public bool Delete(long id)
    {
        var modelToDelete = Get(id);

        if (modelToDelete != null)
        {
            _context.ProcessingResultModels.Remove(modelToDelete);
            _context.SaveChanges();
            
            return true;
        }

        return false;
    }
}