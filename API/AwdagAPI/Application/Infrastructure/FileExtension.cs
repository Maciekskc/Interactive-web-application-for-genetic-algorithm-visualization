using System.IO;
using System.Net;
using Application.Infrastructure.Errors;
using Microsoft.AspNetCore.Http;
using Validation.Models;

namespace Application.Infrastructure
{
    public static class FileExtension
    {
        public const string Docx = ".docx";


        /// <summary>
        /// Metoda sprawdzająca poprawność rozszerzenie w pliku
        /// </summary>
        /// <param name="file"></param>
        /// <param name="extension"></param>
        public static void Validate(IFormFile file, string extension)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != extension)
            {
                var error = new ErrorResult(Validation.Errors.FileErrors.NotSupportedFileType.SetParams(file.Name, extension));
                throw new RestException(HttpStatusCode.BadRequest, error);
            }
        }
    }
}
