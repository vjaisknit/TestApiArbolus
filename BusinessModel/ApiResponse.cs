using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonIgnore] // Exclude from JSON because API does not send this field
        public HttpStatusCode ResponseStatusCode { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public ApiResponse() { }

        public ApiResponse(string responseStatus, string message, T data, HttpStatusCode responseStatusCode = HttpStatusCode.OK)
        {
            Status = responseStatus;
            ResponseStatusCode = responseStatusCode;
            Message = message;
            Data = data;
        }

        public ApiResponse(HttpStatusCode responseStatusCode, string message)
        {
            Status = responseStatusCode == HttpStatusCode.OK ? "success" : "error";
            ResponseStatusCode = responseStatusCode;
            Message = message;
        }
    }

}
