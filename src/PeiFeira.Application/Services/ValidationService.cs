using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace PeiFeira.Application.Services;

public class ValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(T entity) where T : class
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();

        if (validator == null)
            return; // Sem validador, sem validação

        var result = await validator.ValidateAsync(entity);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    public async Task ValidateAsync<T>(T entity, IValidator<T> validator) where T : class
    {
        var result = await validator.ValidateAsync(entity);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}
