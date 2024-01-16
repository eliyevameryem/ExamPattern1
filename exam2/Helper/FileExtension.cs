using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace exam2.Helper
{
    public static class FileExtension
    {
        public static bool CheckFileType(this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }

        public static bool CheckFileLength(this IFormFile file,int length)
        {
            return file.Length > 1024 * length;
        }
        public static string CreateFile(this IFormFile file, string envPath, string folder)
        {
            string filename=Guid.NewGuid().ToString()+ file.FileName; 
            string path=Path.Combine(envPath, folder, filename);
           using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
           return filename;

        }
        public static void DeleteFile(this string Image, string envPath, string folder)
        {
            string path = Path.Combine(folder, Image, envPath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
