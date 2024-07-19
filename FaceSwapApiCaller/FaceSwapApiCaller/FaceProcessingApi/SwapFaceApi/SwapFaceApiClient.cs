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
        Console.WriteLine("Call detect faces api by url: " + SwapFaceApiUrl + ":");
        
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
            // return await response.Content.ReadFromJsonAsync<DetectFacesResult>();
            
            var result = await response.Content.ReadFromJsonAsync<DetectFacesResult>();

            if (result != null)
            {
                Console.WriteLine("Detect face api result:");
                
                if (result.Status != null)
                    Console.WriteLine("Status: " + result.Status);

                if (result.DetectedFacesRectangles != null)
                {
                    Console.WriteLine("Detected faces count: " + result.DetectedFacesRectangles.Count);
                    result.DetectedFacesRectangles.ForEach(Console.WriteLine);
                }
            }

            return result;
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
        Console.WriteLine("Call swap face api by url: " + SwapFaceApiUrl + ":");
        
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
            // return await response.Content.ReadFromJsonAsync<SwapFaceResult>();
            
            var result = await response.Content.ReadFromJsonAsync<SwapFaceResult>();

            if (result != null)
            {
                Console.WriteLine("Swap face api result:");
                
                if (result.Status != null)
                    Console.WriteLine("Status: " + result.Status);
                
                Console.WriteLine("Image is null: " + (result.Image == null));
            }

            return result;
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