namespace Ims.Application.Users;

public class AddUserCommand : IRequest<ApplicationResponse<ListUsersResponse>>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
