# clearing .sdf files
rm /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/*.sdf || true && \
# clearing .columns files
rm /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/Output_1/*.columns  || true && \
# clearing .csv files
rm /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/Output/*.csv || true && \
# clearing location temp output folder
rm /home/ubuntu/src/Output/*.csv || true && rm /home/ubuntu/src/Output/*.columns || true && \
# clear zip files
rm /home/ubuntu/src/*.zip || true