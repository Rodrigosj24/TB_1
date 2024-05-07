using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaEntidad.Response
{
    public class ApiResponseError
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public object ErrorsMessage { get; set; }
        public ApiResponseError(bool success, int statusCode, string message , object errorsMessage)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            ErrorsMessage = errorsMessage;
        }
    }
}