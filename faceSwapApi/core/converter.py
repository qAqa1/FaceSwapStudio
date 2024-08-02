import base64
import io
import cv2
import imageio


def base64_to_cvimage(base64str):
    received_image = imageio.v2.imread(io.BytesIO(base64.b64decode(base64str)))
    # received_image = imageio.v2.imread(io.BytesIO(base64.b64decode(base64.decodebytes(base64str.split(",")[1]))))
    # return cv2.cvtColor(received_image, cv2.COLOR_RGBA2BGRA)
    return cv2.cvtColor(received_image, cv2.COLOR_RGB2BGR)


def cvimage_to_base64(cvimage):
    _, encoded_img = cv2.imencode('.png', cvimage)  # Works for '.jpg' as well
    return base64.b64encode(encoded_img).decode("utf-8")


# def file_to_base64(path):
#     with open(path, 'rb') as file:
#         return base64.b64encode(file.read()).decode('utf-8')
#
#
# def base64_to_file(base64_str, save_path):
#     with open(save_path, "wb") as file:
#         file.write(base64.b64decode(base64_str))
