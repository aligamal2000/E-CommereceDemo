using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BadRequestException(List<string>errors):Exception("Bad Request")
  
    {
        public List<string> Errors { get; set; } = errors;
    }
}
