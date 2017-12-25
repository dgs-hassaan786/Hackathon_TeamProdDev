using BLL.Helper;
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
                            listOfDataTable.Add(_readCSV.ExtractCSVData(paths[i], headers));
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

            //Now Step 4 Transformation of Data
            // step 4.1 Merging of data sources
            var singleDataSource = _employeeDataProcessor.MergeDataSource(listOfDataTable);

            //step 4.2 Transforming Datatable to list for processing
            var unprocessedEmployees = _employeeDataProcessor.FormattingData(singleDataSource);

            //step 4.3 Finalizing the list
            var finalizedList = _employeeDataProcessor.ProcessData(unprocessedEmployees);

            //Now Final Step 5 is to Write on the File
            _writeCSV.Write(@"D:\Hackathon\OutPut", finalizedList);

            return true;
        }
    }
}
