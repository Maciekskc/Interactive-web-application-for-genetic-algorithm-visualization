using API.Utilities;
using Application.Dtos.Logs.Responses;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    public class LogsController : BaseController
    {
        private readonly ILogsService _logsService;

        public LogsController(ILogsService logsService)
        {
            _logsService = logsService;
        }

        /// <summary>
        /// Zwraca listę danych wszystkich plików z logami.
        /// </summary>
        /// <returns></returns>
        [Produces(typeof(Response<GetLogsFilesResponse>))]
        [HttpGet]
        public IActionResult GetLogsFiles()
        {
            var response = _logsService.GetLogsFiles();
            return SendResponse(response);
        }

        /// <summary>
        /// Zwraca plik logu o nazwie zadanej w parametrze metody.
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        [Produces(typeof(FileContentResult))]
        [HttpGet("download/{logName}")]
        public IActionResult DownloadLogFile(string logName)
        {
            var response = _logsService.GetLogsFileBytes(logName);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string zipName = $"{logName}.txt";
                string mimeType = System.Net.Mime.MediaTypeNames.Application.Octet;

                return new FileContentResult(response.Payload, mimeType)
                {
                    FileDownloadName = zipName
                };
            }
            return SendResponse(response);
        }

        /// <summary>
        /// Zwraca treść pliku z logami o nazwie zadanej w parametrze.
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        [Produces(typeof(Response<string[]>))]
        [HttpGet("{logName}")]
        public IActionResult GetLogsFileText(string logName)
        {
            var response = _logsService.GetLogsFileText(logName);
            return SendResponse(response);
        }

        /// <summary>
        /// Zwraca wszystkie pliki z logami w formacie .zip.
        /// </summary>
        /// <returns></returns>
        [Produces(typeof(FileContentResult))]
        [HttpGet("download/all")]
        public IActionResult DownloadAllLogsFiles()
        {
            var response = _logsService.GetAllLogsFilesBytes();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string zipName = $"Logs_{DateTime.Now:yyyy-MM-dd-HHmmss}.zip";
                string mimeType = "application/zip";

                return new FileContentResult(response.Payload, mimeType)
                {
                    FileDownloadName = zipName
                };
            }
            return SendResponse(response);
        }
    }
}