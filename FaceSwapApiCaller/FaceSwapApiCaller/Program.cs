using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Console = System.Console;
using System;
using System.Net.Http.Json;

public record class FaceRectangle(
    int X1 = 0,
    int Y1 = 0,
    int X2 = 0,
    int Y2 = 0);

public record class DetectFaseResult(
    string? Status = null,
    List<FaceRectangle>? DetectedFacesRectangles = null);

public record class ImageRecord(
    string? image = null);

// namespace declaration 
namespace FaceSwapApiCaller
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            // using HttpClient client = new();
            //
            // // var jsonObject = (dynamic)new JsonObject();
            // // jsonObject.image = "sfsmfskfksfsfsf";
            // var jsonObject = new { image = "dfslakfklsjfkasjf"};
            // // myObject.Data2 = "some more data";
            //
            // // var json = await client.GetStringAsync(
            // //     "http://127.0.0.1:8000/get");
            // //
            // // Console.Write(json);
            //
            // var url = "http://127.0.0.1:8000/detect_face";
            //
            // var content = new StringContent(jsonObject.AsJson(), Encoding.UTF8, "application/json");
            // var result = client.PostAsync(url, content).Result;
            // Console.Write(result);

            var url = "http://127.0.0.1:8000/detect_face";

            using HttpClient httpClient = new();

            var base64Image =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/body/body1.png"));

            // using StringContent jsonContent = new(
            //     JsonSerializer.Serialize(new
            //     {
            //         image = base64Image,
            //     }),
            //     Encoding.UTF8,
            //     "application/json");

            // using HttpResponseMessage response = await httpClient.PostAsync(
            //     url,
            //     jsonContent);

            var imageRecord = new ImageRecord(base64Image);
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, imageRecord);
            
        // response.EnsureSuccessStatusCode()
        //     .WriteRequestToConsole();

            // var jsonResponse = await response.Content.ReadAsStringAsync();
            // Console.WriteLine($"{jsonResponse}\n");
            
            var jsonResponse = await response.Content.ReadFromJsonAsync<DetectFaseResult>();
            Console.WriteLine(jsonResponse.Status);
            Console.WriteLine(jsonResponse.DetectedFacesRectangles.Count);
        }
    }
}