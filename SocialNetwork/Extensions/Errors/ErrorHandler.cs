using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialNetwork.Extensions.Errors
{
    public class ErrorHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            
            ErrorModel error;

            if (context.Exception is ValidationException)
            {
                error = new ErrorModel(context.Exception.Message, null,400);
            }
            else
            {
                error = new ErrorModel(context.Exception.Message, context.Exception.ToString(),500);
            }

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(error);
            context.ExceptionHandled = true;
            Console.WriteLine(context.Exception.ToString());
        }
    }
}