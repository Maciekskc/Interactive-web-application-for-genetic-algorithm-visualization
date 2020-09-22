using System.Collections.Generic;
using System.Net;

namespace Application.Utilities
{
    public class ServiceResponse
    {
        public HttpStatusCode ResponseType { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ServiceResponse(HttpStatusCode responseType)
        {
            ResponseType = responseType;
        }

        public ServiceResponse(HttpStatusCode responseType, IEnumerable<string> errors)
        {
            ResponseType = responseType;
            Errors = errors;
        }
    }

    public class ServiceResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Payload { get; set; }
        public string CreatedUrlLocation { get; set; }

        public ServiceResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceResponse(HttpStatusCode statusCode, IEnumerable<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public ServiceResponse(HttpStatusCode statusCode, T payload)
        {
            StatusCode = statusCode;
            Payload = payload;
        }

        public ServiceResponse(HttpStatusCode statusCode, IEnumerable<string> errors, T payload)
        {
            StatusCode = statusCode;
            Errors = errors;
            Payload = payload;
        }
    }
}