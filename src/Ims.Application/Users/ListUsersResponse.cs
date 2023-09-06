namespace Ims.Application.Users;

public record ListUsersResponse
{
    public required int Id { get; set; }

    public required string Email { get; set; }
}
