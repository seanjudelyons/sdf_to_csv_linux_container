### Summary

The SQLCE `.sdf` files are a Microsoft proprietary filetype.  This repo describes how to write a Windows file system inside a Linux Docker container for `.sdf` to `.csv` conversion.  This is useful for training and deploying newer multi-class classification models such as YOLO v5 with older video recording software.


### Structure 

`src` contains the installation guide for `.sdf` to `.csv` conversion.

### Requirement

CPU - `x86_64`

### Container Example

After following the steps in the `src` folder, the container can be run with the command below.

`docker run -it --user ubuntu --publish="8000:8000" --name="remote-desktop" seanjudelyons/windows_container:sdf_api sh -c "cd /home/ubuntu/src/ && uvicorn main:app --host 0.0.0.0”`

### Test

```python
# api test
import requests
url = 'http://0.0.0.0:8000/'
r = requests.get(url)
r.text
------------------------------------------------------
'{"message":"Ready"}'
```

### Usage

```python
# convert .sdf to .csv
import requests, zipfile, io
url = 'http://0.0.0.0:8000/convert/'
files = {'file': open('Test.sdf', 'rb')}
r = requests.post(url, files=files)
z = zipfile.ZipFile(io.BytesIO(r.content))
z.extractall('./output/')
------------------------------------------------------
```

### Dataframe Test

```python
# .csv output test
import pandas as pd
df = pd.read_csv('./output/JOB.csv')
print(df.to_markdown())
------------------------------------------------------
```

