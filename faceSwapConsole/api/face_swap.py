import requests

from api.face_swap_core import FaceProcessingStatus


class FaceSwapRequestModel:
    def __init__(self, body_image, face_image, body_image_face_index):
        self.body_image = body_image
        self.face_image = face_image
        self.body_image_face_index = body_image_face_index


class FaceSwapResultModel:
    def __init__(self, status, image):
        self.status = status
        self.image = image


def face_swap_request(face_swap_api_url, request: FaceSwapRequestModel) -> FaceSwapResultModel:
    request_body = {
        'body': request.body_image,
        'face': request.face_image,
        'targetFaceIndex': request.body_image_face_index,
    }
    response = requests.post(face_swap_api_url + "/swap_face", json=request_body)
    result_json = response.json()
    status = FaceProcessingStatus(result_json["status"])
    if status is FaceProcessingStatus.SUCCESS:
        return FaceSwapResultModel(status, result_json["image"])
    return FaceSwapResultModel(status, None)
