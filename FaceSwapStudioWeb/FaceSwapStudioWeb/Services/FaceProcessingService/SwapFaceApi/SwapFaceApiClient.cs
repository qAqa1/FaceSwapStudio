using System.Diagnostics;
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
        Debug.WriteLine("Call detect faces api by url: " + SwapFaceApiUrl + ":");

        // using var response = await _httpClient.PostAsJsonAsync(DetectFaceApiUrl, request);
        // return await response.Content.ReadFromJsonAsync<DetectFacesResult>();

        HttpResponseMessage? response = null;

        try
        {
            response = await _httpClient.PostAsJsonAsync(DetectFaceApiUrl, request);
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Unexpected exception while sending request:\n" + exception);
            return null;
        }

        try
        {
            // return await response.Content.ReadFromJsonAsync<DetectFacesResult>();

            var result = await response.Content.ReadFromJsonAsync<DetectFacesResult>();

            if (result != null)
            {
                Debug.WriteLine("Detect face api result:");

                if (result.Status != null)
                    Debug.WriteLine("Status: " + result.Status);

                if (result.DetectedFacesRectangles != null)
                {
                    Debug.WriteLine("Detected faces count: " + result.DetectedFacesRectangles.Count);
                    result.DetectedFacesRectangles.ForEach(rectangle => Debug.WriteLine(rectangle));
                }
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
            catch
            {
            }

            return null;
        }
    }

    public async Task<SwapFaceResult?> SwapFace(SwapFaceRequest request)
    {
        System.Diagnostics.Debug.WriteLine("Call swap face api by url: " + SwapFaceApiUrl + ":");

        // using var response = await _httpClient.PostAsJsonAsync(SwapFaceApiUrl, request);
        // return await response.Content.ReadFromJsonAsync<SwapFaceResult>();

        HttpResponseMessage? response = null;

        try
        {
            response = await _httpClient.PostAsJsonAsync(SwapFaceApiUrl, request);
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Unexpected exception while sending request:\n" + exception);
            return null;
        }

        try
        {
            // return await response.Content.ReadFromJsonAsync<SwapFaceResult>();

            var result = await response.Content.ReadFromJsonAsync<SwapFaceResult>();

            if (result != null)
            {
                Debug.WriteLine("Swap face api result:");

                if (result.Status != null)
                    Debug.WriteLine("Status: " + result.Status);

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
            catch
            {
            }

            return null;
        }
    }
}