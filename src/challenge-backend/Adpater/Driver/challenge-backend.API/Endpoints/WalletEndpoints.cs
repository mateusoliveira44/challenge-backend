
using challenge_backend.Application.DTOs;
using challenge_backend.Application.Ports.Services.Interfaces;
using challenge_backend.Core;
using challenge_backend.API.Constantes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace challenge_backend.API.Endpoints
{
    public static class WalletEndpoints
    {
        public static void MapWalletEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/wallet/balance", GetBalance)
                .WithTags(EndpointTagConstantes.TAG_WALLET)
                .WithMetadata(new SwaggerOperationAttribute(summary: "Consultar saldo", "Consulta o saldo da carteira do usu�rio"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(WalletBalanceResponseDTO), description: "Saldo consultado com sucesso"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Unauthorized, type: typeof(ErrorResponseDTO), description: "N�o autorizado"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"))
                .RequireAuthorization();

            app.MapPost("api/wallet/deposit", Deposit)
                .WithTags(EndpointTagConstantes.TAG_WALLET)
                .WithMetadata(new SwaggerOperationAttribute(summary: "Adicionar saldo", "Adiciona saldo � carteira do usu�rio"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, description: "Saldo adicionado com sucesso"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisi��o inv�lida"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Unauthorized, type: typeof(ErrorResponseDTO), description: "N�o autorizado"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"))
                .RequireAuthorization();

            app.MapPost("api/wallet/transfer", Transfer)
                .WithTags(EndpointTagConstantes.TAG_WALLET)
                .WithMetadata(new SwaggerOperationAttribute(summary: "Transferir saldo", "Realiza uma transfer�ncia entre usu�rios"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, description: "Transfer�ncia realizada com sucesso"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisi��o inv�lida"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Unauthorized, type: typeof(ErrorResponseDTO), description: "N�o autorizado"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"))
                .RequireAuthorization();

            app.MapGet("api/wallet/transactions", GetTransactions)
                .WithTags(EndpointTagConstantes.TAG_WALLET)
                .WithMetadata(new SwaggerOperationAttribute(summary: "Listar transa��es", "Lista as transfer�ncias de um usu�rio, com filtro opcional por data"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(List<TransferResponseDTO>), description: "Lista de transa��es consultada com sucesso"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Unauthorized, type: typeof(ErrorResponseDTO), description: "N�o autorizado"))
                .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"))
                .RequireAuthorization();

        }

        private static async Task<IResult> GetBalance(
        [FromQuery] int userId,
        [FromServices] IWalletService walletService)
        {
            var balance = await walletService.GetBalance(userId);
            return Results.Ok(new WalletBalanceResponseDTO { Balance = balance });
        }

        private static async Task<IResult> Deposit(
            [FromBody] DepositRequestDTO deposit,
            [FromServices] IWalletService walletService)
        {
            if (deposit == null || deposit.Amount <= 0)
                return Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Valor inv�lido.", StatusCode = HttpStatusCode.BadRequest });

            await walletService.Deposit(deposit.UserId, deposit.Amount);
            return Results.Ok();
        }

        private static async Task<IResult> Transfer(
            [FromBody] TransferRequestDTO transfer,
            [FromServices] IWalletService walletService)
        {
            if (transfer == null || transfer.Amount <= 0 || transfer.ToUserId == 0)
                return Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Dados inv�lidos para transfer�ncia.", StatusCode = HttpStatusCode.BadRequest });

            try
            {
                await walletService.Transfer(transfer.SourceUserId, transfer.ToUserId, transfer.Amount);
                return Results.Ok();
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new ErrorResponseDTO { MensagemErro = ex.Message, StatusCode = HttpStatusCode.BadRequest });
            }
        }

        private static async Task<IResult> GetTransactions(
            [FromQuery] int userId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromServices] IWalletService walletService)
        {
            var transactions = await walletService.GetTransactions(userId, startDate, endDate);
            return Results.Ok(transactions.Select(t => new TransferResponseDTO(t.SourceUserId, t.DestinationUserId, t.Amount, t.Type, t.CreatedAt)));
        }
    }
}