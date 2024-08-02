using System.ComponentModel.DataAnnotations;

namespace FaceSwapStudioWeb.Models;

public class FaceSwapRequestModel
{
    public string BodyImage { get; set; }
    public string FaceImage { get; set; }
    public int BodyImageFaceIndex { get; set; }
}

// public class FaceSwapRequest2Model
// {
//     public int BodyImage { get; set; }
//     public int FaceImage { get; set; }
//     public int BodyImageFaceIndex { get; set; }
// }