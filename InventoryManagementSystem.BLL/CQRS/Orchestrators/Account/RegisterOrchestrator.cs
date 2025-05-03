using InventoryManagementSystem.API.DTOs.Account;
using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Commands.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators.Account
{
    public class RegisterchestratorResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expired { get; set; }
    }
    public class RegisterOrchestrator : IRequest<Result<RegisterchestratorResponse>>
    {
        public RegisterRequest registerCommand { get; set; } = null!;
    }
    public class RegisterOrchestratorHandler : IRequestHandler<RegisterOrchestrator, Result<RegisterchestratorResponse>>
    {
        private readonly IMediator mediator;
        public RegisterOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<Result<RegisterchestratorResponse>> Handle(RegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new RegisterCommand()
            {
                registerRequest = request.registerCommand
            }, cancellationToken);
            if (result.IsSuccess)
            {
                string token = await mediator.Send(new GenerateTokenCommand()
                {
                    user = result.Data.ApplicationUser,
                    Expired = result.Data.Expired
                }, cancellationToken);

                return
                    Result<RegisterchestratorResponse>.Success(new RegisterchestratorResponse()
                    {
                        Token = token,
                        Expired = result.Data.Expired
                    });
                
            }
            return Result<RegisterchestratorResponse>.Failure(result.Error);
        }
    }

}
