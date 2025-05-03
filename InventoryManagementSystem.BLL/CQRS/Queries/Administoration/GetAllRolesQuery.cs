using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Administoration
{

    public class GetAllRolesQuery :IRequest<List<ApplicationRole>>
    {
     
    }
    public class GetallRolesQueryHandler : IRequestHandler<GetAllRolesQuery,List<ApplicationRole>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public GetallRolesQueryHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<ApplicationRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return roles;

        }

       
    }


}
