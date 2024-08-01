namespace FaceSwapStudioWeb.Services;

public class CustomConvert
{
    public string FileToBase64(IFormFile file)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
    }

    public IFormFile Base64ToFile(string base64String)
    {
        var bytes = Convert.FromBase64String(base64String);
        var stream = new MemoryStream(bytes);
        
        var file = new FormFile(stream, 0, bytes.Length, "SwapImage.png", "SwapImage.png");
        // file.Headers.ContentType = "application/json";
        return file;
        
        // var form = new MultipartFormDataContent();
        //        
        // byte[] fileData = Convert.FromBase64String(base64String);
        //
        // ByteArrayContent byteContent = new ByteArrayContent(fileData);
        //
        // byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
        //
        // var fileStream = byteContent.ReadAsStream();
        //
        // var file = new FormFile(fileStream, 0, fileStream.Length, "SwapImage.png", "SwapImage.png");
        //
        // return file;
    }
}