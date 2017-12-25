using BLL.Helper;
using BLL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTester.UnitTesting
{
    [TestClass]
    public class EmployeeUnitTester
    {
        
        [TestMethod]
        public void EmployeeExport_CSVExporting_SuccessTest()
        {
            var expected = true;
            try
            {
                IWriteCSV _writeCSV = new CSVHelper();
                var actual = _writeCSV.Write(@"D:\Hackathon\Output", EmployeeMock.GetEmployeesMockData());
                Assert.AreEqual(expected, actual);

            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }

    public class EmployeeMock
    {
        public static IEnumerable<Employee> GetEmployeesMockData()
        {
            var lst = new List<Employee>();

            lst.Add(new Employee("111", "Kent Beck", 170));
            lst.Add(new Employee("222", "Martin Fowler", 116));
            lst.Add(new Employee("333", "Jeff Sutherland", 89));
            lst.Add(new Employee("444", "Ken Schwaber", 219));
            lst.Add(new Employee("555", "Azfar Akram", 166));
            lst.Add(new Employee("666", "Shafqat Mahmood", 79));

            return lst;
        }
    }
}
