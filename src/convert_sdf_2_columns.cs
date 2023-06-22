using System.Data.SqlServerCe;
using System;
using System.Data;
using System.Text;
using System.IO;

public class Convert
{
    public static void Main(string[] args)
    {
        //Initialise a connection
        SqlCeConnection connection;

        //Initialise readers
        SqlCeDataReader reader_parameter;

        SqlCeDataAdapter adapter;

        //Initialise data tables and string builders
        DataTable datatable_tableNames = new DataTable();

        DataTable datatable_tableParameters = new DataTable();

        StringBuilder sb_tableParameter = new StringBuilder();

        //Find .sdf files
        string[] files = System.IO.Directory.GetFiles(
            "C:\\windows\\Microsoft.NET\\Framework64\\v4.0.30319\\",
            "*.sdf"
        );

        foreach (string file in files)
        {
            string connectionString = "Data Source =" + file + ";";

            Console.WriteLine(connectionString);

            connection = new SqlCeConnection(connectionString);

            //Open a connection and check the state
            connection.Open();

            Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);

            Console.WriteLine("State: {0}", connection.State);

            //Initialising commandTableNames and adapter
            SqlCeCommand commandTableNames = connection.CreateCommand();

            SqlCeCommand commandTableContent = connection.CreateCommand();

            SqlCeCommand commandTableParameters = connection.CreateCommand();

            //Simple query to get table names
            commandTableNames.CommandText =
                "select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'";

            adapter = new SqlCeDataAdapter(commandTableNames);

            adapter.Fill(datatable_tableNames);

            connection.Close();

            foreach (DataRow dataRow_tableName in datatable_tableNames.Rows)
            {
                foreach (var tableName in dataRow_tableName.ItemArray)
                {
                    var outputFileName =
                        @"C:\windows\Microsoft.NET\Framework64\v4.0.30319\Output_1\"
                        + tableName
                        + ".columns";

                    var fileStream = File.Create(outputFileName);

                    fileStream.Close();

                    using (StreamWriter writetext = new StreamWriter(outputFileName))
                    {
                        connection.Open();

                        commandTableParameters.CommandText =
                            "SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @p1 ORDER BY ORDINAL_POSITION";

                        commandTableParameters.Parameters.Clear();

                        commandTableParameters.Parameters.AddWithValue("@p1", tableName);

                        reader_parameter = commandTableParameters.ExecuteReader();

                        writetext.Write(
                            "\"COLUMN_NAME\",\"DATA_TYPE\",\"CHARACTER_MAXIMUM_LENGTH\",\"NUMERIC_PRECISION\",\"NUMERIC_SCALE\""
                        );

                        while (reader_parameter.Read())
                        {
                            writetext.Write("\n");

                            for (int i = 0; i < reader_parameter.FieldCount; i++)
                            {                                
                                var parameter = reader_parameter.GetValue(i);
                                sb_tableParameter.Append("\"");
                                sb_tableParameter.Append(parameter);
                                sb_tableParameter.Append("\"");
                                sb_tableParameter.Append(",");
                            }

                            sb_tableParameter.Length--;

                            writetext.Write(sb_tableParameter);

                            sb_tableParameter.Length = 0;
                        }
                    }

                    connection.Close();
                }
            }
        }
    }
}
