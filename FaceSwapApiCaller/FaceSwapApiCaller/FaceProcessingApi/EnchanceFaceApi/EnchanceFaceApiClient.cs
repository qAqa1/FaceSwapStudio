using System.Net.Http.Json;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

namespace FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;

public class EnchanceFaceApiClient
{
    public EnchanceFaceApiClient(string apiUrl) => ApiUrl = apiUrl;

    public string ApiUrl { get; private set; }

    private const string EnchanceFaceApiName = "sdapi/v1/extra-single-image";

    private string EnchanceFaceApiUrl => ApiUrl + "/" + EnchanceFaceApiName;

    private HttpClient _httpClient = new();

    public async Task<EnchanceFacesResult?> EnchanceFaces(EnchanceFacesRequest request)
    {
        // using var response = await _httpClient.PostAsJsonAsync(EnchanceFaceApiUrl, request);
        // return await response.Content.ReadFromJsonAsync<EnchanceFacesResult>();
        
        HttpResponseMessage? response = null; 
        
        try
        {
            response = await _httpClient.PostAsJsonAsync(EnchanceFaceApiUrl, request);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unexpected exception while sending request:\n" + exception);
            return null;
        }
        
        try
        {
            return await response.Content.ReadFromJsonAsync<EnchanceFacesResult>();
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