using System.Net;

namespace Nano_Health.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public object? Error { get; set; } = null;
        public long? Total { get; set; } 
        public object? Message { get; set; } = null;
        public object? Data { get; set; } = null;
    }
}
