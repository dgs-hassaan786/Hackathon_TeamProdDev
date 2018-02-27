﻿using BLL;
using BLL.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTester.Extensions;

namespace UnitTester.IntegrationTesting
{
    [TestClass]
    public class FileIntegrationTester
    {

        IFileValidationProvider _FileValidationProvider;
        #region File Exist and validation 

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile1_FileExists_SuccessTest()
        {
            //Arrange
            var path = Paths.File1;
            var actual = true;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile1_FileExists_FailTest()
        {
            //Arrange
            var path = Paths.Incorrect;
            var actual = true;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual, "File not exist");
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile2_FileExists_SuccessTest()
        {
            //Arrange
            var path = Paths.File2; 
            var actual = true;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile2_FileExists_FailTest()
        {
            //Arrange
            var path = Paths.Incorrect;
            var actual = true;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual, "File not exist");
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile3_FileExists_SuccessTest()
        {
            //Arrange
            var path = Paths.File3;
            var actual = true;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile3_FileExists_FailTest()
        {
            //Arrange
            var path = Paths.Incorrect;
            var actual = true;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.Exists(path);
            //Assert
            Assert.AreEqual(expected, actual, "File not exist");
        }

        [TestMethod]
        [TestCategory("File Validation")]
        public void EmployeeImportFile1_MulltipleFileExists_SuccessTest()
        {
            //Arrange
            var file = "first file.csv";
            var directory = Paths.Directory;
            var actual = 1;
            //Act
            _FileValidationProvider = new FileValidationProvider();
            var expected = _FileValidationProvider.MultipleExist(directory, file);
            //Assert
            Assert.AreEqual(expected, actual, "Multiple File exist with same name");
        }

        #endregion


        #region File Header Checking Test cases

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
        #endregion

        [TestMethod]        
        public void EmployeeImportFile_DataFormation_Datatable()
        {
            IReadCSV readCSV = new CSVHelper();
            var actual = readCSV.ExtractCSVData(@"D:\Hackathon\instructions_to_start_the_hackathon_coding\third file.csv", IntegrationMockProvider.MockHeaders());
        }


        #region Final Integration

        [TestMethod]
        public void EmployeeWholeIntegration_ReadingValidationProcessingWriting_ReturnTrueSuccess()
        {
            IEmployeeHourCalculator employeeHourCalculator = new EmployeeHourCalculator(new FileValidation(), new CSVHelper(), new CSVHelper(), new EmployeeDataProcessor());
            var expected = employeeHourCalculator.CalculateEmployeeHoursWorked();
            Assert.IsTrue(expected);
        }

        #endregion

    }

    public class IntegrationMockProvider
    {
        public static string[] MockHeaders()
        {
            return new string[3] { "EmployeeID", "Name", "HoursWorked" };
        }
    }
    public class Paths
    {
        public const string File1 = @"C:\Users\ghazanfer.khan\Desktop\first file.csv";

        public const string File2 = @"C:\Users\ghazanfer.khan\Desktop\second file.csv";

        public const string File3 = @"C:\Users\ghazanfer.khan\Desktop\third file.csv";

        public const string Incorrect = @"C:\Users\ghazanfer.khan\Desktop\file.csv";

        public const string Directory = @"C:\Users\ghazanfer.khan\Desktop";
        public const string Output = "";
    }
}
