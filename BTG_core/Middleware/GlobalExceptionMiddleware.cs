using BTG_core.Models;
using Newtonsoft.Json;

namespace BTG_core.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var returnedErrorMessage = ex.Message;
            var code = 500; 

            if (ex is NotFoundException) code = 404;
            else if (ex is InvalidRequestException) code = 400;



            var result = JsonConvert.SerializeObject(new { transactionId = Guid.NewGuid(), error = returnedErrorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
