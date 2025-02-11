import cv2
from api import face_processing
from api import configuration
from core import converter
from api.face_swap_core import FaceProcessingStatus

body = cv2.imread("../faceSwapApi/test_images/body/body1.png")
face = cv2.imread("../faceSwapApi/test_images/face/face3.jpg")

api_urls = configuration.ApiURLs()

face_processing_request = face_processing.FaceProcessingRequestModel(converter.cvimage_to_base64(body), converter.cvimage_to_base64(face))
face_processing_result = face_processing.process_faces(api_urls, face_processing_request)

print(face_processing_result.status)

if face_processing_result.status is FaceProcessingStatus.SUCCESS:
    cv2.imshow("Swap result", converter.base64_to_cvimage(face_processing_result.image))

cv2.waitKey()
