using System.Collections.Generic;
using Newtonsoft.Json;

namespace Validation.Models
{
    public class ErrorResult
    {
        [JsonProperty("detailedErrors")]
        public IEnumerable<DetailedError> DetailedErrors { get; set; }

        [JsonProperty("commonErrors")]
        public IEnumerable<string> CommonErrors { get; set; }

        #region Constructors

        public ErrorResult()
        {
        }

        public ErrorResult(DetailedError detailedError)
        {
            DetailedErrors = new[] { detailedError };
        }

        public ErrorResult(IEnumerable<DetailedError> detailedErrors)
        {
            DetailedErrors = detailedErrors;
        }

        public ErrorResult(string normalError)
        {
            CommonErrors = new[] { normalError };
        }

        public ErrorResult(IEnumerable<string> commonErrors)
        {
            CommonErrors = commonErrors;
        }

        public ErrorResult(DetailedError detailedError, string normalError)
        {
            DetailedErrors = new[] { detailedError };
            CommonErrors = new[] { normalError };
        }

        public ErrorResult(IEnumerable<DetailedError> detailedErrors, string normalError)
        {
            DetailedErrors = detailedErrors;
            CommonErrors = new[] { normalError };
        }

        public ErrorResult(DetailedError detailedError, IEnumerable<string> commonErrors)
        {
            DetailedErrors = new[] { detailedError };
            CommonErrors = commonErrors;
        }

        public ErrorResult(IEnumerable<DetailedError> detailedErrors, IEnumerable<string> commonErrors)
        {
            DetailedErrors = detailedErrors;
            CommonErrors = commonErrors;
        }

        #endregion Constructors
    }
}