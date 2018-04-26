using System;
using System.Collections.Generic;

namespace ProgrammingTest.BusinessLogic.Exceptions
{
    public class ValidationException :Exception
    {
        public ValidationException(Dictionary<string, string> messages)
        {
            ValidationMessages = messages;
        }

        public Dictionary<string, string> ValidationMessages { get; set; }
    }
}
