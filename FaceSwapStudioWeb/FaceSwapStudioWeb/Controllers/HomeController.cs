using System.Diagnostics;
using FaceSwapApiCaller.FaceProcessingApi;
using FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;
using FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;
using Microsoft.AspNetCore.Mvc;
using FaceSwapStudioWeb.Models;
using FaceSwapStudioWeb.Services;

namespace FaceSwapStudioWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly FaceProcessingApi _faceProcessingApi;
    private readonly CustomConvert _customConvert;

    private readonly FaceSwapModel _faceSwapModel;

    public HomeController(ILogger<HomeController> logger, FaceProcessingApi faceProcessingApi,
        CustomConvert customConvert)
    {
        _logger = logger;
        _faceProcessingApi = faceProcessingApi;
        _customConvert = customConvert;

        _faceSwapModel = new FaceSwapModel(_customConvert);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SingleImagesProcessing()
    {
        return View(_faceSwapModel);
    }

    // public async Task<string> UploadFile(IFormFile file)
    // {
    //     // создаем папку для хранения файлов
    //     Directory.CreateDirectory(UploadPath);
    //         
    //     var extension = Path.GetExtension(file.FileName);
    //     var trustedFileNameForFileStorage = Path.ChangeExtension(Path.GetRandomFileName(), extension);
    //     var savePath = Path.Combine(UploadPath, trustedFileNameForFileStorage);
    //     
    //     using (var fileStream = new FileStream(savePath, FileMode.Create))
    //         await file.CopyToAsync(fileStream);
    //
    //     return savePath;
    // }
    //
    // private static readonly string UploadPath = $"{Directory.GetCurrentDirectory()}/Uploads";

    [HttpPost]
    public async Task<IActionResult> UploadImagesForSwap(IFormFile body, IFormFile face)
    {
        if (body != null && face != null)
        {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);
            //
            // _bodyFilename = await UploadFile(body);
            // _faceFilename = await UploadFile(face);
            //
            // Debug.WriteLine("Body filename: " + _bodyFilename);
            // Debug.WriteLine("Face filename: " + _faceFilename);


            // Console.WriteLine("EnchanceFaces:");
            //
            // var base64Image =
            //     Convert.ToBase64String(File.ReadAllBytes("../../../../../faceSwapApi/test_images/face_swap_result.png"));
            //
            // var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, base64Image);
            // // var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, "sadasfdasf");
            // var result = await FaceProcessingApi.FaceProcessingApiController.EnchanceFaceApi.EnchanceFaces(request);
            //
            // // if (result != null)
            // //     Console.WriteLine("Image is null: " + (result.Image == null));
            //
            // if (result.Image != null)
            //     File.WriteAllBytes("enchance_face_result.png", Convert.FromBase64String(result.Image));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _faceSwapModel.BodyImage = body;
            _faceSwapModel.FaceImage = face;

            var base64BodyImage =
                _customConvert.FileToBase64(_faceSwapModel.BodyImage);

            var base64FaceImage =
                _customConvert.FileToBase64(_faceSwapModel.FaceImage);

            var targetFaceIndex = 0;

            var swapFaceRequest = new SwapFaceRequest(base64BodyImage, base64FaceImage, targetFaceIndex);
            var swapFaceResult = await _faceProcessingApi.SwapFaceApi.SwapFace(swapFaceRequest);

            if (swapFaceResult is { Status: FaceProcessingStatus.Success })
            {
                _faceSwapModel.SwapImage = _customConvert.Base64ToFile(swapFaceResult.Image);

                var enchanceFacesRequest = new EnchanceFacesRequest(1, swapFaceResult.Image);
                // var request = new FaceProcessingApi.EnchanceFaceApi.EnchanceFacesRequest(1, "sadasfdasf");
                var enchanceFacesResult = await _faceProcessingApi.EnchanceFaceApi.EnchanceFaces(enchanceFacesRequest);

                // if (result != null)
                //     Console.WriteLine("Image is null: " + (result.Image == null));

                if (enchanceFacesResult != null && enchanceFacesResult.Image != null)
                    _faceSwapModel.EnchancedSwapImage = _customConvert.Base64ToFile(enchanceFacesResult.Image);
            }
        }

        return View("SingleImagesProcessing", _faceSwapModel);
        // return RedirectToAction("Index");
    }

    // [HttpPost]
    // public async Task<IActionResult> AddFile(IFormFile uploadedFile)
    // {
    //     if (uploadedFile != null)
    //     {
    //         // var trustedFileNameForDisplay = WebUtility.HtmlEncode(
    //         //     contentDisposition.FileName.Value);
    //         // var trustedFileNameForFileStorage = Path.GetRandomFileName();
    //
    //         // // путь к папке Files
    //         // string path = "/Files/" + uploadedFile.FileName;
    //         // // сохраняем файл в папку Files в каталоге wwwroot
    //         // using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
    //         // {
    //         //     await uploadedFile.CopyToAsync(fileStream);
    //         // }
    //         // FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
    //         // _context.Files.Add(file);
    //         // _context.SaveChanges();
    //         
    //         // создаем папку для хранения файлов
    //         Directory.CreateDirectory(UploadPath);
    //         
    //         var extension = Path.GetExtension(uploadedFile.FileName);
    //         var trustedFileNameForFileStorage = Path.ChangeExtension(Path.GetRandomFileName(), extension);
    //         
    //         if (!ModelState.IsValid)
    //         {
    //             return BadRequest(ModelState);
    //         }
    //
    //         var savePath = Path.Combine(UploadPath, trustedFileNameForFileStorage);
    //         Debug.WriteLine("Save path: " + savePath);
    //         
    //         using (var fileStream = new FileStream(savePath, FileMode.Create))
    //         {
    //             await uploadedFile.CopyToAsync(fileStream);
    //         }
    //     }
    //     return RedirectToAction("Index");
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}