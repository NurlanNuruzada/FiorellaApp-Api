using Fiorella.Aplication.DTOs.AuthDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Persistence.Exceptions
{
    public class SingInFailureException : Exception, IBaseException
    {
        public int StatusCode { get; set; }

        public string CustomMessage { get; set; }
        public SingInFailureException(string Message) : base(Message)
        {
            StatusCode = (int)HttpStatusCode.Conflict;
            CustomMessage = Message;
        }
    }
}
