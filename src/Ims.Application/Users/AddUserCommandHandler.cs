using BCrypt.Net;

namespace Ims.Application.Users;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ApplicationResponse<ListUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<ApplicationResponse<ListUsersResponse>> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var foundUser = await _userRepository.Any(command.Email);

        if (foundUser)
            return ApplicationResponse.BadRequest<ListUsersResponse>("User exists!");

        var user = new User { Email = command.Email, Password = command.Password };

        var validationResult = new UserValidator().Validate(user);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
            return ApplicationResponse.BadRequest<ListUsersResponse>(errorMessages);
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var addedUser = await _userRepository.Add(user);

        var result = new ListUsersResponse
        {
            Id = addedUser.Id,
            Email = addedUser.Email,
        };

        return ApplicationResponse.Success(result);
    }
}
