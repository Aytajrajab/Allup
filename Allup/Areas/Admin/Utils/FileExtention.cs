using Microsoft.AspNetCore.Http;

namespace Fiorella.Areas.Admin.Utils
{
    public static class FileExtention
    {
        public static bool IsContains(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool IsRightSize(this IFormFile file, int kb)
        {
            return file.Length > 1024 * kb;
        }
    }
}
