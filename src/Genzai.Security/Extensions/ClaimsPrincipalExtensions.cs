using Microsoft.Identity.Web;
using System.Security.Claims;

namespace Genzai.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the user's Azure AD object id
    /// </summary>
    public static string GetObjectId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));
        return principal.FindFirstValue(ClaimConstants.ObjectId);
    }
}