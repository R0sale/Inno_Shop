using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Entities.Exceptions;
using System.Text.Json;

namespace UserService.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            ServiceUnavailableException => StatusCodes.Status503ServiceUnavailable,
                            UnauthorizedException => StatusCodes.Status401Unauthorized,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            ForbiddenException => StatusCodes.Status403Forbidden,
                            NotFoundException => StatusCodes.Status404NotFound,
                            ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        if (contextFeature.Error is ValidationAppException exception)
                        {
                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                exception.Errors
                            }));

                        }
                        else
                        {
                            await context.Response.WriteAsync(
                                new ErrorDetails()
                                {
                                    StatusCode = context.Response.StatusCode,
                                    Message = contextFeature.Error.Message
                                }.ToString());
                        }
                    }
                });
            });
        }
    }
}
