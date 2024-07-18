using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Console = System.Console;
using System;
using System.Net.Http.Json;
using System.Runtime.Serialization;

// public enum FaceProcessingStatus
// {
//     Unknown = 0,
//     Success = 1,
//     UnexpectedError = 2,
//     SourceImageNoFaceError = 3,
//     TargetImageNoFaceError = 4,
//     ImageNoFaceError = 5,
//     TargetImageFaceIndexOutOfRangeError = 6,
//     TargetImageFaceIndexNanError = 7,
//     NoRequiredArgsError = 8,
//     EmptyArgsError = 9,
//     BadImageError = 10,
// }
//
// public record class FaceRectangle(
//     int X1 = 0,
//     int Y1 = 0,
//     int X2 = 0,
//     int Y2 = 0);
//
// public record class DetectFaseResult(
//     // string? Status = null,
//     FaceProcessingStatus? Status = null,
//     List<FaceRectangle>? DetectedFacesRectangles = null);
//
// public record class ImageRecord(
//     string? image = null);

// namespace declaration 
namespace FaceSwapApiCaller
{
    static class Program
    {
        static async Task DetectFaces()
        {
            var base64Image =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/body/body1.png"));
            
            var faceSwapApiClient = new FaceProcessingApi.SwapFaceApi.SwapFaceApiClient("http://127.0.0.1:8000");
            var request = new FaceProcessingApi.SwapFaceApi.DetectFacesRequest(base64Image);
            var result = await faceSwapApiClient.DetectFaces(request);

            if (result != null)
            {
                if (result.Status != null)
                    Console.WriteLine(result.Status);

                if (result.DetectedFacesRectangles != null)
                    Console.WriteLine(result.DetectedFacesRectangles.Count);
            }
        }
        
        static async Task SwapFace()
        {
            var base64BodyImage =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/body/body1.png"));
            
            var base64FaceImage =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/face/face1.jpg"));

            var targetFaceIndex = 0;
            
            var client = new FaceProcessingApi.SwapFaceApi.SwapFaceApiClient("http://127.0.0.1:8000");
            var request = new FaceProcessingApi.SwapFaceApi.SwapFaceRequest(base64BodyImage, base64FaceImage, targetFaceIndex);
            // var request = new FaceProcessingApi.SwapFaceApi.SwapFaceRequest(base64BodyImage, "ddssfafsfsfsffssfwrwreretwtewrtd", targetFaceIndex);
            // var request = new FaceProcessingApi.SwapFaceApi.SwapFaceRequest(base64BodyImage, "dd", targetFaceIndex);
            var result = await client.SwapFace(request);

            if (result != null)
            {
                if (result.Status != null)
                    Console.WriteLine(result.Status);

                // if (result.Image != null)
                //     Console.WriteLine(result.Image);
            }
        }

        static async Task EnchanceFaces()
        {
            var base64Image =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/face_swap_result.png"));
            
            var enchanceSwapApiClient = new FaceProcessingApi.EnchanceFaceApi.EnchanceFaceApiClient("http://127.0.0.1:7860");
            var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, base64Image);
            // var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, "sadasfdasf");
            var result = await enchanceSwapApiClient.EnchanceFaces(request);

            if (result != null)
            {
                Console.WriteLine("Image is null: " + (result.Image == null));
            }
        }

        static async Task Main(string[] args)
        {
            // // using HttpClient client = new();
            // //
            // // // var jsonObject = (dynamic)new JsonObject();
            // // // jsonObject.image = "sfsmfskfksfsfsf";
            // // var jsonObject = new { image = "dfslakfklsjfkasjf"};
            // // // myObject.Data2 = "some more data";
            // //
            // // // var json = await client.GetStringAsync(
            // // //     "http://127.0.0.1:8000/get");
            // // //
            // // // Console.Write(json);
            // //
            // // var url = "http://127.0.0.1:8000/detect_face";
            // //
            // // var content = new StringContent(jsonObject.AsJson(), Encoding.UTF8, "application/json");
            // // var result = client.PostAsync(url, content).Result;
            // // Console.Write(result);
            //
            // var url = "http://127.0.0.1:8000/detect_face";
            //
            // using HttpClient httpClient = new();
            //
            // var base64Image =
            //     Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/body/body1.png"));
            //
            // // using StringContent jsonContent = new(
            // //     JsonSerializer.Serialize(new
            // //     {
            // //         image = base64Image,
            // //     }),
            // //     Encoding.UTF8,
            // //     "application/json");
            //
            // // using HttpResponseMessage response = await httpClient.PostAsync(
            // //     url,
            // //     jsonContent);
            //
            // var imageRecord = new ImageRecord(base64Image);
            // using HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, imageRecord);
            //
            // // response.EnsureSuccessStatusCode()
            // //     .WriteRequestToConsole();
            //
            // // var jsonResponse = await response.Content.ReadAsStringAsync();
            // // Console.WriteLine($"{jsonResponse}\n");
            //
            // var jsonResponse = await response.Content.ReadFromJsonAsync<DetectFaseResult>();
            // Console.WriteLine(jsonResponse.Status);
            // Console.WriteLine(jsonResponse.DetectedFacesRectangles.Count);

            // await DetectFaces();
            // await SwapFace();
            await EnchanceFaces();
        }
    }
}