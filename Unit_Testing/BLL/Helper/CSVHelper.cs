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
        Task Write(string outputPath, IEnumerable<Employee> employee);
    }

    class CSVHelper : IWriteCSV
    {
        
        public Task Write(string outputPath, IEnumerable<Employee> employee)
        {
            using (var fileStream = File.OpenWrite(outputPath))
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                var result = false;
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        var writer = new BinaryWriter(ms);
                        writer.Write(employee.);
                        var binaryRecord = ms.ToArray();

                        
                        result = true;
                    }
                }
                catch (Exception e)
                {
                    //log exception here
                    //_Logger.Error(e, $"Exception generated in method: {nameof(AdwordsSnapshotWriter)}.{nameof(Write)}");
                }
                //  return result;

                return Task.FromResult(0);
            }
        }


        internal bool Write(IEnumerable<Employee> employee)
        {
            var result = false;
            //try
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        var writer = new BinaryWriter(ms);
            //        writer.Write(adwordsPull.Data);
            //        var binaryRecord = ms.ToArray();

            //        DatabaseManager.AddAdwordsPullData(adwordsPull.TrendDate, adwordsPull.Hour, adwordsPull.Minute, binaryRecord).Forget();
            //        result = true;
            //    }
            //}
            //catch (Exception e)
            //{
            //    //log exception here
            //    _Logger.Error(e, $"Exception generated in method: {nameof(AdwordsSnapshotWriter)}.{nameof(Write)}");
            //}
            return result;
        }

    }
}
