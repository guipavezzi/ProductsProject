using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Shared.Exceptions
{
    public class NotFoundException : ProductProjectException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}