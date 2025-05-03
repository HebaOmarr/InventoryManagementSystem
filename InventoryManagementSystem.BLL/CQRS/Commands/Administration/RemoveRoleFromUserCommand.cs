using InventoryManagementSystem.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Administration
{
     public class REmoveRoleResponse
    {
        public string message { get; set; }
        public bool IsSucess { get; set; }
    }
    public class RemoveRoleFromUserCommand : IRequest<REmoveRoleResponse>
    {
        public UserRoleDTO UserRoleDTO { get; set; }
    }
    public class RemoveRoleHandler:IRequestHandler<RemoveRoleFromUserCommand, REmoveRoleResponse>
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RemoveRoleHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<REmoveRoleResponse> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.UserRoleDTO.RoleName) || string.IsNullOrEmpty(request.UserRoleDTO.UserName))
            {
                return new REmoveRoleResponse
                {
                    IsSucess = false,
                    message = "Role name and User name cannot be empty."
                };

            }
            bool exit = await _roleManager.RoleExistsAsync(request.UserRoleDTO.RoleName);
            ApplicationUser? user = await _userManager.FindByNameAsync(request.UserRoleDTO.UserName);
            if (!exit || user == null)
            {
                return new REmoveRoleResponse
                {
                    IsSucess = false,
                    message = "User || Role Not Found"
                };
            }
            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, request.UserRoleDTO.RoleName);
            if (result.Succeeded)
            {
                return new REmoveRoleResponse
                {
                    IsSucess = true,
                    message = $"Role '{request.UserRoleDTO.RoleName}' removed from user '{request.UserRoleDTO.UserName}' successfully."
                };

            }
            return new REmoveRoleResponse
            {
                IsSucess = false,
                message = "Failed to Delete role to user."
            };



        }
    }
    
}
