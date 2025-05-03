using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystem.API.ViewModel
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }
        public T? Data { get; private set; }
        public int? StatusCode { get; private set; }


        private Result(bool isSuccess, T? data, string? error, int statusCode)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T data,int statuscode=200)
        {
            return new Result<T>(true, data, null, statuscode);
        }

        public static Result<T> Failure(string error,int statuscode=400)
        {
            return new Result<T>(false, default(T), error, statuscode);
        }

    }
}
