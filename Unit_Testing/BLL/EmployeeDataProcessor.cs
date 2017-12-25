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
        List<Employee> FormattingData(DataTable table);
        DataTable MergeDataSource(List<DataTable> dataSources);
        List<Employee> ProcessData(List<Employee> unproccessedEmployeeList);
    }    

    public class EmployeeDataProcessor : IEmployeeDataProcessor
    {

        public DataTable MergeDataSource(List<DataTable> dataSources)
        {
            DataTable table = new DataTable("TblEmployee");
            foreach (var dt in dataSources)
            {
                table.Merge(dt);
                table.Merge(dt);
                table.Merge(dt);
            }

            try
            {
                var dt = table.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
                var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
                DataTable dt2 = UniqueRows.CopyToDataTable();
                return dt2;

            }
            catch (Exception)
            {

                return table;
            }
            
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
