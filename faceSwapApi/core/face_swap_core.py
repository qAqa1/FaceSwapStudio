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
    faces = app.get(cv_image)
    face_boxes = list()
    [face_boxes.append(face['bbox']) for face in faces]
    return face_boxes
