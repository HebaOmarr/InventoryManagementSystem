using InventoryManagementSystem.BLL.Notification;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Products
{
    public class SendLowStockNotificationCommand:IRequest
    {
       public  int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
    public class SendLowStockNotificationCommandHandler : IRequestHandler<SendLowStockNotificationCommand>
    {
        private readonly INotificarionService notification;

        public SendLowStockNotificationCommandHandler(INotificarionService notification)
        {
            this.notification = notification;
        }
        public async Task Handle(SendLowStockNotificationCommand request, CancellationToken cancellationToken)
        {
           
          await  notification.NotifyLowStock(request.ProductId, request.ProductName, request.Quantity);
        }
    }
}
