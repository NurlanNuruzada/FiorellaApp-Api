using Fiorella.Persistence.Exceptions;
using System.Net;

namespace Fiorello.Persistence.Exceptions.CustomExceptions
{
    public class SignInFailureException : Exception, IBaseException
    {
        public int StatusCode { get; set; }
        public string CustomMessage { get; set; }
        public SignInFailureException(string message) : base(message)
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
