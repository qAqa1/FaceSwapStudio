import face_swap_api
from core import converter
import cv2
import json


def call_detect_face():
    image = cv2.imread("test_images/body/body1.png")

    json_dict = {
        "image": converter.cvimage_to_base64(image),
    }

    status, detected_faces_rectangles = face_swap_api.detect_face_handler(json_dict)

    print('status =', status)
    print('Detected faces:')
    for index, face_box in enumerate(detected_faces_rectangles):
        print(str(index) + ')', face_box)

    test_dict = {
        'status': status.name,
        'detected_faces_rectangles': []
        # 'detected_faces_rectangles': detected_faces_rectangles
    }

    [test_dict['detected_faces_rectangles'].append(rectangle.pack_to_dict()) for rectangle in detected_faces_rectangles]

    test_json = json.dumps(test_dict)

    print(test_json)


def call_swap_face():
    cv_bodies = cv2.imread("test_images/body/body1.png")
    # cv_bodies = cv2.imread("test_images/no_face_image.jpg")

    cv_face1 = cv2.imread("test_images/face/face1.jpg")
    # cv_face1 = cv2.imread("test_images/no_face_image.jpg")

    json_dict = {
        "body": converter.cvimage_to_base64(cv_bodies),
        "face": converter.cvimage_to_base64(cv_face1),
        "target_face_index": '1',
    }

    status, image = face_swap_api.swap_face_handler(json_dict)

    print('status =', status)
    print('image =', image)

    # cv2.imshow('Result image', image)
    # cv2.waitKey()


call_detect_face()
# call_swap_face()
