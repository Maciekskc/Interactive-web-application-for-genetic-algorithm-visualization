using Application.Infrastructure.Errors;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Utilities.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object errors = null;
            object errorsForClient;

            switch (ex)
            {
                case RestException re:
                    errors = re.Errors;
                    errorsForClient = re.Errors;
                    Logger.Error($"{(int)re.Code}, {re.Errors}", ex);
                    context.Response.StatusCode = (int)re.Code;
                    break;

                case DbUpdateException due:
                    errors = GetAllErrors(due);
                    errorsForClient = "Nastąpił błąd podczas aktualizacji bazy danych";
                    Logger.Error(errors, ex);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case AutoMapperMappingException amme:
                    errors = GetAllErrors(amme);
                    errorsForClient = "Nastąpił błąd podczas przetwarzania danych";
                    Logger.Error(errors, ex);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case Exception e:
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    errorsForClient = "Błąd serwera";
                    Logger.Error(errors, ex);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
#warning Na produkcji damy tutaj errorsForClient, ale na razie lepiej będzie Nam pracować na rzeczywistych errorach (tj szybciej będziemy diagnozować błędy).
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }

        private List<string> GetAllErrors(Exception innerException)
        {
            var errors = new List<string>();
            while (innerException != null)
            {
                errors.Add(innerException.Message);
                innerException = innerException.InnerException;
            }

            return errors;
        }
    }
}