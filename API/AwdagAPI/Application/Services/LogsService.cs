using Application.Dtos.Logs.Responses;
using Application.Infrastructure.Errors;
using Application.Interfaces;
using Ionic.Zip;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Application.Utilities;
using Validation;
using Validation.Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Application.Services
{
    public class LogsService : Service, ILogsService
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;

        public LogsService(IServiceProvider serviceProvider, IWebHostEnvironment webHostingEnvironment, IConfiguration configuration) : base(serviceProvider)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        public ServiceResponse<GetLogsFilesResponse> GetLogsFiles()
        {
            var dir = new DirectoryInfo(Path.Combine(_webHostingEnvironment.ContentRootPath, _configuration["Constants:logsDirectory"]));
            var logs = dir.GetFiles();

            var response = new GetLogsFilesResponse();
            try
            {
                response.Logs = Mapper.Map<List<LogForGetLogsFilesResponse>>(logs).OrderByDescending(x => x.Date).ToList();
            }
            catch (FormatException)
            {
                ErrorResultToReturn = new ErrorResult(Errors.LogErrors.ConvertingDateFromFileNameFailed);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }
            catch (Exception)
            {
                ErrorResultToReturn = new ErrorResult(Errors.LogErrors.ProcessingError);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            return new ServiceResponse<GetLogsFilesResponse>(HttpStatusCode.OK, response);
        }

        public ServiceResponse<byte[]> GetLogsFileBytes(string logName)
        {
            var dir = new DirectoryInfo(Path.Combine(_webHostingEnvironment.ContentRootPath, _configuration["Constants:logsDirectory"]));
            var filePath = $"{dir}\\{logName}.txt";

            if (!File.Exists(filePath))
                throw new RestException(HttpStatusCode.NotFound);

            byte[] fileBytes = File.ReadAllBytes(filePath);
            return new ServiceResponse<byte[]>(HttpStatusCode.OK, fileBytes);
        }

        public ServiceResponse<byte[]> GetAllLogsFilesBytes()
        {
            var dir = new DirectoryInfo(Path.Combine(_webHostingEnvironment.ContentRootPath, _configuration["Constants:logsDirectory"]));
            var logs = dir.GetFiles();
            ZipFile zip = new ZipFile { AlternateEncodingUsage = ZipOption.AsNecessary };
            zip.AddDirectoryByName("Logs");
            foreach (var log in logs)
            {
                zip.AddFile(log.FullName, "Logs");
            }

            using MemoryStream memoryStream = new MemoryStream();
            zip.Save(memoryStream);
            return new ServiceResponse<byte[]>(HttpStatusCode.OK, memoryStream.ToArray());
        }

        public ServiceResponse<string[]> GetLogsFileText(string logName)
        {
            var dir = new DirectoryInfo(Path.Combine(_webHostingEnvironment.ContentRootPath, _configuration["Constants:logsDirectory"]));
            var filePath = $"{dir}\\{logName}.txt";

            if (!File.Exists(filePath))
                throw new RestException(HttpStatusCode.NotFound);

            var fileLines = File.ReadAllLines(filePath);

            return new ServiceResponse<string[]>(HttpStatusCode.OK, fileLines);
        }
    }
}