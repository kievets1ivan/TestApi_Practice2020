using System;

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
