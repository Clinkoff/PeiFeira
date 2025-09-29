using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;
using PeiFeira.Communication.Responses;

namespace PeiFeira.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        switch (exception)
        {
            case ValidationException validationException:
                HandleValidationException(context, validationException);
                break;

            case InvalidOperationException invalidOpException:
                HandleInvalidOperationException(context, invalidOpException);
                break;

            case UnauthorizedAccessException:
                HandleUnauthorizedException(context);
                break;

            case KeyNotFoundException:
                HandleNotFoundException(context);
                break;

            default:
                HandleGenericException(context, exception);
                break;
        }
    }

    private void HandleValidationException(ExceptionContext context, ValidationException exception)
    {
        var errorMessages = exception.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
        var response = new ResponseErrorJson(errorMessages, 400);

        context.Result = new BadRequestObjectResult(response);
        context.ExceptionHandled = true;

        _logger.LogWarning("Validation error: {Errors}", string.Join(", ", exception.Errors.Select(e => e.ErrorMessage)));
    }

    private void HandleInvalidOperationException(ExceptionContext context, InvalidOperationException exception)
    {
        var response = new ResponseErrorJson(exception.Message, 409);

        context.Result = new ConflictObjectResult(response);
        context.ExceptionHandled = true;

        _logger.LogWarning("Business rule violation: {Message}", exception.Message);
    }

    private void HandleUnauthorizedException(ExceptionContext context)
    {
        var response = new ResponseErrorJson("Acesso não autorizado", 401);

        context.Result = new UnauthorizedObjectResult(response);
        context.ExceptionHandled = true;

        _logger.LogWarning("Unauthorized access attempt");
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var response = new ResponseErrorJson("Recurso não encontrado", 404);

        context.Result = new NotFoundObjectResult(response);
        context.ExceptionHandled = true;

        _logger.LogWarning("Resource not found");
    }

    private void HandleGenericException(ExceptionContext context, System.Exception exception)
    {
        var response = new ResponseErrorJson("Erro interno do servidor", 500);

        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;

        _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
    }
}
