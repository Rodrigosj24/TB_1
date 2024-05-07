using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaEntidad.Response
{
    public class ApiResponseData
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public object Messages { get; set; }
        public object Data { get; set; }

        public ApiResponseData(bool success, int statusCode,object messages, object data)
        {
            Success = success;
            StatusCode = statusCode;
            Messages = messages;
            Data = data;
        }
    }
}