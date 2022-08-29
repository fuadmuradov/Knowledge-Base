using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using KnowledgeBase.Service.Exceptions;

namespace KnowledgeBase.API.ServiceExtensions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    int statusCode = 500;
                    string message = "Internal Server Error!";
                    if (contextFeature != null)
                    {
                        message = contextFeature.Error.Message;
                        if (contextFeature.Error is ItemNotFoundException)
                        {
                            statusCode = 404;
                        }
                        else if (contextFeature.Error is RecordDublicateException)
                        {
                            statusCode = 409;
                        }
                       
                    }
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    string responseStr = JsonConvert.SerializeObject(new
                    {
                        code = statusCode,
                        Message = message
                    });
                    await context.Response.WriteAsync(responseStr).ConfigureAwait(false);
                });
            });

          
        }
    }
}
