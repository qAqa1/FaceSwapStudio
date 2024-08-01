using System.Diagnostics;
using FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

namespace FaceSwapApiCaller.FaceProcessingApi;

public class FaceProcessingService
{
    public FaceProcessingService(Configuration.FaceProcessingApiConfiguration configuration)
    {
        // var config = new ConfigurationBuilder().AddJsonFile("ExternalApiSettings.json").Build();
        // var faceSwapApiUrl = config.GetValue<string>("ApiUrl:FaceSwapApi");
        // var stableDiffusionApiUrl = config.GetValue<string>("ApiUrl:StableDiffusionApiUrl");
        
        // Configuration.FaceProcessingApiConfiguration configuration = new(faceSwapApiUrl, stableDiffusionApiUrl);
        Debug.WriteLine("External api configuration: " + configuration);
        InitApi(configuration);
        // var configurationFile = new Configuration.FaceProcessingApiXmpConfigurationFile(ConfigurationFilename);
        // if (configurationFile.Exist())
        // {
        //     Debug.WriteLine("Configuration file " + ConfigurationFilename + " exist");
        //     var configuration = configurationFile.Read();
        //     Debug.WriteLine("Use from file configuration:\n" + configuration);
        //     InitApi(configuration);
        // }
        // else
        // {
        //     Debug.WriteLine("Configuration file " + ConfigurationFilename + " don't exist, write and use default configuration:\n" + _defaultConfiguration);
        //     configurationFile.Write(_defaultConfiguration);
        //     InitApi(_defaultConfiguration);
        // }
    }
    
    // private static readonly Configuration.FaceProcessingApiConfiguration _defaultConfiguration =
    //     new ("http://127.0.0.1:8000", "http://127.0.0.1:7860");


    private void InitApi(Configuration.FaceProcessingApiConfiguration configuration)
    {
        SwapFaceApi = new SwapFaceApiClient(configuration.FaceSwapApiUrl);
        EnchanceFaceApi = new EnchanceFaceApiClient(configuration.StableDiffusionApiUrl); 
    }
    
    public SwapFaceApiClient SwapFaceApi { get; private set; }
    public EnchanceFaceApiClient EnchanceFaceApi{ get; private set; }
}