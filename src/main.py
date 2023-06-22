from fastapi import FastAPI, UploadFile, BackgroundTasks
from fastapi.responses import FileResponse
import shutil
import os

app = FastAPI()

def zipOutput(name):
    shutil.make_archive(name, 'zip', '/home/ubuntu/src/Output')
    return()

def runSdf2Csv():
    print("Running: SDF to CSV")
    os.system('./convertSdf.sh')
    return()

def cleanUp():
    print("Running: Clean Up")
    os.system('./clearOutputFolders.sh')
    return

def sdfConversion(name):
    print("Conversion Start")
    runSdf2Csv()
    zipOutput(name)
    print("Conversion End")
    return()

# main route
@app.get("/")
async def root():
    return {"message": "Ready"}

# route for file upload
@app.post("/convert/")
async def create_upload_file(file: UploadFile):

    # clean up
    cleanUp()

    # write file to disk
    destination = "files/" + file.filename
    with open(destination, "wb") as buffer:
        shutil.copyfileobj(file.file, buffer)
    file.file.close()

    # run conversion
    filename = os.path.splitext(file.filename)[0]
    sdfConversion(filename)
    return FileResponse(filename + ".zip")