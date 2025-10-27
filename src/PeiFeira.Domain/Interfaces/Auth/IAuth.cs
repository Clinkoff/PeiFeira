namespace PeiFeira.Domain.Interfaces.Auth;

public interface IAuth
{
    Task<bool> VerifyPassword(string password, string hashedPassword);

}
