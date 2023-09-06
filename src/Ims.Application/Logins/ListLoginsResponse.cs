using Ims.Domain.Enums;

namespace Ims.Application.Logins;

public record ListLoginsResponse
{
    public required int Id { get; set; }

    public required string Email { get; set; }

    public required DateTimeOffset LoggedAt { get; set; }

    public required LoginStatus Status { get; set; }
}
