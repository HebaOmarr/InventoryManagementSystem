using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Entities.Model
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
