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
        Task Write(string outputPath, IEnumerable<Employee> employee);
    }

    class CSVHelper : IWriteCSV
    {

        public Task Write(string outputPath, IEnumerable<Employee> employee)
        {
            using (var fileStream = File.OpenWrite(outputPath))
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                var result = false;
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        var writer = new BinaryWriter(ms);
                        writer.Write(employee.);
                        var binaryRecord = ms.ToArray();


                        result = true;
                    }
                }
                catch (Exception e)
                {
                    //log exception here
                    //_Logger.Error(e, $"Exception generated in method: {nameof(AdwordsSnapshotWriter)}.{nameof(Write)}");
                }
                //  return result;

                return Task.FromResult(0);
            }
        }


        internal bool Write(IEnumerable<Employee> employee)
        {
            var result = false;
            //try
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        var writer = new BinaryWriter(ms);
            //        writer.Write(adwordsPull.Data);
            //        var binaryRecord = ms.ToArray();

            //        DatabaseManager.AddAdwordsPullData(adwordsPull.TrendDate, adwordsPull.Hour, adwordsPull.Minute, binaryRecord).Forget();
            //        result = true;
            //    }
            //}
            //catch (Exception e)
            //{
            //    //log exception here
            //    _Logger.Error(e, $"Exception generated in method: {nameof(AdwordsSnapshotWriter)}.{nameof(Write)}");
            //}
            return result;
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
