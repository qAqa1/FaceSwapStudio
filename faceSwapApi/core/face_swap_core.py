import cv2
import os.path
import datetime
# import numpy as np
import insightface
from insightface.app import FaceAnalysis
from sys import platform
# import matplotlib.pyplot as plt
# from insightface.data import get_image

print("Load models:")

app = FaceAnalysis(name='buffalo_l')
app.prepare(ctx_id=0, det_size=(640, 640))

swapper = insightface.model_zoo.get_model('core/inswapper_128.onnx',
                                          download=False,
                                          download_zip=False)


def detect_faces(cv_image):
    return app.get(cv_image)


def get_faces_rectangles(faces):
    face_boxes = list()
    [face_boxes.append(face['bbox']) for face in faces]
    return face_boxes


def swap_face(body_image, source_face, target_face):
    image = body_image.copy()
    image = swapper.get(body_image, source_face, target_face, paste_back=True)
    return image


def swap_face_by_images(cv_image_source_body, cv_image_target_face, source_body_face_index = 0):
    image = cv_image_source_body.copy()

    source_faces = detect_faces(cv_image_source_body)

    if len(source_faces) == 0:
        raise ValueError("Source image don't contains any face")

    if len(source_faces) - 1 < source_body_face_index:
        raise IndexError("Face index out of range")
        # raise ValueError("Face index out of range")

    target_faces = detect_faces(cv_image_target_face)

    if len(target_faces) == 0:
        raise ValueError("Target face-image don't contains any face")

    return swap_face(cv_image_source_body, source_faces[source_body_face_index], target_faces[0])
