using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models;

namespace WebApi.Filters
{
    public class ApiExeptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            error.Message = context.Exception.Message;
            error.Detail = context.Exception.StackTrace;

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
