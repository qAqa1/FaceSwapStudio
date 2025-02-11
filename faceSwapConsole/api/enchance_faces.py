import requests


class EnchanceFacesRequestModel:
    def __init__(self, gfpgan_visibility, image):
        self.gfpgan_visibility = gfpgan_visibility
        self.image = image


class EnchanceFacesResultModel:
    def __init__(self, image):
        self.image = image


def enchance_faces_request(stable_diffusion_api_url, request: EnchanceFacesRequestModel) -> EnchanceFacesResultModel:
    request_body = {
        'gfpgan_visibility': request.gfpgan_visibility,
        'image': request.image,
    }
    response = requests.post(stable_diffusion_api_url + "/sdapi/v1/extra-single-image", json=request_body)
    result_json = response.json()
    return EnchanceFacesResultModel(result_json["image"])
