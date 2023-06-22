# Change permisions of .sdf files
chmod o+w /home/ubuntu/src/files/* || true && \
# Move the files to the location of the C# compiler
mv /home/ubuntu/src/files/* /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/ || true && \
# command below runs an executable after compiled with wine and the C# compiler: wine /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/csc.exe convert_sdf_2_csv.cs -r:"C:\windows\Microsoft.NET\Framework64\v4.0.30319\System.Data.SqlServerCe.dll"
wine /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/convert_sdf_2_csv.exe || true && \
# The command below runs an executable compiled with wine and the C# compiler: wine /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/csc.exe convert_sdf_2_columns.cs -r:"C:\windows\Microsoft.NET\Framework64\v4.0.30319\System.Data.SqlServerCe.dll"
wine /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/convert_sdf_2_columns.exe || true && \
# moving files to the Output folder
mv /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/Output_1/*.columns  /home/ubuntu/src/Output/ || true && \
mv /home/ubuntu/.wine/drive_c/windows/Microsoft.NET/Framework64/v4.0.30319/Output/*.csv /home/ubuntu/src/Output/ || true && \
# change permision of zip files
chmod o+w /home/ubuntu/src/Output/* || true