using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Notification
{
    public class LogNotification : INotificarionService
    {
        private readonly ILogger logger;

        public LogNotification(ILogger<LogNotification> logger)
        {
            this.logger = logger;
        }

        public Task NotifyLowStock(int productId, string productName, int quantity)
        {
            logger.LogWarning($"Low stock: {productId} - {productName} (Qunatity: {quantity})");
            return Task.CompletedTask;
        }

       
    }
    
}
