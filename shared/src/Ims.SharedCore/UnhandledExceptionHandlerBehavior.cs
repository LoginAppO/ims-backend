using MediatR;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Ims.SharedCore;

public class UnhandledExceptionHandlerBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger _logger;

    public UnhandledExceptionHandlerBehavior(ILogger logger, IHostEnvironment environment)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.Error(ex, "Request: Unhandled Exception for {RequestName} {@Request}", requestName, request);

            var responseType = typeof(TResponse);
            Type? failedResponseType = null;
            TResponse? failedResponse = null;
            if (responseType == typeof(ApplicationResponse))
            {
                failedResponseType = responseType;
            }
            else if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(ApplicationResponse<>))
            {
                var genericArgType = responseType.GetGenericArguments().FirstOrDefault();
                if (genericArgType is not null)
                    failedResponseType = typeof(ApplicationResponse<>).MakeGenericType(genericArgType);
            }

            var errorMessage = _environment.IsProduction()
                ? "Internal server error"
                : ex.Message;
            if (failedResponseType is not null)
            {
                failedResponse = Activator.CreateInstance(
                    failedResponseType,
                    "InternalServerError",
                    new[] { errorMessage }) as TResponse;
            }

            if (failedResponse is not null)
                return failedResponse;
            throw;
        }
    }
}
