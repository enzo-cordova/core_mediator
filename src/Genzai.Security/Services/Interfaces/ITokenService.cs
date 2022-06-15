using System.Security.Claims;
using Genzai.Security.MiddleWares;

namespace Genzai.Security.Services.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Gets a new token and optionally auto-enrolls a new user inserting it in the DB
        /// </summary>
        /// <param name="claimPrincipal"></param>
        /// <returns></returns>
        Task<GToken> NewToken(ClaimsPrincipal claimPrincipal);

        /// <summary>
        /// Verifies token
        /// </summary>
        /// <param name="gToken"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        Task<bool> VerifyToken(GToken gToken, string? userCode);
    }
}