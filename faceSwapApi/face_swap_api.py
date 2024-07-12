import uvicorn
import core.converter
# from fastapi import FastAPI

from fastapi import FastAPI, Depends, Request, HTTPException
from starlette.datastructures import FormData

import core.face_swap_core as face_swap
from core import converter

app = FastAPI()


# http://127.0.0.1:8000/get-message

@app.get("/get-message")
async def hello(name: str):
    return {'Message': "Congrats " + name + '! This is your first API!'}


# http://127.0.0.1:8000/get

@app.get("/get")
async def hello():
    return {"Hello": "World"}


# @app.get("/swap-face")
# async def swap_face(base64image_source_body: str, base64image_target_face: str, source_body_face_index: str):
#     return { base64image }

import base64
import io
import cv2
import imageio


async def get_body(request: Request):
    content_type = request.headers.get('Content-Type')
    if content_type is None:
        raise HTTPException(status_code=400, detail='No Content-Type provided!')
    elif (content_type == 'application/x-www-form-urlencoded' or
          content_type.startswith('multipart/form-data')):
        try:
            return await request.form()
        except Exception:
            raise HTTPException(status_code=400, detail='Invalid Form data')
    else:
        raise HTTPException(status_code=400, detail='Content-Type not supported!')


# http://127.0.0.1:8000/get_image
# http://127.0.0.1:8000/get_image?image=Testt
@app.post("/copy_image")
async def copy_image(request: Request):
    msg = await request.json()
    # print('result:')
    # print(type(msg))
    # print(msg)

    received_image = converter.base64_to_cvimage(msg['image'])
    # cv2.imwrite("test.png", received_image)

    # received_image[:] = (255, 0, 0)

    base64_result_image = converter.cvimage_to_base64(received_image)
    print(base64_result_image)

    result = {
        "image": base64_result_image
    }

    return result


if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
