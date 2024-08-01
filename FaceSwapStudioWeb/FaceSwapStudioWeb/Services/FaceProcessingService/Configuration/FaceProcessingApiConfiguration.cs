namespace FaceSwapApiCaller.FaceProcessingApi.Configuration;

public record FaceProcessingApiConfiguration(
    string FaceSwapApiUrl,
    string StableDiffusionApiUrl);