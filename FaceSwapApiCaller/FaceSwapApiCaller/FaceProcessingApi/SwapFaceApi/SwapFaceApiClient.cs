using System.Net.Http.Json;

namespace FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

public class SwapFaceApiClient
{
    public SwapFaceApiClient(string apiUrl) => ApiUrl = apiUrl;
    
    public string ApiUrl { get; private set; }
    
    private const string DetectFaceApiName = "detect_faces";
    private const string SwapFaceApiName = "swap_face";
    
    private string DetectFaceApiUrl => ApiUrl + "/" + DetectFaceApiName;
    private string SwapFaceApiUrl => ApiUrl + "/" + SwapFaceApiName;
    
    private HttpClient _httpClient = new();

    public async Task<DetectFacesResult?> DetectFaces(DetectFacesRequest request)
    {
        using var response = await _httpClient.PostAsJsonAsync(DetectFaceApiUrl, request);
        // var jsonResponse = await response.Content.ReadFromJsonAsync<DetectFaceResult>();
        // return jsonResponse;
        
        return await response.Content.ReadFromJsonAsync<DetectFacesResult>();
    }
    
    public async Task<SwapFaceResult?> SwapFace(SwapFaceRequest request)
    {
        using var response = await _httpClient.PostAsJsonAsync(SwapFaceApiUrl, request);
        
        // var jsonResponse = await response.Content.ReadAsStringAsync();
        // Console.WriteLine($"{jsonResponse}\n");
        
        return await response.Content.ReadFromJsonAsync<SwapFaceResult>();
    }
}