using challenge_backend.Application.DTOs;
using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.API.Constantes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace challenge_backend.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/auth/login", Login)
                .WithTags(EndpointTagConstantes.TAG_AUTH)
                .WithMetadata(new SwaggerOperationAttribute(summary: "Realizer o Login", description: "Retorna o token"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(string), description: "Login realizado com sucesso"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Usuário ou senha inválido"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> Login(
        [FromBody] LoginDtoRequest loginDto,
        [FromServices] IAuthService authService)
        {
            var token = await authService.Login(loginDto.Email, loginDto.Password);

            return token is not null
                ? Results.Ok(token)
                : Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Usuário ou senha inválido", StatusCode = HttpStatusCode.NotFound });
        }
    }
}
