using InventoryManagementSystem.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoryManagementSystem.API.DTOs;
using MediatR;
using InventoryManagementSystem.BLL.CQRS.Commands.Administration;
using InventoryManagementSystem.API.ViewModel;
using Azure.Core;
using InventoryManagementSystem.BLL.CQRS.Queries.Administoration;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AdministrationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateRole")]
        public async Task<ApiResponseResult<string>> CreateRole([FromBody] string roleName)
        {
            var result = await mediator.Send(new CreateRoleCommand { RoleName = roleName });
            if (!result.IsSuccess)
            {
                return ApiResponseResult<string>.Error(result.Message);
            }

            return ApiResponseResult<string>.Success(result.Message, "Role created successfully.");

        }
        [HttpPost("DeleteRole")]
        public async Task<ApiResponseResult<string>> DeleteRole([FromBody] DeleteRoleCommand request)
        {
            var result = await mediator.Send(request);
            if (!result.IsSuccess)
            {
                return ApiResponseResult<string>.Error(result.Message);
            }

            return ApiResponseResult<string>.Success(result.Message, "Role Deleted successfully.");

        }


        [HttpPost("AsssignRolesToUser")]
        public async Task<ApiResponseResult<string>> AssignRolesToUser([FromBody] UserRoleDTO model)
        {
            var result = await mediator.Send(new AssignRoleToUser { UserRoleDTO=model});
            if (!result.IsSucess)
            {
                return ApiResponseResult<string>.Error(result.message);
            }

            return ApiResponseResult<string>.Success(result.message, "Role Assign successfully.");

        }
        [HttpPost("RemoveRoleFromUser")]
        public async Task<ApiResponseResult<string>> RemoveRoleFromUser([FromBody] UserRoleDTO model)
        {
            var result = await mediator.Send(new RemoveRoleFromUserCommand { UserRoleDTO = model });
            if (!result.IsSucess)
            {
                return ApiResponseResult<string>.Error(result.message);
            }

            return ApiResponseResult<string>.Success(result.message, "Role Deleted successfully.");



        }

        [HttpGet("GetAllRoles")]
        public async Task<ApiResponseResult<List<ApplicationRole>>> GetAllRoles()
        {
            var result = await mediator.Send(new GetAllRolesQuery());
            if (result==null)
            {
                return ApiResponseResult<List<ApplicationRole>>.Error();
            }

            return ApiResponseResult<List<ApplicationRole>>.Success(result);
        }


    }
}
