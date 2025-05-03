using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Entities.Model
{
    public class ApplicationUser : IdentityUser<int>
    {
        public String FirstName { get; set; } = null!;
        public String LastName { get; set; } = null!;

      
        public IEnumerable<InventoryTransaction>? TransferTransactions { get; set; }

        public static implicit operator ApplicationUser?(ApplicationRole? v)
        {
            throw new NotImplementedException();
        }
    }
}
