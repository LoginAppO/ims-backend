using Ims.Domain.Enums;

namespace Ims.Domain.Entities;

public class Login
{
    public int Id { get; set; }

    public required string Email { get; set; }

    public required DateTimeOffset LoggedAt { get; set; }

    public required LoginStatus Status { get; set; }
}
