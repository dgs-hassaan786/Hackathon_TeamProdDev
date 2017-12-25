using BLL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helper
{

    public interface IWriteCSV
    {
        bool Write(string path, IEnumerable<Employee> employee);
    }

    public class CSVHelper : IWriteCSV
    {

        public bool Write(string path, IEnumerable<Employee> employees)
        {
            var status = true;

            bool exists = Directory.Exists(path);
            if (!exists)
            {
                Directory.CreateDirectory(path);
            }
            if (!path.EndsWith(@"\"))
                path += @"\";


            string filePath = path + "output.csv";
            if (!string.IsNullOrEmpty(filePath))
            {
                using (StreamWriter w = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
                {
                    var header = string.Format($"{nameof(Employee.EmployeeId)},{nameof(Employee.EmployeeId)},{nameof(Employee.EmployeeId)}");
                    w.WriteLine(header);
                    w.Flush();
                    foreach (var e in employees)
                    {
                        var line = string.Format($"{e.EmployeeId},{e.Name},{e.HoursWorked}");
                        w.WriteLine(line);
                        w.Flush();
                    }
                }
            }
            
            return status;
        }


    }
}
