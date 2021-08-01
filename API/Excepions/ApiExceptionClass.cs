using Newtonsoft.Json;

namespace API.Excepions
{
    public class ApiExceptionClass
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}