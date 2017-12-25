using BLL.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helper
{

    public interface IWriteCSV
    {
        bool Write(string path, IEnumerable<Employee> employee);
    }

    public class CSVHelper : IWriteCSV
    {

        public bool Write(string path, IEnumerable<Employee> employees)
        {
            var status = true;

            bool exists = Directory.Exists(path);
            if (!exists)
            {
                Directory.CreateDirectory(path);
            }
            if (!path.EndsWith(@"\"))
                path += @"\";


            string filePath = path + "output.csv";
            if (!string.IsNullOrEmpty(filePath))
            {
                using (StreamWriter w = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
                {
                    var header = string.Format($"{nameof(Employee.EmployeeId)},{nameof(Employee.EmployeeId)},{nameof(Employee.EmployeeId)}");
                    w.WriteLine(header);
                    w.Flush();
                    foreach (var e in employees)
                    {
                        var line = string.Format($"{e.EmployeeId},{e.Name},{e.HoursWorked}");
                        w.WriteLine(line);
                        w.Flush();
                    }
                }
            }
            
            return status;
        }


        //public Task Read(string path, IEnumerable<Employee> employee)
        //{
        //    bool IsHeader = true;
        //    DataTable dt = new DataTable();
        //    List<string[]> parsedData = new List<string[]>();
        //    using (var parser = new TextFieldParser(path))
        //    {
        //        parser.HasFieldsEnclosedInQuotes = true;
        //        parser.SetDelimiters(",");
        //        parser.TrimWhiteSpace = true;
        //        parser.TextFieldType = FieldType.Delimited;
        //        while (!parser.EndOfData)
        //        {
        //            // get the column headers
        //            if (IsHeader)
        //            {
        //                IsHeader = false;
        //                continue;
        //            }
        //            //Processing row
        //            string[] fields = parser.ReadFields();
        //            parsedData.Add(fields);
        //        }
        //    }
        //    return Task.FromResult(0);
        //}
        public static DataTable ParseCSVFile(string path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names from the first row
                    string[] colFields = csvReader.ReadFields();
                    //iterate each column to create the DataColumn for the DataTable structure
                    foreach (string column in colFields)
                    {
                        DataColumn datcolumn = new DataColumn(column);
                        datcolumn.AllowDBNull = true;
                        csvData.Columns.Add(datcolumn);
                    }//end foreach
                     //now on to the data
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                                fieldData[i] = null;
                        }//end for
                         //add the DataRow
                        csvData.Rows.Add(fieldData);
                    }//end while
                }//end using
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return csvData;
        }
    }
}
