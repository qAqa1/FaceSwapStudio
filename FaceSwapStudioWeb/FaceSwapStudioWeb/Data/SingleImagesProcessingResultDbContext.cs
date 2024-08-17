using FaceSwapStudioWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceSwapStudioWeb.Data;

public class SingleImagesProcessingResultDbContext : DbContext
{
    public DbSet<SingleImagesProcessingResultModel> ProcessingResultModels { get; set; }

    public SingleImagesProcessingResultDbContext(DbContextOptions<SingleImagesProcessingResultDbContext> options) : base(options)
    {
    }
}