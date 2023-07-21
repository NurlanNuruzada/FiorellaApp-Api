using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Persistence.Exceptions
{
    public interface IBaseException
    {
        int StatusCode { get; }
        string CustomMessage { get; }
    }
}
