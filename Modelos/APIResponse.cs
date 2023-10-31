using System.Net;

namespace primeraApi.Modelos
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsExitoso { get; set; }
        public List<string> ErrorMessage { get; set; }
        public object Resultado { get; set; }

    }
}
