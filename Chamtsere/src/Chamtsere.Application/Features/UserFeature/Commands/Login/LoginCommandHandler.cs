using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Application.Common.Models;
using Chamtsere.Application.Common.Services;

namespace Chamtsere.Application.Features.UserFeature.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IChamtsereDbContext _context;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(
        IIdentityService identityService,
        IChamtsereDbContext context,
        ITokenService tokenService
        )
    {
        _identityService = identityService;
        _context = context;
        _tokenService = tokenService;
    }
    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _identityService.FindByUserNameAsync(command.UserName);
        if (user == null)
        {
            return Result.Failure(["User Not Found."]);
        }
        var isPasswordValid = await _identityService.CheckPasswordAsync(user.Id, command.Password);
        if (!isPasswordValid)
        {
            return Result.Failure(["Invalid password."]);
        }
        var roles = await _identityService.GetRolesAsync(user.Id);

        var token = await _tokenService.GenerateAccessToken(user, roles);

        return Result.Success(token);
    }
}
