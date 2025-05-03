using InventoryManagementSystem.API.DTOs.Account;
using InventoryManagementSystem.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MediatR;
using InventoryManagementSystem.BLL.CQRS.Orchestrators.Account;
using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.Account;
using Azure.Core;
using System.Threading;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {

            this.mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<ApiResponseResult<RegisterchestratorResponse>> Register([FromBody] RegisterRequest model,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponseResult<RegisterchestratorResponse>.Error("invalid Model state");
            }

            var result = await mediator.Send(new RegisterOrchestrator { registerCommand = model }, cancellationToken);
            if (result.IsSuccess)
            {
                return ApiResponseResult<RegisterchestratorResponse>.Success(result.Data, "Register Successfully");

            }
            return ApiResponseResult<RegisterchestratorResponse>.Error(result.Error);
        }

           

            [HttpPost("login")]
        public async Task<ApiResponseResult<LoginOrchestratorResponse>> Login([FromBody] LoginRequest model,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponseResult<LoginOrchestratorResponse>.Error("invalid Model state");
            }
            var result =await mediator.Send(new LoginOrchestrator { loginRequest= model },cancellationToken);
            if (result.IsSuccess)
            {
                return ApiResponseResult<LoginOrchestratorResponse>.Success(result, "Login Successfully");

            }
            return ApiResponseResult<LoginOrchestratorResponse>.Error("Invalid Login");
            

        }

    }
}
