using System.Diagnostics;
using FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

namespace FaceSwapApiCaller.FaceProcessingApi;

public class FaceProcessingService
{
    public FaceProcessingService(Configuration.FaceProcessingApiConfiguration configuration)
    {
        Debug.WriteLine("External api configuration: " + configuration);
        InitApi(configuration);
    }

    private void InitApi(Configuration.FaceProcessingApiConfiguration configuration)
    {
        SwapFaceApi = new SwapFaceApiClient(configuration.FaceSwapApiUrl);
        EnchanceFaceApi = new EnchanceFaceApiClient(configuration.StableDiffusionApiUrl); 
    }
    
    public SwapFaceApiClient SwapFaceApi { get; private set; }
    public EnchanceFaceApiClient EnchanceFaceApi{ get; private set; }
}