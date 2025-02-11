from api.configuration import ApiURLs
from api import face_swap
from api import enchance_faces
from api.face_swap_core import FaceProcessingStatus

class FaceProcessingRequestModel:
    def __init__(self, body_image, face_image, body_image_face_index=0, gfpgan_visibility=1):
        self.body_image = body_image
        self.face_image = face_image
        self.body_image_face_index = body_image_face_index
        self.gfpgan_visibility = gfpgan_visibility


class FaceProcessingResultModel:
    def __init__(self, status, image):
        self.status = status
        self.image = image


def process_faces(api_urls: ApiURLs, request: FaceProcessingRequestModel) -> FaceProcessingResultModel:
    face_swap_request = face_swap.FaceSwapRequestModel(request.body_image, request.face_image, request.body_image_face_index)
    face_swap_result = face_swap.face_swap_request(api_urls.face_swap, face_swap_request)

    if face_swap_result.status is FaceProcessingStatus.SUCCESS:
        enchance_faces_request = enchance_faces.EnchanceFacesRequestModel(request.gfpgan_visibility, face_swap_result.image)
        enchance_faces_result = enchance_faces.enchance_faces_request(api_urls.stable_diffusion, enchance_faces_request)
        return FaceProcessingResultModel(face_swap_result.status, enchance_faces_result.image)

    return FaceProcessingResultModel(face_swap_result.status, None)
