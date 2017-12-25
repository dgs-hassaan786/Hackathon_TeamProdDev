using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class Employee
    {
        public Employee()
        {

        }

        public Employee(string employeeId, string name, int hoursWorked)
        {
            EmployeeId = employeeId;
            Name = name;
            HoursWorked = hoursWorked;
        }

        public string EmployeeId  { get; set; }
        public string Name { get; set; }
        public int HoursWorked { get; set; }
    }
}
