using System.Net.Http.Json;

namespace FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

public class SwapFaceApiClient
{
    public SwapFaceApiClient(string apiUrl) => ApiUrl = apiUrl;
    
    public string ApiUrl { get; private set; }
    
    private const string DetectFaceUrl = "detect_face";
    private const string SwapFaceUrl = "swap_face";
    
    private string DetectFaceApiUrl => ApiUrl + "/" + DetectFaceUrl;
    private string SwapFaceApiUrl => ApiUrl + "/" + SwapFaceUrl;
    
    private HttpClient _httpClient = new();

    public async Task<DetectFaceResult?> DetectFaces(DetectFaceRequest request)
    {
        var imageRecord = new DetectFaceRequest(request.Image);
        using var response = await _httpClient.PostAsJsonAsync(DetectFaceApiUrl, imageRecord);
        var jsonResponse = await response.Content.ReadFromJsonAsync<DetectFaceResult>();
        return jsonResponse;
    }
}