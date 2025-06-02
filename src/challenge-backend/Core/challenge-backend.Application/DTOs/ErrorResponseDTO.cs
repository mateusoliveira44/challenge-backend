using System.Net;

namespace challenge_backend.Application.DTOs
{
    public class ErrorResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; }
        public string MensagemErro { get; set; }
    }
}
