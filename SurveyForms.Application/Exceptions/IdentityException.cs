using System;

namespace SurveyForms.Application.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException(string message) : base(message)
        {
        }
    }
}
