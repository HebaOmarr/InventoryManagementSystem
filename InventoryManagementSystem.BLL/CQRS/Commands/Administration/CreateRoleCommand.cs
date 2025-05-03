using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Administration
{
    public class CreateCommandResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }



    public class CreateRoleCommand :IRequest<CreateCommandResponse>
    {
        public string RoleName { get; set; }
       
  
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateCommandResponse>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<CreateCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
           
           if (string.IsNullOrEmpty(request.RoleName))
            {
                return new CreateCommandResponse { Message="Role name cannot be empty.",
                IsSuccess = false};
            }
            bool exit = await _roleManager.RoleExistsAsync(request.RoleName);
            if (exit)
            {
                return new CreateCommandResponse
                {
                    Message = $"Role '{request.RoleName}' already exists.",
                IsSuccess = false
                };
            }
            ApplicationRole newRole = new ApplicationRole
            {
                Name = request.RoleName,
                NormalizedName = request.RoleName.ToUpper()
            };
            IdentityResult result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            return new CreateCommandResponse
            {
                Message = $"Role '{request.RoleName}' created successfully.",
 IsSuccess = true
            };

            return new CreateCommandResponse
            {
                Message = "Failed to create role.",
                IsSuccess = false
            };
          
        }
    }
}
