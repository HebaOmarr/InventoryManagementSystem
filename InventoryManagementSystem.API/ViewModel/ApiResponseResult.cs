using InventoryManagementSystem.Entities.Enums;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem.API.ViewModel
{
    public  class ApiResponseResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public static SuccessResponse<T> Success(T data, string message = "Operation Success", int statusCode = 200)
        {
            return new SuccessResponse<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static ErrorResponse<T> Error( string message = "Error Happened", int statusCode = 400)
        {
            return new ErrorResponse<T>
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Data = default
            };
        }

    }
    public class SuccessResponse<T> : ApiResponseResult<T>
    {

    }

    public class ErrorResponse<T> : ApiResponseResult<T>
    {

    }




}
