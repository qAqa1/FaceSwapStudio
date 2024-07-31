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
        
        IFormFile file = new FormFile(stream, 0, bytes.Length, "result", "result.png");
        return file;
    }
}