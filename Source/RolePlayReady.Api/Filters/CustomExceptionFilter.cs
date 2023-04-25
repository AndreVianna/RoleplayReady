namespace RolePlayReady.Api.Filters;

internal class CustomExceptionFilter : IExceptionFilter {
    private readonly ILogger _logger;

    public CustomExceptionFilter(ILoggerFactory loggerFactory) {
        _logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
    }

    public void OnException(ExceptionContext context) {
        var exception = context.Exception;
        _logger.LogError(exception, "An unhandled exception occurred.");

        context.Result = new JsonResult("An unexpected error occurred.") {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        context.ExceptionHandled = true;
    }
}