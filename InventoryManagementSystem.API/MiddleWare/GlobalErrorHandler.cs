
namespace InventoryManagementSystem.API.MiddleWare
{
    public class GlobalErrorHandler : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
