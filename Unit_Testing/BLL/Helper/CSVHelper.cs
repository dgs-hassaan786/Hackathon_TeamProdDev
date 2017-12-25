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
   
    public interface IReadCSV 
    {
        bool CheckHeader(string path, string[] headers);
        DataTable ExtractCSVData(string path, string[] headersToProcessed);
    }

    public class CSVHelper : IWriteCSV, IReadCSV
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

        public  DataTable ExtractCSVData(string path, string[] headersToProcessed)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    
                    string[] colFields = csvReader.ReadFields();                                     
                    var indexesToProceed = new int[headersToProcessed.Length];
                    var indexCounter = 0;
                    
                    for (int i = 0; i < headersToProcessed.Length; i++)
                    {                        
                        for (int j = 0; j < colFields.Length; j++)
                        {
                            if (colFields[j] == headersToProcessed[i])
                            {
                                DataColumn datcolumn = new DataColumn(colFields[j]);
                                datcolumn.AllowDBNull = true;
                                csvData.Columns.Add(datcolumn);
                                indexesToProceed[indexCounter++] = j;
                                break;
                            }
                        }                 
                    }


                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        string[] filtered_data = new string[indexesToProceed.Length];


                        for (int i = 0; i < indexesToProceed.Length; i++)
                        {
                            filtered_data[i] = fieldData[indexesToProceed[i]];
                        }
                                                                     
                        csvData.Rows.Add(filtered_data);
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
