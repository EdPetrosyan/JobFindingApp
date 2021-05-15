using System;
using System.Net;


namespace AppLayer.Exceptions
{
    public class AppException : Exception
    {
        public AppException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public object Errors { get; set; }

        public HttpStatusCode Code { get; }
    }
}
