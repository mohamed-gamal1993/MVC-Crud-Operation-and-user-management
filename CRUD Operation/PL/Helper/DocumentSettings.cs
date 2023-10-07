using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            // get located folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);
            //get file name and make it uniqu [use Guid]
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            //get file path
            var filePath = Path.Combine(folderPath, fileName);

            //save file as stream[stream:data per time]
            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName; 
        }
        public static void DeleteFile(string fileName,string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
