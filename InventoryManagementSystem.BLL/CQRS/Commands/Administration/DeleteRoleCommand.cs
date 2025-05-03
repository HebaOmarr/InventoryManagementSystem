using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Administration
{
    public class DeleteCommandResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class DeleteRoleCommand : IRequest<DeleteCommandResponse>
    {
        public string RoleName { get; set; }
    }
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, DeleteCommandResponse>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<DeleteCommandResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {


            if (string.IsNullOrEmpty(request.RoleName))
            {
                return (new DeleteCommandResponse
                {
                    Message = "Role name cannot be empty.",
                    IsSuccess = false
                });
            }
            ApplicationRole? role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                return (new DeleteCommandResponse
                {
                    Message = $"Role '{request.RoleName}' not found.",
                    IsSuccess = false
                });
            }
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return (new DeleteCommandResponse
                {
                    Message = $"Role '{request.RoleName}' deleted successfully.",
                    IsSuccess =true
                });
            }
            string errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            return (new DeleteCommandResponse
            {
                Message = $"Failed to delete role. Errors: {errorMessages}",
                IsSuccess = false
            });



        }

    }
}


