using BLL.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTester.Extensions;

namespace UnitTester.IntegrationTesting
{
    [TestClass]
    public class FileIntegrationTester
    {

        [TestMethod]
        public void EmployeeImportFile_FileExists_SuccessTest()
        {
            
        }

        [TestMethod]
        public void EmployeeImportFile_FileExists_FailTest()
        {

        }

        [TestMethod]
        public void EmployeeImportFile_HeaderExist_SuccessTest()
        {
            IReadCSV readCSV = new CSVHelper();
            var actual = readCSV.CheckHeader(@"D:\Hackathon\instructions_to_start_the_hackathon_coding\first file.csv", IntegrationMockProvider.MockHeaders());
            Assert.AreEqual(true, actual);

        }

        [TestMethod]
        [CustomExpectedException(typeof(Exception))]
        public void EmployeeImportFile_HeaderExist_Exception()
        {
            IReadCSV readCSV = new CSVHelper();
            var actual = readCSV.CheckHeader(@"D:\Hackathon\instructions_to_start_the_hackathon_coding\sample_input.csv", IntegrationMockProvider.MockHeaders());            
        }

        [TestMethod]        
        public void EmployeeImportFile_DataFormation_Datatable()
        {
            IReadCSV readCSV = new CSVHelper();
            var actual = readCSV.ExtractCSVData(@"D:\Hackathon\instructions_to_start_the_hackathon_coding\third file.csv", IntegrationMockProvider.MockHeaders());
        }

    }

    public class IntegrationMockProvider
    {
        public static string[] MockHeaders()
        {
            return new string[3] { "EmployeeID", "Name", "HoursWorked" };
        }
    }
}
