using System.Net;

namespace Ubereats.Helpers
{
    public class UberEatsException : Exception
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public UberEatsException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public UberEatsException(string? message) : base(message)
        {

        }
    }
}