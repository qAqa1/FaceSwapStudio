namespace FaceSwapStudioWeb.Models;

public class SingleImagesProcessingResultModel
{
    public long Id { get; set; }

    public DateTime StartCalculationDateTime { get; set; }
    public DateTime EndCalculationDateTime { get; set; }

    public string BodyImage { get; set; }
    public string FaceImage { get; set; }
    public string SwapImage { get; set; }
    public string EnchancedSwapImage { get; set; }

    public static void CopyDataFields(SingleImagesProcessingResultModel dataSourceModel,
        SingleImagesProcessingResultModel dataDestinationModel)
    {
        dataDestinationModel.StartCalculationDateTime = dataSourceModel.StartCalculationDateTime;
        dataDestinationModel.EndCalculationDateTime = dataSourceModel.EndCalculationDateTime;
        dataDestinationModel.BodyImage = dataSourceModel.BodyImage;
        dataDestinationModel.FaceImage = dataSourceModel.FaceImage;
        dataDestinationModel.SwapImage = dataSourceModel.SwapImage;
        dataDestinationModel.EnchancedSwapImage = dataSourceModel.EnchancedSwapImage;
    }
}

public class SingleImagesProcessingResultModelSaveProxy
{
    public SingleImagesProcessingResultModel Model { get; set; }

    public static readonly string SaveDirectory = "DataContent";

    public SingleImagesProcessingResultModelSaveProxy(SingleImagesProcessingResultModel processingResultModel)
    {
        Model = processingResultModel;
    }

    private string FilenamePrefix
    {
        get => Model.EndCalculationDateTime.ToString("yyyy-MM-dd_HH-mm-s.ffff") + $"__{Model.Id}";
    }

    // public string BodyImageFilename
    // {
    //     get => model.EndCalculationDateTime.ToString("yyyy-MM-dd_HH-mm-s.ffff") + $"__{model.Id}";
    // }
    //
    // public string FaceImageFilename
    // {
    //     get => model.EndCalculationDateTime.ToString("yyyy-MM-dd_HH-mm-s.ffff") + $"__{model.Id}";
    // }
    //
    // public string SwapImageFilename
    // {
    //     get => model.EndCalculationDateTime.ToString("yyyy-MM-dd_HH-mm-s.ffff") + $"__{model.Id}";
    // }
    //
    // public string EnchancedSwapImageFilename
    // {
    //     get => model.EndCalculationDateTime.ToString("yyyy-MM-dd_HH-mm-s.ffff") + $"__{model.Id}";
    // }

    private const string BodyImageFilename = "__body_image.png";
    private const string FaceImageFilename = "__face_image.png";
    private const string SwapImageFilename = "__swap_image.png";
    private const string EnchancedSwapImageFilename = "__enchanced_swap_image.png";

    public void Create()
    {
        Directory.CreateDirectory(SaveDirectory);

        var filenamePrefix = FilenamePrefix;

        Action<string, string> saveImage = (base64Image, filenamePostfix) =>
        {
            if (base64Image != "")
                File.WriteAllBytes(
                    SaveDirectory + Path.DirectorySeparatorChar + filenamePrefix + filenamePostfix,
                    Convert.FromBase64String(base64Image));
        };

        saveImage(Model.BodyImage, BodyImageFilename);
        saveImage(Model.FaceImage, FaceImageFilename);
        saveImage(Model.SwapImage, SwapImageFilename);
        saveImage(Model.EnchancedSwapImage, EnchancedSwapImageFilename);
    }

    public void Get()
    {
        var filenamePrefix = FilenamePrefix;

        Func<string, string> readImage = (filenamePostfix) =>
            Convert.ToBase64String(File.ReadAllBytes(SaveDirectory + Path.DirectorySeparatorChar + filenamePrefix +
                                                     filenamePostfix));

        Model.BodyImage = readImage(BodyImageFilename);
        Model.FaceImage = readImage(FaceImageFilename);
        Model.SwapImage = readImage(SwapImageFilename);
        Model.EnchancedSwapImage = readImage(EnchancedSwapImageFilename);
    }

    public void Delete()
    {
        var filenamePrefix = FilenamePrefix;

        Action<string> deleteFile = (filenamePostfix) =>
        {
            var fileName = SaveDirectory + Path.DirectorySeparatorChar + filenamePrefix + filenamePostfix;
            
            if (File.Exists(fileName))
                File.Delete(fileName);
        };
        
        deleteFile(BodyImageFilename);
        deleteFile(FaceImageFilename);
        deleteFile(SwapImageFilename);
        deleteFile(EnchancedSwapImageFilename);
    }
}