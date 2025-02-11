import base64
import io
import cv2
import imageio


def base64_to_cvimage(base64str):
    received_image = imageio.v2.imread(io.BytesIO(base64.b64decode(base64str)))
    return cv2.cvtColor(received_image, cv2.COLOR_RGB2BGR)


def cvimage_to_base64(cvimage):
    _, encoded_img = cv2.imencode('.png', cvimage)  # Works for '.jpg' as well
    return base64.b64encode(encoded_img).decode("utf-8")
