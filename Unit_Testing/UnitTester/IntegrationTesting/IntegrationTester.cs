using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;

namespace UnitTester.IntegrationTesting
{
    [TestClass]
    public class IntegrationTester
    {

        IValidation _Validation;

        public IntegrationTester()
        {
            _Validation = new Validation();
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

        

    }
}
