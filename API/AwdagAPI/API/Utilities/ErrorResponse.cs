using System.Collections.Generic;

namespace API.Utilities
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }
    }
}