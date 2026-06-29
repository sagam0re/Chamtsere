using Chamtsere.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Chamtsere.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success(null)
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}
