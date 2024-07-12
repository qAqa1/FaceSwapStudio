import uvicorn
# from fastapi import FastAPI

from fastapi import FastAPI, Depends, Request, HTTPException
from starlette.datastructures import FormData

import core.face_swap_core as face_swap

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
@app.post("/get_image")
async def get_image(request: Request):
    msg = await request.json()
    print('result:')
    print(type(msg))
    print(msg)

    received_image = imageio.v2.imread(io.BytesIO(base64.b64decode(msg['image'])))
    cv2_img = cv2.cvtColor(received_image, cv2.COLOR_RGB2BGR)
    cv2.imwrite("test.png", cv2_img)

    # # received_image = imageio.v2.imread(io.BytesIO(base64.b64decode(image)))
    # # cv2_img = cv2.cvtColor(received_image, cv2.COLOR_RGB2BGR)
    # # cv2.imwrite("test.png", cv2_img)
    # items = body.getlist('items')
    # files = body.getlist('files')  # returns a list of UploadFile objects
    # if files:
    #     for file in files:
    #         print(f'Filename: {file.filename}. Content (first 15 bytes): {await file.read(15)}')
    return 'OK'
    # # received_image = imageio.v2.imread(io.BytesIO(base64.b64decode(image)))
    # # cv2_img = cv2.cvtColor(received_image, cv2.COLOR_RGB2BGR)
    # # cv2.imwrite("test.png", cv2_img)
    # print(image)
    # return {'image received'}


if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
