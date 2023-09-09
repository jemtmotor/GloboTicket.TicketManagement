using System.Runtime.CompilerServices;

namespace GloboTicket.TicketManagement.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder useCustomExceptionHandler(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
