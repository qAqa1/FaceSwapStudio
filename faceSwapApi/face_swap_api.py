import uvicorn
from fastapi import FastAPI, Depends, Request, HTTPException
import core.face_swap_core as face_swap
from core import converter

app = FastAPI()


# http://127.0.0.1:8000/get_image
# @app.post("/copy_image")
# async def copy_image(request: Request):
#     message = await request.json()
#     # print('result:')
#     # print(type(msg))
#     # print(msg)
#
#     received_image = converter.base64_to_cvimage(message['image'])
#     # cv2.imwrite("test.png", received_image)
#
#     # received_image[:] = (255, 0, 0)
#
#     base64_result_image = converter.cvimage_to_base64(received_image)
#     # print(base64_result_image)
#
#     result = {
#         "image": base64_result_image
#     }
#
#     return result


class FaceRectangle:
    def __init__(self, detection_array):
        self.x1 = int(detection_array[0])
        self.y1 = int(detection_array[1])
        self.x2 = int(detection_array[2])
        self.y2 = int(detection_array[3])

    def pack_to_dict(self):
        return {
            'x1': self.x1,
            'y1': self.y1,
            'x2': self.x2,
            'y2': self.y2,
        }

    def __str__(self):
        return '{ x1 = ' + str(self.x1) + ', y1 = ' + str(self.y1) + ', x2 = ' + str(self.x2) + ', y2 = ' + str(self.y2) + ' }'

    x1 = 0
    y1 = 0
    x2 = 0
    y2 = 0


def detect_faces_handler(json_dict):
    required_data = {'image'}

    for val in required_data:
        if val not in json_dict:
            return face_swap.FaceProcessingStatus.NO_REQUIRED_ARGS_ERROR, None

    for val in required_data:
        if json_dict[val] == '':
            return face_swap.FaceProcessingStatus.EMPTY_ARG_ERROR, None

    try:
        image = converter.base64_to_cvimage(json_dict['image'])
    except ValueError:
        return face_swap.FaceProcessingStatus.BAD_IMAGE_ERROR, None

    detected_faces = face_swap.detect_faces(image)
    detected_faces_rectangles = face_swap.get_faces_rectangles(detected_faces)
    print('Detected faces:')
    for index, face_box in enumerate(detected_faces_rectangles):
        print(str(index) + ')', face_box)

    if len(detected_faces) == 0:
        return face_swap.FaceProcessingStatus.IMAGE_NO_FACE_ERROR, None

    detected_faces_rectangles_formated = [FaceRectangle(rectangle) for rectangle in detected_faces_rectangles]

    return face_swap.FaceProcessingStatus.SUCCESS, detected_faces_rectangles_formated
    # return face_swap.FaceProcessingStatus.SUCCESS, detected_faces_rectangles


# http://127.0.0.1:8000/detect_faces
@app.post("/detect_faces")
async def detect_faces(request: Request):
    message = await request.json()

    status, detected_faces_rectangles = detect_faces_handler(message)

    if status != face_swap.FaceProcessingStatus.SUCCESS:
        return {
            "status": status.value
        }

    result_dict = {
        'status': status.value,
        'detectedFacesRectangles': []
        # 'detected_faces_rectangles': detected_faces_rectangles
    }

    [result_dict['detectedFacesRectangles'].append(rectangle.pack_to_dict()) for rectangle in detected_faces_rectangles]

    return result_dict


def swap_face_handler(json_dict):
    required_data = {'body', 'face', 'targetFaceIndex'}

    for val in required_data:
        if val not in json_dict:
            return face_swap.FaceProcessingStatus.NO_REQUIRED_ARGS_ERROR, None

    for val in required_data:
        if json_dict[val] == '':
            return face_swap.FaceProcessingStatus.EMPTY_ARG_ERROR, None

    try:
        body_image = converter.base64_to_cvimage(json_dict['body'])
        face_image = converter.base64_to_cvimage(json_dict['face'])
    except ValueError:
        return face_swap.FaceProcessingStatus.BAD_IMAGE_ERROR, None

    try:
        target_face_index = int(json_dict['targetFaceIndex'])
    except ValueError:
        return face_swap.FaceProcessingStatus.TARGET_IMAGE_FACE_INDEX_NAN_ERROR, None

    try:
        status, result = face_swap.swap_face_by_images(body_image, face_image, target_face_index)
    except:
        return face_swap.FaceProcessingStatus.UNEXPECTED_ERROR, None

    # print('status =', status)
    return status, result


# http://127.0.0.1:8000/swap_face
@app.post("/swap_face")
async def swap_face(request: Request):
    message = await request.json()

    status, image = swap_face_handler(message)

    if status != face_swap.FaceProcessingStatus.SUCCESS:
        return {
            "status": status.value
        }

    base64_result_image = converter.cvimage_to_base64(image)
    # print(base64_result_image)

    print('status =', status)
    return {
        "status": status.value,
        "image": base64_result_image
    }


if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
