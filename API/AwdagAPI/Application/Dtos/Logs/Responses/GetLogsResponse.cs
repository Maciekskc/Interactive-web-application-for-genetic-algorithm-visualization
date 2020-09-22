using System;
using System.Collections.Generic;

namespace Application.Dtos.Logs.Responses
{
    public class GetLogsFilesResponse
    {
        public List<LogForGetLogsFilesResponse> Logs { get; set; }
    }

    public class LogForGetLogsFilesResponse
    {
        public string Name { get; set; }
        public long SizeInKb { get; set; }
        public DateTime Date { get; set; }
    }
}