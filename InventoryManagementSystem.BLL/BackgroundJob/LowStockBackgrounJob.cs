using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
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

          var result=  await mediator.Send(new GetAllProductLessLowStockThresholdQuery());
            if (result != null && result.Any())
            {
                foreach (var product in result)
                {
                    var command = new SendLowStockNotificationCommand
                    {
                        ProductId = product.ID,
                        ProductName = product.Name,
                        Quantity = product.Quantity
                    };
                    await mediator.Send(command);
                }
            }
        }
    }
}
