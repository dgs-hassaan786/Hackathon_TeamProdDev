using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Logging
{
    public interface ILog
    {        
        void WriteLog(string message);
    }
    public class Logger : ILog
    {
        string _filename = "";
        string _path = @"d:\Unit_testcases\";
        public Logger()
        {

        }

        public Logger(string fileName, string path)
        {
            _filename = fileName;
            _path = path;
        }
        public void WriteLog(string message)
        {
            try
            {
                bool exists = Directory.Exists(_path);
                if (!exists)
                {
                    Directory.CreateDirectory(_path);
                }
                if (!_path.EndsWith(@"\"))
                    _path += @"\";

                if (!string.IsNullOrEmpty(message))
                {

                    string filePath = _path + "Log(" + _filename + ").txt";
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        using (StreamWriter sw = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
                        {
                            sw.WriteLine(Environment.NewLine + String.Format("DateTime {0}, Logging \n {1}", DateTime.Now, message));
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
