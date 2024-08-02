import insightface
from insightface.app import FaceAnalysis
from enum import IntEnum


print("Load models:")

app = FaceAnalysis(name='buffalo_l')
app.prepare(ctx_id=0, det_size=(640, 640))

# https://huggingface.co/bes-dev/roop/blob/main/inswapper_128.onnx
swapper = insightface.model_zoo.get_model('core/inswapper_128.onnx',
                                          download=False,
                                          download_zip=False)


class FaceProcessingStatus(IntEnum):
    UNKNOWN_ERROR = 0
    SUCCESS = 1
    UNEXPECTED_ERROR = 2
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
    image = cv_image_source_body
    source_faces = detect_faces(image)

    if len(source_faces) == 0:
        return FaceProcessingStatus.SOURCE_IMAGE_NO_FACE_ERROR, None

    if len(source_faces) - 1 < source_body_face_index:
        return FaceProcessingStatus.TARGET_IMAGE_FACE_INDEX_OUT_OF_RANGE_ERROR, None

    target_faces = detect_faces(cv_image_target_face)

    if len(target_faces) == 0:
        return FaceProcessingStatus.TARGET_IMAGE_NO_FACE_ERROR, None

    return FaceProcessingStatus.SUCCESS, swap_face(image, source_faces[source_body_face_index], target_faces[0])
