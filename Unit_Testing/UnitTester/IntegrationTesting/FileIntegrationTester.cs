using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
