using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AddressNotFoundException(string UserName) : Exception($"Address not found for user {UserName}")
    {
    }
}
