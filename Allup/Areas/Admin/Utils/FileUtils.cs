using Fiorella.Areas.Admin.Controllers.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Fiorella.Areas.Admin.Utils
{
    public static class FileUtils
    {
        public static string FileCreate(string folderPath,IFormFile file)
        {
            string fileName = file.FileName;
            var path = Path.Combine(folderPath, fileName);
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return fileName;
        }

        public static void FileDelete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
