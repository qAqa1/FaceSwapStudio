import json


class ApiURLs:
    def __init__(self, configuration_file = 'ExternalApiSettings.json'):
        with open(configuration_file) as json_data:
            data = json.load(json_data)

        print('Configuration: ', data)

        self.face_swap: str = data['ApiUrl']['FaceSwapApi']
        self.stable_diffusion: str = data['ApiUrl']['StableDiffusionApi']
