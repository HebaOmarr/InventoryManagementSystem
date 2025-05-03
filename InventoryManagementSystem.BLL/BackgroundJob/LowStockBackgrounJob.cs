using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.BackgroundJob
{
    public class LowStockBackgrounJob 
    {
        private readonly IMediator mediator;

        public LowStockBackgrounJob(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task RunTask() {
        
        
        }
    }
}
