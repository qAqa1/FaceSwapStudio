using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Console = System.Console;
using System;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Xml;

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
            Console.WriteLine("DetectFaces:");
            
            var base64Image =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/body/body1.png"));
            
            var request = new FaceProcessingApi.SwapFaceApi.DetectFacesRequest(base64Image);
            var result = await FaceProcessingApi.FaceProcessingApiController.SwapFaceApi.DetectFaces(request);

            if (result != null)
            {
                // if (result.Status != null)
                //     Console.WriteLine(result.Status);

                // if (result.DetectedFacesRectangles != null)
                // {
                //     Console.WriteLine(result.DetectedFacesRectangles.Count);
                //     result.DetectedFacesRectangles.ForEach(Console.WriteLine);
                // }
            }
        }
        
        static async Task SwapFace()
        {
            Console.WriteLine("SwapFace:");
            
            var base64BodyImage =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/body/body1.png"));
            
            var base64FaceImage =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/face/face1.jpg"));

            var targetFaceIndex = 0;
            
            var request = new FaceProcessingApi.SwapFaceApi.SwapFaceRequest(base64BodyImage, base64FaceImage, targetFaceIndex);
            // var request = new FaceProcessingApi.SwapFaceApi.SwapFaceRequest(base64BodyImage, "ddssfafsfsfsffssfwrwreretwtewrtd", targetFaceIndex);
            // var request = new FaceProcessingApi.SwapFaceApi.SwapFaceRequest(base64BodyImage, "dd", targetFaceIndex);
            var result = await FaceProcessingApi.FaceProcessingApiController.SwapFaceApi.SwapFace(request);

            if (result != null)
            {
                // if (result.Status != null)
                //     Console.WriteLine(result.Status);

                // if (result.Image != null)
                //     Console.WriteLine(result.Image);
                
                if (result.Image != null)
                    File.WriteAllBytes("swap_face_result.png", Convert.FromBase64String(result.Image));
            }
        }

        static async Task EnchanceFaces()
        {
            Console.WriteLine("EnchanceFaces:");
            
            var base64Image =
                Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/face_swap_result.png"));
            
            var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, base64Image);
            // var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, "sadasfdasf");
            var result = await FaceProcessingApi.FaceProcessingApiController.EnchanceFaceApi.EnchanceFaces(request);

            // if (result != null)
            //     Console.WriteLine("Image is null: " + (result.Image == null));
            
            if (result.Image != null)
                File.WriteAllBytes("enchance_face_result.png", Convert.FromBase64String(result.Image));
        }

        static async Task Main(string[] args)
        {
            await DetectFaces();
            await SwapFace();
            await EnchanceFaces();
        }
    }
}