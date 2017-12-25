using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    public interface ICore
    {
        string ReverseString(string input);
        int[] SumElementWithinIndex(int[] a);
    }

    public interface IValidation
    {
        bool StringValidation(string input);
        bool ArrayPreValidation(int[] a);

    }
    public interface IFileValidation
    {
        bool Exists(string path);
    }
    public class Validation : IValidation
    {
        public bool StringValidation(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("input", "Argument is null or length is zero");

            return true;
        }

        public bool ArrayPreValidation(int[] a)
        {
            if (a == null)
                throw new ArgumentNullException();

            return true;
        }
    }

    public class Core : ICore
    {
        public string ReverseString(string input)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = input.Length - 1; i >= 0; i--)
            {

                builder.Append(input[i]);
            }

            return builder.ToString();
        }

        public int[] SumElementWithinIndex(int[] a)
        {
            try
            {
                var b = new int[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                    b[a[i]]++;
                }

                return b;
            }
            catch (IndexOutOfRangeException ex)
            {

                throw new IndexOutOfRangeException("Array does not contain that index", ex);
            }
        }

    }
    public class FileValidation : IFileValidation
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
