using System.Collections.Generic;

namespace API.Excepions
{
    public class ValidationErrorResponse : ApiExceptionClass
    {
        public ValidationErrorResponse()
        {
            
        }
        public IEnumerable<string> Errors { get; set; }

    }
}