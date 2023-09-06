namespace Ims.Application.Logins;

public record LoginResponse
{
    public required string Token { get; set; }
}
