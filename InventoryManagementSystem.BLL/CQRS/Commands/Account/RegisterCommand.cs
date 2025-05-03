using InventoryManagementSystem.API.DTOs.Account;
using InventoryManagementSystem.API.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Account
{
    public class RegisterCommandResponse
    {
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Expired { get; set; }
    }
    public class RegisterCommand : IRequest<Result<RegisterCommandResponse>>
    {
        public RegisterRequest registerRequest { get; set; }
 
    }
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterCommandResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            if (await _userManager.Users.AnyAsync(u => u.Email == request.registerRequest.EmailAddress))
            {
                return Result<RegisterCommandResponse>.Failure("Email is already registered.");
            }
            if (await _userManager.Users.AnyAsync(u => u.UserName == request.registerRequest.UserName))
            {
                return Result<RegisterCommandResponse>.Failure("UserName is already registered.");
            }
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = request.registerRequest.FirstName,
                LastName = request.registerRequest.LastName,
                UserName = request.registerRequest.UserName,
                Email = request.registerRequest.EmailAddress,
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.registerRequest.Password);
            if (result.Succeeded)
            {
                if (await _userManager.Users.AnyAsync()) await _userManager.AddToRoleAsync(user, "User");
                else await _userManager.AddToRoleAsync(user, "Admin");

                DateTime expired = DateTime.Now.AddHours(3);
            }
            else
            {
                string errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<RegisterCommandResponse>.Failure(errorMessages);
            }
            return Result<RegisterCommandResponse>.Success(new RegisterCommandResponse()
            {
                ApplicationUser = user,
                Expired = DateTime.Now.AddHours(3)
            });
        }
    }
}
