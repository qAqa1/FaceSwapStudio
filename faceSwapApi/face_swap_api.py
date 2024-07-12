import uvicorn
from fastapi import FastAPI

app = FastAPI()

# http://127.0.0.1:8000/get-message

@app.get("/get-message")
async def hello(name: str):
    return {'Message': "Congrats " + name + '! This is your first API!'}

# http://127.0.0.1:8000/get

@app.get("/get")
async def hello():
    return {"Hello": "World"}

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)
