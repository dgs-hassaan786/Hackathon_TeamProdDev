﻿using BLL;
using BLL.Helper;
using BLL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTester.UnitTesting
{
    [TestClass]
    public class EmployeeUnitTester
    {

        [TestMethod]
        [TestCategory("Unit Testing")]
        public void EmployeeDataProcessing_GenerateListOfProcessedData_ReturnList()
        {

            IEmployeeDataProcessor employeeDataProcessor = new EmployeeDataProcessor();
            var actual = EmployeeMock.ResultantProcessedData().ToList().OrderBy(x=>x.EmployeeId).ToList();
            var expected = employeeDataProcessor.ProcessData(EmployeeMock.GetMockEmployeeUnprocessedData().ToList()).OrderBy(x=>x.EmployeeId).ToList();
            CollectionAssert.AreEqual(expected, actual,new EmployeeComparer());


        }


        [TestMethod]
        [TestCategory("Unit Testing")]
        public void EmployeeDataProcessing_MergeDataSources_ReturnDataTable()
        {

            IEmployeeDataProcessor employeeDataProcessor = new EmployeeDataProcessor();

            var expected = employeeDataProcessor.MergeDataSource(EmployeeMock.GetDataTable());
            var resultant = EmployeeMock.Resultant();

            Assert.AreEqual(expected.Rows.Count, resultant.Rows.Count);


        }

        [TestMethod]
        [TestCategory("Unit Testing")]
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

        public static IEnumerable<Employee> GetMockEmployeeUnprocessedData()
        {
            var lst = new List<Employee>();

            lst.Add(new Employee("111", "Kent Beck", 170));
            lst.Add(new Employee("222", "Martin Fowler", 116));
            lst.Add(new Employee("333", "Jeff Sutherland", 89));
            lst.Add(new Employee("444", "Ken Schwaber", 219));
            lst.Add(new Employee("555", "Azfar Akram", 166));
            lst.Add(new Employee("666", "Shafqat Mahmood", 79));
            lst.Add(new Employee("444", "Ken Schwaber", 144));
            lst.Add(new Employee("555", "Azfar Akram", 20));
            lst.Add(new Employee("666", "Shafqat Mahmood", 212));

            return lst;
        }

        public static IEnumerable<Employee> ResultantProcessedData()
        {
            var lst = new List<Employee>();

            lst.Add(new Employee("111", "Kent Beck", 170));
            lst.Add(new Employee("222", "Martin Fowler", 116));
            lst.Add(new Employee("333", "Jeff Sutherland", 89));
            lst.Add(new Employee("444", "Ken Schwaber", 363));
            lst.Add(new Employee("555", "Azfar Akram", 186));
            lst.Add(new Employee("666", "Shafqat Mahmood", 291));
            return lst;
        }

        public static List<DataTable> GetDataTable()
        {
            var lst = new List<DataTable>();

            DataTable table1 = new DataTable();
            table1.Columns.Add("EmployeeID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("HoursWorked", typeof(string));


            table1.Rows.Add(25, "Hassaan", 10);
            table1.Rows.Add(50, "Ghazanfar", 20);
            table1.Rows.Add(10, "Haroon", 30);
           


            DataTable table2 = new DataTable();
            table2.Columns.Add("EmployeeID", typeof(int));
            table2.Columns.Add("Name", typeof(string));
            table2.Columns.Add("HoursWorked", typeof(string));


            table2.Rows.Add(25, "Hassaan", 10);
            table2.Rows.Add(50, "Ghazanfar", 20);
            table2.Rows.Add(10, "Haroon", 30);

            lst.Add(table1);
            lst.Add(table2);

            return lst;

        }

        public static DataTable Resultant() {

            DataTable table1 = new DataTable();
            table1.Columns.Add("EmployeeID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("HoursWorked", typeof(string));
            table1.Rows.Add(25, "Hassaan", 10);
            table1.Rows.Add(50, "Ghazanfar", 20);
            table1.Rows.Add(10, "Haroon", 30);
            table1.Rows.Add(25, "Hassaan", 10);
            table1.Rows.Add(50, "Ghazanfar", 20);
            table1.Rows.Add(10, "Haroon", 30);

            return table1;
        }



    }
    public class EmployeeComparer : Comparer<Employee>
    {
        public override int Compare(Employee x, Employee y)
        {
            // compare the two mountains
            // for the purpose of this tests they are considered equal when their identifiers (hours worked) match
            return x.HoursWorked.CompareTo(y.HoursWorked);
        }
    }
}
