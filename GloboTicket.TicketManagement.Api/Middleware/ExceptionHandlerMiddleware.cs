using CsvHelper.TypeConversion;
using GloboTicket.TicketManagement.Application.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace GloboTicket.TicketManagement.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next) 
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
                await ConvertException(context, ex);
            }

        }

        private Task ConvertException(HttpContext context, Exception ex) 
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = string.Empty;

            switch(ex)
            {
                case ValidatorException validatorException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validatorException.ValidationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException NotFound:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case Exception exeption:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result.IsNullOrEmpty()) 
            {
                result = JsonSerializer.Serialize(new {error = ex.Message});
            }

            return context.Response.WriteAsync(result);
        }
    }
}
