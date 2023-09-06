using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Ims.Domain.Entities;
using Ims.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ims.Application.Logins;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApplicationResponse<LoginResponse>>
{
    private readonly ILoginRepository _loginRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(ILoginRepository loginRepository, IUserRepository userRepository, IConfiguration configuration)
    {
        _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
    }

    public async Task<ApplicationResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var foundUser = await _userRepository.GetByEmail(request.Email);
        if (foundUser == null)
        {
            await _loginRepository.Add(new Login { Email = request.Email, LoggedAt = DateTimeOffset.UtcNow, Status = LoginStatus.Fail });
            return ApplicationResponse.BadRequest<LoginResponse>("User doesn't exist!");
        }

        if(!BCrypt.Net.BCrypt.Verify(request.Password, foundUser.Password))
        {
            await _loginRepository.Add(new Login { Email = request.Email, LoggedAt = DateTimeOffset.UtcNow, Status = LoginStatus.Fail });
            return ApplicationResponse.BadRequest<LoginResponse>("Wrong password!");
        }

        await _loginRepository.Add(new Login { Email = request.Email, LoggedAt = DateTimeOffset.UtcNow, Status = LoginStatus.Success });

        var userToken = CreateToken(foundUser);

        return ApplicationResponse.Success(new LoginResponse { Token = userToken});
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtSettings:Key"] ?? string.Empty));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            "issuer",
            "audience",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
