using System.Diagnostics;
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
        Debug.WriteLine("Call enchance faces api by url: " + EnchanceFaceApiUrl + ":");
        
        // using var response = await _httpClient.PostAsJsonAsync(EnchanceFaceApiUrl, request);
        // return await response.Content.ReadFromJsonAsync<EnchanceFacesResult>();
        
        HttpResponseMessage? response = null; 
        
        try
        {
            response = await _httpClient.PostAsJsonAsync(EnchanceFaceApiUrl, request);
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Unexpected exception while sending request:\n" + exception);
            return null;
        }
        
        try
        {
            // return await response.Content.ReadFromJsonAsync<EnchanceFacesResult>();
            
            var result = await response.Content.ReadFromJsonAsync<EnchanceFacesResult>();

            if (result != null)
            {
                Debug.WriteLine("Enchance face api result:");
                Debug.WriteLine("Image is null: " + (result.Image == null));
            }

            return result;
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Unexpected exception while reading request:\n" + exception);
            
            try
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Error data from server:\n{jsonResponse}\n");
            }
            catch { }
            
            return null;
        }
    }
}