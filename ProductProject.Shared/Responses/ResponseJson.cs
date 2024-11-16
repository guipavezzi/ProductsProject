using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Shared.Responses
{
    public class ResponseJson
    {
        public string Message { get; set; }

        public ResponseJson(string message)
        {
            Message = message;
        }
    }
}