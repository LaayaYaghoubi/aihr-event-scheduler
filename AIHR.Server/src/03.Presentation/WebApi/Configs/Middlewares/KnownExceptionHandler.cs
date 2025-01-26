using AIHR.EventScheduler.Contracts.BaseClasses;
using Microsoft.AspNetCore.Diagnostics;

namespace AIHR.EventScheduler.WebApi.Configs.Middlewares;

public class  KnownExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is KnownException knownException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(knownException.Message, cancellationToken);

            return true;
        }

        return false;
    }
}