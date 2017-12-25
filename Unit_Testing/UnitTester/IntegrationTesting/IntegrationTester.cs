using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;

namespace UnitTester.IntegrationTesting
{
    [TestClass]
    public class IntegrationTester
    {

        IValidation _Validation;
        IFileValidation _FileValidation;
        public IntegrationTester()
        {
            _Validation = new Validation();
            _FileValidation = new FileValidation();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "paratemer: input is null")]
        public void StringReverse_NullCase_ArgumentNullException()
        {
            _Validation.StringValidation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "paratemer: input is empty")]
        public void StringReverse_EmptyStringCase_ArgumentNullException()
        {
            _Validation.StringValidation("");
        }

        [TestMethod]        
        public void StringReverse_CorrectInputString_Success()
        {
            var actual = true;
            var expected = _Validation.StringValidation("My name is hassaan");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "paratemer: a[] is null")]
        public void ArrayElementSumUp_NullCase_ArgumentNullException()
        {
            _Validation.ArrayPreValidation(null);
        }
        [TestMethod]
        public void File_Exist_ReturnFile()
        {
            //Arrange
            string path = @"D:\BrainTraces\logs1\Log(20161206).txt";
            bool actual = true;

            //Act
            var expected = _FileValidation.Exists(path);

            Assert.AreEqual(actual, expected);
        }
        

    }
}
