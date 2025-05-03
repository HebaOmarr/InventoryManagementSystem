
using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.Entities.Enums;
using System.Net;

namespace InventoryManagementSystem.API.MiddleWare
{
    public class GlobalErrorHandler : IMiddleware
    {
        ILogger<GlobalErrorHandler> _logger;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                string errorMessages = string.Join(", ", ex.Message);
                ErrorResponse<string> errorResponse = new ErrorResponse<string>
                {
                    Message = errorMessages,
                    StatusCode = context.Response.StatusCode
                };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
