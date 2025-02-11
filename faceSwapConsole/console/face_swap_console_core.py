import cv2
import os.path
from datetime import datetime
from sys import platform
from api import face_processing
from core import converter
from api.face_swap_core import FaceProcessingStatus
from api import configuration


api_urls = configuration.ApiURLs()

def face_swap(output_dir, face_filename, body_filename):
    if not os.path.isdir(output_dir):
        print("output directory doesn't exist")
        exit(1)

    if not os.path.isfile(body_filename):
        print("body path doesn't exist")
        exit(1)

    if not os.path.isfile(face_filename):
        print("face path doesn't exist")
        exit(1)


    face = cv2.imread(face_filename)
    body = cv2.imread(body_filename)

    face_processing_request = face_processing.FaceProcessingRequestModel(converter.cvimage_to_base64(body),
                                                                         converter.cvimage_to_base64(face))
    start_time = datetime.now()
    print("start time: {}".format(start_time))
    face_processing_result = face_processing.process_faces(api_urls, face_processing_request)
    end_time = datetime.now()
    if face_processing_result.status is FaceProcessingStatus.SUCCESS:
        output_path = os.path.join(output_dir, datetime.now().strftime("%Y-%m-%d %H-%M-%S%z.png"))
        print('end time: ' + str(end_time) + ', calculation time:', str(end_time - start_time) + '\noutput path:', output_path)
        cv2.imwrite(output_path, converter.base64_to_cvimage(face_processing_result.image))

        if platform == "darwin":
            print('You can open preview for output file by this command:')
            print('qlmanage -p ' + output_path.replace(' ', '\ '))

        return

    print("Face processing error, error status is " + str(face_processing_result.status))
