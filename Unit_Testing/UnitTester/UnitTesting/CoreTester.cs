using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using UnitTester.Extensions;
using Foundation.Logging;
using System.Configuration;

namespace UnitTester.UnitTesting
{
    [TestClass]
    public class CoreTester
    {
        ICore _Core;

        public CoreTester()
        {
            _Core = new Core();
        }

        [TestMethod]
        public void ReverseString_CustomValueTest_SuccessTest()
        {
            var input = "My name is Hassaan Khan";
            var expected = _Core.ReverseString(input);
            var actual = "nahK naassaH si eman yM";

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        // [ExpectedException(typeof(IndexOutOfRangeException))]//,"paratemer: a[] is null"
        [CustomExpectedException(typeof(IndexOutOfRangeException))]
        public void ArrayElementSumUp_ZeroElement_IndexOutOfRangeException()
        {

            _Core.SumElementWithinIndex(new int[5] { 0, 1, 2, 5, 6 });
            
        }


    }


}
