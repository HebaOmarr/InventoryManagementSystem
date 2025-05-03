using InventoryManagementSystem.API.DTOs.Account;
using InventoryManagementSystem.Entities.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventoryManagementSystem.API.ViewModel;


namespace InventoryManagementSystem.BLL.CQRS.Commands.Account
{
    public class LoginCommandResponse
    {
     public  ApplicationUser ApplicationUser { get; set; } 
        public DateTime Expired { get; set; }  
    }
    public class LoginCommand :IRequest<LoginCommandResponse>
    {
      public  LoginRequest loginRequest { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        UserManager<ApplicationUser> _userManager;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.loginRequest.EmailAddress);
            if (user == null)
            {
                throw new Exception("User Not found");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.loginRequest.Password);
            if (result) {
                DateTime expireDate = request.loginRequest.RememberMe ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(3);
            }
            else
            {
                throw new Exception("Invalid Password");
            }

                LoginCommandResponse loginCommandResponse = new LoginCommandResponse()
                {
                    ApplicationUser = user,
                    Expired = DateTime.Now.AddHours(3)
                };
            return loginCommandResponse;
        }
    }

}
