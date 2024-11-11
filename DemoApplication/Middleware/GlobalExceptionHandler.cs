using DemoApplication.Business;
using Newtonsoft.Json;
using System.Net;

namespace DemoApplication.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> logger)
        {
                _requestDelegate = requestDelegate;
                _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured: {0}", ex.Message);
                await HandleExceptionAsync(ex, context);
            }
        }

        public async Task HandleExceptionAsync(Exception ex, HttpContext httpContext)
        {
            var response = new  ErrorResponse
            {
                Message = ex.Message,
            
            };
            switch(httpContext.Response.StatusCode)
            {
                case (int)HttpStatusCode.InternalServerError:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; break;
                case (int)HttpStatusCode.BadRequest:
                    response.StatusCode = (int)HttpStatusCode.BadRequest; break;
                case (int)HttpStatusCode.NotFound:
                    response.StatusCode = (int)HttpStatusCode.NotFound; break;
                default: response.StatusCode = (int)HttpStatusCode.InternalServerError; break;
            }

             await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
