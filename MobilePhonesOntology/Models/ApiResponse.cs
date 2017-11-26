using Newtonsoft.Json;

namespace MobilePhonesOntology.Models
{
    public class ApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("InnerException")]
        public object InnerException { get; set; }
    }
}