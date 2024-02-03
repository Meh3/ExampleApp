using ErpApp.Common.Domain;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ErpApp.Api;

internal static class ExceptionHandlerExtension
{
    public static void UseExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = 500; // or another status code of your choice
                context.Response.ContentType = "application/json";

                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                if (exception is DomainValidationException)
                {
                    context.Response.StatusCode = 400; // or another status code of your choice
                }

                var result = JsonSerializer.Serialize(new { error = exception?.Message });
                await context.Response.WriteAsync(result);
            });
        });
    }
}
