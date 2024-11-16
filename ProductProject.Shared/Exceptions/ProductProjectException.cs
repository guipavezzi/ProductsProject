using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Shared.Exceptions
{
    public class ProductProjectException : Exception
    {
        public ProductProjectException(string message) : base(message){}
    }
}