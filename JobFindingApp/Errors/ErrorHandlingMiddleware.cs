using AppLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobFindingApp.Errors
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                

                var response = context.Response;

                switch (error)
                {
                    case AppException appEx:
                        _logger.LogError(appEx, appEx.Message);
                        response.StatusCode = (int)appEx.Code;
                        break;
                    case Exception e:
                        _logger.LogError(e,"Something went wrong");
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = JsonConvert.SerializeObject(new { response.StatusCode, Error = error?.Message });

                response.ContentType = "application/json";

               await response.WriteAsync(result);
            }
        }
    }
}
