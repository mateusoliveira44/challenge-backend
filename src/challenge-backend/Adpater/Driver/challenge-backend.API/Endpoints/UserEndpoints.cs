using challenge_backend.Application.DTOs;
using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.API.Constantes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace challenge_backend.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/users", UserRegister)
               .WithTags(EndpointTagConstantes.TAG_USER)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Cadastrar usuário", "Efetua o cadatro do usuário"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Created, type: typeof(UserResponseDTO), description: "Usuário cadastrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Usuário não cadastrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> UserRegister(
        [FromBody] UserRequestDTO userRequestDto,
        [FromServices] IUserService userService)
        {
            var user = await userService.UserRegister(userRequestDto.Name, userRequestDto.Email, userRequestDto.Password);

            return user is not null
                ? Results.Created($"api/users/{user.Id}", user)
                : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Usuário não cadastrado.", StatusCode = HttpStatusCode.BadRequest });
        }
    }
}
