using System.IO;

namespace BLL.Helper
{

    public interface IFileValidation
    {
        bool Exists(string path);
    }

    public class FileValidation : IFileValidation
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
