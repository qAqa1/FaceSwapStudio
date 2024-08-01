using System.Text.Json.Serialization;

namespace FaceSwapApiCaller.FaceProcessingApi.EnchanceFaceApi;

public record class EnchanceFacesRequest
{
    public EnchanceFacesRequest(int gfpganVisibility, string image)
    {
        GfpganVisibility = gfpganVisibility;
        Image = image;
    }
    
    [JsonPropertyName("gfpgan_visibility")]
    public int GfpganVisibility { get; set; }
    
    public string Image { get; set; }
}

public record class EnchanceFacesResult(
    string? Image = null);