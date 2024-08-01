using System.ComponentModel.DataAnnotations;

namespace FaceSwapStudioWeb.Models;

public class FaceSwapRequestModel
{
    public IFormFile BodyImage { get; set; }
    public IFormFile FaceImage { get; set; }
    public int BodyImageFaceIndex { get; set; }
}

public class FaceSwapRequest2Model
{
    public int BodyImage { get; set; }
    public int FaceImage { get; set; }
    public int BodyImageFaceIndex { get; set; }
}