using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Account
{
    
    public class GenerateTokenCommand:IRequest<string>
    {
        public ApplicationUser user { get; set; } = null!;
        public DateTime Expired { get; set; }
    }
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, string>
    {
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;

        public GenerateTokenCommandHandler(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        public async Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {

            string jti = Guid.NewGuid().ToString();
            string userID = request.user.Id.ToString();
            var userRoles = await userManager.GetRolesAsync(request.user);

            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userID),
                new Claim(ClaimTypes.Name, request.user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,jti)
            };
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    claim.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            //----------------------------------
            SymmetricSecurityKey signKey =
                new(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            SigningCredentials signingcredential = new SigningCredentials
                (signKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken myToken = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                expires: request.Expired,
                claims: claim,
                signingCredentials: signingcredential
                );


            return new JwtSecurityTokenHandler().WriteToken(myToken);
           
        }
    }

}
