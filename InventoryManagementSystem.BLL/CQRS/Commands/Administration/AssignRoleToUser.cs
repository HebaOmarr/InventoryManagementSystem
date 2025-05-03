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
    public class AssignRoleResponse
    {
        public string message { get; set; }
        public bool IsSucess { get; set; }
    }
    public class AssignRoleToUser : IRequest<AssignRoleResponse>
    {
       public UserRoleDTO UserRoleDTO { get; set; }
    }
    public class AssignRoleHandler : IRequestHandler<AssignRoleToUser, AssignRoleResponse>
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AssignRoleHandler(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<AssignRoleResponse> Handle(AssignRoleToUser request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserRoleDTO.RoleName) || string.IsNullOrEmpty(request.UserRoleDTO.UserName))
            {
                return new AssignRoleResponse
                {
                    IsSucess = false,
                    message = "Role name and User name cannot be empty."
                };
           
            }
            bool exit = await _roleManager.RoleExistsAsync(request.UserRoleDTO.RoleName);
       
            ApplicationUser? user = await _userManager.FindByNameAsync(request.UserRoleDTO.UserName);
            if (!exit || user == null)
            {
                return new AssignRoleResponse
                {
                    IsSucess = false,
                    message = "User || Role Not Found"
                };
            }
            IdentityResult result = await _userManager.AddToRoleAsync(user, request.UserRoleDTO.RoleName);
            if (result.Succeeded)
            {
                return   new AssignRoleResponse
                {
                    IsSucess = true,
                    message = $"Role '{request.UserRoleDTO.RoleName}' assigned to user '{request.UserRoleDTO.UserName}' successfully."
                };

            }
            return new AssignRoleResponse
            {
                IsSucess = false,
                message = "Failed to assign role to user."
            };

        }
    }

}
