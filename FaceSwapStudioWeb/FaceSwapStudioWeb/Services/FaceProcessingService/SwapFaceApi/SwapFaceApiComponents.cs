namespace FaceSwapApiCaller.FaceProcessingApi.SwapFaceApi;

public enum FaceProcessingStatus
{
    UnknownError = 0,
    Success = 1,
    UnexpectedError = 2,
    SourceImageNoFaceError = 3,
    TargetImageNoFaceError = 4,
    ImageNoFaceError = 5,
    TargetImageFaceIndexOutOfRangeError = 6,
    TargetImageFaceIndexNanError = 7,
    NoRequiredArgsError = 8,
    EmptyArgsError = 9,
    BadImageError = 10,
}

public record class DetectFacesRequest(
    string Image);

public record class FaceRectangle(
    int X1 = 0,
    int Y1 = 0,
    int X2 = 0,
    int Y2 = 0);

public record class DetectFacesResult(
    FaceProcessingStatus? Status = null,
    List<FaceRectangle>? DetectedFacesRectangles = null);
    
public record class SwapFaceRequest(
    string Body,
    string Face,
    int TargetFaceIndex);
    
public record class SwapFaceResult(
    FaceProcessingStatus? Status = null,
    string? Image = null);