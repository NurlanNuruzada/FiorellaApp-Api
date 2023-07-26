using Fiorella.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Persistence.Exceptions;

public class UserBlockedException : Exception, IBaseException
{
	public int StatusCode { get; set; }
	public string CustomMessage { get; set; }

	public UserBlockedException(string message):base(message)
	{
		StatusCode=(int)HttpStatusCode.Locked;
		CustomMessage=message;
	}
}
