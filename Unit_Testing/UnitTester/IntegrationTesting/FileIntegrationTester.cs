﻿using BLL.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTester.Extensions;

using BLL.Helper;
namespace UnitTester.IntegrationTesting
{
    [TestClass]
    public class FileIntegrationTester
    {
        public IFileValidation _FileValidation;
        public FileIntegrationTester()
        {
            _FileValidation = new FileValidation();
        }
        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile1_FileExists_SuccessTest()
        {
            //Arrange
            var path = @"C:\Users\ghazanfer.khan\Desktop\first file.csv";
            var actual = true;
            //Act
            var expected = _FileValidation.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile1_FileExists_FailTest()
        {
            //Arrange
            var path = @"C:\Users\ghazanfer.khan\Desktop\first.csv";
            var actual = true;
            //Act
            var expected = _FileValidation.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual,"File not exist");
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile2_FileExists_SuccessTest()
        {
            //Arrange
            var path = @"C:\Users\ghazanfer.khan\Desktop\second file.csv";
            var actual = true;
            //Act
            var expected = _FileValidation.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile2_FileExists_FailTest()
        {
            //Arrange
            var path = @"C:\Users\ghazanfer.khan\Desktop\second.csv";
            var actual = true;
            //Act
            var expected = _FileValidation.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual, "File not exist");
        }


        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile3_FileExists_SuccessTest()
        {
            //Arrange
            var path = @"C:\Users\ghazanfer.khan\Desktop\third file.csv";
            var actual = true;
            //Act
            var expected = _FileValidation.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile3_FileExists_FailTest()
        {
            //Arrange
            var path = @"C:\Users\ghazanfer.khan\Desktop\third.csv";
            var actual = true;
            //Act
            var expected = _FileValidation.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual, "File not exist");
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
