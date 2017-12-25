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
    public interface IFileValidation
    {
        bool Exists(string path);
    }
    public class FileValidation : IFileValidation
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
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
       

        public bool CheckHeader(string path,  string[] headers)
        {            
            using (TextFieldParser csvReader = new TextFieldParser(path))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                string[] colFields = csvReader.ReadFields();

                for (int i = 0; i < headers.Length; i++)
                {
                    var status = false;
                    for (int j = 0; j < colFields.Length; j++)
                    {
                        if(colFields[j]== headers[i])
                        {
                            status = true;
                            break;
                        }
                    }

                    if (!status)
                        throw new Exception($"File header {headers[i]} not found");
                }
                               
            }

            return true;
        }


        public static DataTable ParseCSVFile(string path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    
                    string[] colFields = csvReader.ReadFields();
                    
                    foreach (string column in colFields)
                    {
                        DataColumn datcolumn = new DataColumn(column);
                        datcolumn.AllowDBNull = true;
                        csvData.Columns.Add(datcolumn);
                    }
                     
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                                fieldData[i] = null;
                        }
                         
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return csvData;
        }
    }
}
