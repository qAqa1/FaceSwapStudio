import insightface
from insightface.app import FaceAnalysis
from enum import Enum


print("Load models:")

app = FaceAnalysis(name='buffalo_l')
app.prepare(ctx_id=0, det_size=(640, 640))

swapper = insightface.model_zoo.get_model('core/inswapper_128.onnx',
                                          download=False,
                                          download_zip=False)


class FaceProcessingStatus(Enum):
    UNKNOWN = 0
    UNEXPECTED_ERROR = 1
    SUCCESS = 2
    SOURCE_IMAGE_NO_FACE_ERROR = 3
    TARGET_IMAGE_NO_FACE_ERROR = 4
    IMAGE_NO_FACE_ERROR = 5
    TARGET_IMAGE_FACE_INDEX_OUT_OF_RANGE_ERROR = 6
    TARGET_IMAGE_FACE_INDEX_NAN_ERROR = 7,
    NO_REQUIRED_ARGS_ERROR = 8,
    EMPTY_ARG_ERROR = 9,
    BAD_IMAGE_ERROR = 10,


def detect_faces(cv_image):
    return app.get(cv_image)


def get_faces_rectangles(faces):
    face_boxes = list()
    [face_boxes.append(face['bbox']) for face in faces]
    return face_boxes


def swap_face(body_image, source_face, target_face):
    image = swapper.get(body_image.copy(), source_face, target_face, paste_back=True)
    return image


def swap_face_by_images(cv_image_source_body, cv_image_target_face, source_body_face_index=0):
    # cv_image_source_body = cv2.imread("test_images/body/body1.png")
    # cv_image_target_face = cv2.imread("test_images/face/face1.jpg")

    # cv2.imshow('Face image', cv_image_target_face)
    # cv2.imshow('Body image', cv_image_source_body)
    # cv2.waitKey()

    # print('target face data type', cv_image_target_face.dtype)
    # print('source body data type', cv_image_source_body.dtype)
    #
    # print('target face shape', cv_image_target_face.shape)
    # print('source body shape', cv_image_source_body.shape)
    #
    # print('target face channels count', len(cv_image_target_face.shape))
    # print('source body channels count', len(cv_image_source_body.shape))
    #
    # temp_face = cv2.cvtColor(cv_image_target_face, cv2.COLOR_BGRA2BGR)
    # temp_body = cv2.cvtColor(cv_image_source_body, cv2.COLOR_BGRA2BGR)
    #
    # print('temp face shape', temp_face.shape)
    # print('temp face channels count', len(temp_face.shape))
    #
    # print('temp body shape', temp_body.shape)
    # print('temp body channels count', len(temp_body.shape))

    # image = cv_image_source_body.copy()
    image = cv_image_source_body
    # cv2.imshow('Body image', image)
    # cv2.waitKey()
    source_faces = detect_faces(image)

    if len(source_faces) == 0:
        return FaceProcessingStatus.SOURCE_IMAGE_NO_FACE_ERROR, None
        # raise ValueError("Source image don't contain any face")

    if len(source_faces) - 1 < source_body_face_index:
        return FaceProcessingStatus.TARGET_IMAGE_FACE_INDEX_OUT_OF_RANGE_ERROR, None
        # raise IndexError("Face index out of range")
        # raise ValueError("Face index out of range")

    target_faces = detect_faces(cv_image_target_face)

    if len(target_faces) == 0:
        return FaceProcessingStatus.TARGET_IMAGE_NO_FACE_ERROR, None
        # raise ValueError("Target face-image don't contain any face")

    return FaceProcessingStatus.SUCCESS, swap_face(image, source_faces[source_body_face_index], target_faces[0])
