using PeiFeira.Application.Services;
using PeiFeira.Communication.Requests.Usuario;
using PeiFeira.Domain.Interfaces;

namespace PeiFeira.Application.Services.Usuarios.Services;

public class UsuarioValidatorService : IUsuarioValidator
{
    private readonly ValidationService _validationService;
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioValidatorService(ValidationService validationService, IUnitOfWork unitOfWork)
    {
        _validationService = validationService;
        _unitOfWork = unitOfWork;
    }

    public async Task ValidateCreateRequestAsync(CreateUsuarioRequest request)
    {
        await _validationService.ValidateAsync(request);
    }

    public async Task ValidateUpdateRequestAsync(UpdateUsuarioRequest request)
    {
        await _validationService.ValidateAsync(request);
    }

    public async Task ValidateLoginRequestAsync(LoginRequest request)
    {
        await _validationService.ValidateAsync(request);
    }

    public async Task ValidateMudarSenhaRequestAsync(MudarSenhaRequest request)
    {
        await _validationService.ValidateAsync(request);
    }

    public async Task ValidateUniquenessAsync(string matricula, string email)
    {
        if (await _unitOfWork.Usuarios.ExistsByMatriculaAsync(matricula))
            throw new InvalidOperationException("Matrícula já existe");

        if (await _unitOfWork.Usuarios.ExistsByEmailAsync(email))
            throw new InvalidOperationException("Email já existe");
    }
}