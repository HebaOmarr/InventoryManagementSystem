using InventoryManagementSystem.API.DTOs.Account;
using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Commands.Account;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators.Account
{
    public class LoginOrchestratorResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expired { get; set; }
    }
    public class LoginOrchestrator : IRequest<LoginOrchestratorResponse>
    {
       public LoginRequest loginRequest { get; set; } 
    }
    public class LoginOrchestratorHandler : IRequestHandler<LoginOrchestrator, LoginOrchestratorResponse>
    {
        private readonly IMediator mediator;

        public LoginOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<LoginOrchestratorResponse> Handle(LoginOrchestrator request, CancellationToken cancellationToken)
        {

            var result = await mediator.Send(new LoginCommand()
            {
                loginRequest = request.loginRequest
            }, cancellationToken);

            if (result == null)
            {
                throw new Exception("User Not found");
            }
            
            string token = await mediator.Send(new GenerateTokenCommand()
            {
                user = result!.ApplicationUser,
                Expired = result.Expired
            }, cancellationToken);
            return new LoginOrchestratorResponse()   
            {
                Token = token,
                Expired = result.Expired
            };
        }
    }
}
