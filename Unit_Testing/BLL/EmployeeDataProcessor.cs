using BLL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public interface IEmployeeDataProcessor
    {
        List<Employee> ProcessData(List<Employee> unproccessedEmployeeList);
    }

    public class EmployeeDataProcessor : IEmployeeDataProcessor
    {

        public DataTable MergeDataSource(DataTable dataSource1, DataTable dataSource2, DataTable dataSource3)
        {
            DataTable table = new DataTable("TblEmployee");
            table.Merge(dataSource1);
            table.Merge(dataSource2);
            table.Merge(dataSource3);

            return table;
        }

        public List<Employee> FormattingData(DataTable table)
        {
            List<Employee> employees = new List<Employee>();
            employees = (from DataRow dr in table.Rows
                         select new Employee()
                         {
                             EmployeeId = dr["EmployeeID"].ToString(),
                             Name = dr["Name"].ToString(),
                             HoursWorked = Convert.ToInt32(dr["HoursWorked"])
                         }).ToList();

            return employees;
        }


        public List<Employee> ProcessData(List<Employee> unproccessedEmployeeList)
        {

            var result = unproccessedEmployeeList.GroupBy(x => x.EmployeeId).Select(emp => new Employee()
            {
                EmployeeId = emp.First().EmployeeId,
                Name = emp.First().Name,
                HoursWorked = emp.Sum(h => h.HoursWorked)
            }).ToList();

            return result;
        }

    }
}
