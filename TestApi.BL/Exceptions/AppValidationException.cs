//using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TestApi.BL.Exceptions
{
    public class AppValidationException : Exception
    {

        private const string MESSAGE = "Unknown exception";

        public AppValidationException(): base(MESSAGE)
        {

        }

        public AppValidationException(string message) : base(message)
        {

        }
    }
}
