namespace Ims.Application.Logins;

public class LoginCommand : IRequest<ApplicationResponse<LoginResponse>>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
