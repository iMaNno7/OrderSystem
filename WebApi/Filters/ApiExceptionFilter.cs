using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models;

namespace WebApi.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            var errors = new ApiErros();
            errors.Message = context.Exception.Message;
            errors.Details = context.Exception.StackTrace;
            context.Result = new ObjectResult(errors)
            {
                StatusCode = 500
            };
        }
    }
}
