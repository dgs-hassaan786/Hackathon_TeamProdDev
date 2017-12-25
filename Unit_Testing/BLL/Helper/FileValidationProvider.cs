using System.IO;

namespace BLL.Helper
{
    public interface IFileValidationProvider
    {
        bool Exists(string path);
        int MultipleExist(string directory, string fileName);
    }

    public class FileValidationProvider : IFileValidationProvider
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
        public int MultipleExist(string directory,string fileName)
        {
            int fileCount = Directory.GetFiles(directory, fileName, SearchOption.TopDirectoryOnly).Length;
            return fileCount;
        }
    }
}
