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
        // using var response = await _httpClient.PostAsJsonAsync(DetectFaceApiUrl, request);
        // return await response.Content.ReadFromJsonAsync<DetectFacesResult>();
        
        HttpResponseMessage? response = null; 
        
        try
        {
            response = await _httpClient.PostAsJsonAsync(DetectFaceApiUrl, request);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unexpected exception while sending request:\n" + exception);
            return null;
        }
        
        try
        {
            return await response.Content.ReadFromJsonAsync<DetectFacesResult>();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unexpected exception while reading request:\n" + exception);
            
            try
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error data from server:\n{jsonResponse}\n");
            }
            catch { }
            
            return null;
        }
    }
    
    public async Task<SwapFaceResult?> SwapFace(SwapFaceRequest request)
    {
        // using var response = await _httpClient.PostAsJsonAsync(SwapFaceApiUrl, request);
        // return await response.Content.ReadFromJsonAsync<SwapFaceResult>();

        HttpResponseMessage? response = null; 
        
        try
        {
            response = await _httpClient.PostAsJsonAsync(SwapFaceApiUrl, request);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unexpected exception while sending request:\n" + exception);
            return null;
        }
        
        try
        {
            return await response.Content.ReadFromJsonAsync<SwapFaceResult>();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unexpected exception while reading request:\n" + exception);
            
            try
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error data from server:\n{jsonResponse}\n");
            }
            catch { }
            
            return null;
        }
    }
}