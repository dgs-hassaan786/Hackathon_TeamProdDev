using BLL.Helper;
using BLL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IEmployeeHourCalculator
    {
        bool CalculateEmployeeHoursWorked();
    }

    public class EmployeeHourCalculator : IEmployeeHourCalculator
    {

        private IReadCSV _readCSV;
        private IWriteCSV _writeCSV;
        private IEmployeeDataProcessor _employeeDataProcessor;
        private IFileValidationProvider _fileValidation;

        public EmployeeHourCalculator(IFileValidationProvider fileValidation, IReadCSV readCSV, IWriteCSV writeCSV, IEmployeeDataProcessor employeeDataProcessor)
        {
            _fileValidation = fileValidation;
            _readCSV = readCSV;
            _writeCSV = writeCSV;
            _employeeDataProcessor = employeeDataProcessor;
        }

        public bool CalculateEmployeeHoursWorked()
        {
            //step 1 check files exits?
            //If not exits then this will generate exception
            var paths = new string[3] { @"D:\Hackathon\instructions_to_start_the_hackathon_coding\first file.csv", @"D:\Hackathon\instructions_to_start_the_hackathon_coding\second file.csv", @"D:\Hackathon\instructions_to_start_the_hackathon_coding\third file.csv" };
            var headers = new string[3] { "EmployeeID", "Name", "HoursWorked" };
            var tasks = new List<Task<DataTable>>();
            var listOfDataTable = new List<DataTable>();
            for (int i = 0; i < paths.Length; i++)
            {
                if (_fileValidation.Exists(paths[i]))
                {
                    try
                    {
                        //step 2 check header validation
                        if (_readCSV.CheckHeader(paths[i], headers))
                        {
                            //step 3 transformation of data
                            var task = Task.Factory.StartNew<DataTable>((e) =>
                            {
                                var o = (object[])e;
                               return _readCSV.ExtractCSVData(o[0].ToString(), (string[])o[1]);
                            }, new object[] { paths[i], headers });
                            tasks.Add(task);
                            
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                }
                else
                {
                    throw new Exception($"File not exist at path {paths[i]}");
                }
            }

            Task.WaitAll(tasks.ToArray());
            foreach(var tt in tasks)
            listOfDataTable.Add(tt.Result);

            //Now Step 4 Transformation of Data
            // step 4.1 Merging of data sources
            var singleDataSource = _employeeDataProcessor.MergeDataSource(listOfDataTable);

            //step 4.2 Transforming Datatable to list for processing
            var unprocessedEmployees = _employeeDataProcessor.FormattingData(singleDataSource);

            //step 4.3 Finalizing the list
            var finalizedList = _employeeDataProcessor.ProcessData(unprocessedEmployees);

            //Now Final Step 5 is to Write on the File
            //async call
            var t = Task.Factory.StartNew((e)=> {

                var o = (object[])e;
                _writeCSV.Write(o[0].ToString(), (List<Employee>)o[1]);

            }, new object[] { @"D:\Hackathon\OutPut", finalizedList } );

            t.Wait();

            return true;
        }
    }
}
