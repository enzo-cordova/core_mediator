using Genzai.Security.Domain;
using Genzai.Security.Extensions;
using Genzai.Security.MiddleWares; 
using Genzai.Security.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Claims; 
using Genzai.Core.Telemetry;
using Genzai.Security.Enums;
using Newtonsoft.Json;
using static Genzai.Security.Domain.User;  

namespace Genzai.Security.Services.Implementations
{
    /// <summary>
    /// Implementation
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IPermissionsService _permissionService;
        private readonly IAutoEnrollmentService<User> _autoEnrollmentService;
        private readonly ILogger<TokenService> _logger;
        private readonly ITelemetryProvider _telemetryProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="ae"></param>
        /// <param name="logger"></param>
        /// <param name="telemetryProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TokenService(IPermissionsService ps, IAutoEnrollmentService<User> ae, ILogger<TokenService> logger, ITelemetryProvider telemetryProvider)
        {
            _permissionService = ps ?? throw new ArgumentNullException(nameof(ps));
            _autoEnrollmentService = ae ?? throw new ArgumentNullException(nameof(ae));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telemetryProvider= telemetryProvider ?? throw new ArgumentNullException(nameof(telemetryProvider));
        }

        /// <summary>
        /// NewToken
        /// </summary>
        /// <param name="claimPrincipal"></param>
        /// <returns></returns>
        public async Task<GToken> NewToken(ClaimsPrincipal claimPrincipal)
        {
            var userCode = claimPrincipal.GetObjectId();
            var type = 0;
            var appidacr = claimPrincipal.Claims.SingleOrDefault(c => c.Type == "appidacr")?.Value;

            var azpacr = claimPrincipal.Claims.SingleOrDefault(c => c.Type == "azpacr")?.Value;

            if (!string.IsNullOrEmpty(appidacr))
                type = Convert.ToInt32(appidacr);
            if (!string.IsNullOrEmpty(azpacr))
                type = Convert.ToInt32(azpacr);

            var name = claimPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var familyName = claimPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
            var email = claimPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;

            var p = await _permissionService.GetPermissionCode(userCode, default(CancellationToken));
            _logger.LogInformation($"p: {p}");
            if (p.Permission < 0) //Auto-enrollment
            {
                var defaultRole = 4;
                var defaultPermissionValue = 4;
                User user;
                switch (type)
                {
                    case (int)Authenticated.Secret:
                    {
                        defaultRole = 1;
                        defaultPermissionValue = 1;
                        user = new User(defaultRole, false, name, familyName, userCode, email, (Authenticated)type);
                        _logger.LogInformation(
                            $"Auto-enrollment userCode --- DEFAULT_ROLE : {userCode} {defaultRole}");
                        await _autoEnrollmentService.AddUser(user, default(CancellationToken));
                        p.Permission = defaultPermissionValue;
                        break;
                    }
                    default:
                        user = new User(defaultRole, false, "service principal", "service principal", userCode, "service principal", (Authenticated)type);
                        _logger.LogInformation(
                            $"Auto-enrollment userCode --- DEFAULT_ROLE : {userCode} {defaultRole}");
                        await _autoEnrollmentService.AddUser(user, default(CancellationToken));
                        p.Permission = defaultPermissionValue;
                        break;
                }
             
            }
            _telemetryProvider.AddEventProperty("AutoEnrollment", p.ToString());
            _telemetryProvider.TrackEvent("NewToken");

            var oToken = new GToken()
            {
                PermissionValue = p.Permission,
                IsAdmin = p.ListRole.Any(c => c.Code == Constant.Constant.Admin),
                RoleCode = p.ListRole.Select(c => c.Code),
                Centers = p.ListCenter?.Where(c => c.Active).Select(c => c.Id) ,
                User = userCode
            };
            return  oToken ;
        }

        /// <summary>
        /// VerifyToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public Task<bool> VerifyToken(GToken token, string? userCode)
        {
  
            var bValid =  BelongsToCurrentUser(token.User, userCode);
            _telemetryProvider.AddEventProperty("TokenValid", bValid.ToString());
            _telemetryProvider.TrackEvent("VerifyToken");
            return Task.FromResult(bValid);
        }
        private static bool BelongsToCurrentUser(string? user, string? userCode)
        {
            return user == userCode;
        }
    }
}