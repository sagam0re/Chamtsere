using Chamtsere.Application.Common.Interfaces;
using Chamtsere.Application.Common.Models;
using Chamtsere.Domain.Entities.User;

namespace Chamtsere.Application.Features.UserFeature.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result>
{
    private readonly ICurrentUser _currentUser;
    private readonly IChamtsereDbContext _context;

    public LoginCommandHandler(
        ICurrentUser currentUser,
        IChamtsereDbContext context
        )
    {
        _currentUser = currentUser;
        _context = context;
    }
    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.ExternalAuthId == _currentUser.ExternalAuthId, cancellationToken);

        if (user is null)
        {
            var newUser = new User
            {
                UserName = _currentUser.UserName,
                Email = _currentUser.Email,
                FirstName = _currentUser.FirstName,
                LastName = _currentUser.LastName
            };
            newUser.SetExternalAuthId(_currentUser.ExternalAuthId);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);

            user = newUser;
        }
        return Result.Success(user);
    }
}
