using FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

namespace FaceSwapApiCaller.FaceProcessingApi;

public static class FaceProcessingApiController
{
    static FaceProcessingApiController()
    {
        var configurationFile = new Configuration.FaceProcessingApiXmpConfigurationFile(ConfigurationFilename);
        if (configurationFile.Exist())
        {
            Console.WriteLine("Configuration file " + ConfigurationFilename + " exist");
            var configuration = configurationFile.Read();
            Console.WriteLine("Use from file configuration:\n" + configuration);
            InitApi(configuration);
        }
        else
        {
            Console.WriteLine("Configuration file " + ConfigurationFilename + " don't exist, write and use default configuration:\n" + _defaultConfiguration);
            configurationFile.Write(_defaultConfiguration);
            InitApi(_defaultConfiguration);
        }
    }

    // public static string ConfigurationFilename { get; private set; } = "ExternalApiConfiguration.xml";
    private static string ConfigurationFilename { get; set; } = "ExternalApiConfiguration.xml";

    private static readonly Configuration.FaceProcessingApiConfiguration _defaultConfiguration =
        new ("http://127.0.0.1:8000", "http://127.0.0.1:7860");


    private static void InitApi(Configuration.FaceProcessingApiConfiguration configuration)
    {
        SwapFaceApi = new SwapFaceApiClient(configuration.FaceSwapApiUrl);
        EnchanceFaceApi = new EnchanceFaceApiClient(configuration.StableDiffusionApiUrl); 
    }
    
    public static SwapFaceApiClient SwapFaceApi { get; private set; }
    public static EnchanceFaceApiClient EnchanceFaceApi{ get; private set; }
}