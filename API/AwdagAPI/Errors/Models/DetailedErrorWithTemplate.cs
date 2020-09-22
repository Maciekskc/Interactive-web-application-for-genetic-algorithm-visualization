using Newtonsoft.Json;

namespace Validation.Models
{
    public class DetailedErrorWithTemplate: DetailedError
    {
        [JsonIgnore]
        public string Template { get; set; }
    }
}
