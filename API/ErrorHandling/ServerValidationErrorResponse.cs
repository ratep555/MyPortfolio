using System.Collections.Generic;

namespace API.ErrorHandling
{
    public class ServerValidationErrorResponse : ServerResponse
    {
         public ServerValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}