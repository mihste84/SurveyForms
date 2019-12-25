using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveyForms.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SurveyForms.Clients.API.Filters
{

    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var httpError = new HttpError
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = context.Exception.Message,
                StackTrace = _env.IsDevelopment() ? context.Exception.StackTrace : null
            };

            if (exceptionType == typeof(AppValidationException))
            {
                httpError.ValidationErrors = ((AppValidationException)context.Exception).Errors;
                httpError.StatusCode = HttpStatusCode.BadRequest;
            }

            if (exceptionType == typeof(NotFoundException))        
                httpError.StatusCode = HttpStatusCode.NotFound;
            

            if (exceptionType == typeof(ArgumentException) || exceptionType == typeof(ArgumentNullException))          
                httpError.StatusCode = HttpStatusCode.BadRequest;
            

            if(exceptionType == typeof(DbUpdateException) && context.Exception.InnerException is SqlException inner)
                httpError.Message = MessageFromSqlException(inner);
            

            context.Result = new JsonResult(httpError) { StatusCode = (int)httpError.StatusCode };
        }

        private string MessageFromSqlException(SqlException ex)
        => ex.Number switch
        {
            2601 => "The item you are trying to insert/update already exists.",
            _ => "An error occured when updating the database."
        };

        private class HttpError
        {
            public List<AppValidationException.ValidationError> ValidationErrors { get; set; }
            public IEnumerable<string> AuthErrors { get; set; }
            public string Message { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            public string StackTrace { get; set; }
        }
    }
}
