namespace RolePlayReady.Api.Utilities;

[ExcludeFromCodeCoverage]
internal class CustomExceptionFilter(ILoggerFactory loggerFactory, IWebHostEnvironment env) : IExceptionFilter {
    private readonly IWebHostEnvironment _env = env;
    private readonly ILogger _logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
    private const string _errorMessage = "An unhandled exception occurred.";

    public void OnException(ExceptionContext context) {
        var exception = context.Exception;
        _logger.LogError(exception, _errorMessage);

        context.Result = new ObjectResult(new ProblemDetails {
            Title = _env.IsDevelopment() ? _errorMessage : exception.Message,
            Detail = _env.IsDevelopment() ? exception.ToString() : exception.Message,
            Status = (int)HttpStatusCode.InternalServerError,
            Instance = context.HttpContext.Request.Path,
            Type = $"https://httpstatuses.com/{(int)HttpStatusCode.InternalServerError}",
        });
        context.ExceptionHandled = true;
    }
}