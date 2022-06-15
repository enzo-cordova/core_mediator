namespace Genzai.Core.Extensions;

/// <summary>
/// Security extensions.
/// </summary>
public static class SecurityExtensions
{
    /// <summary>
    /// Add TCM JWT Authetication Bearer.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    [ExcludeFromCodeCoverage]
    public static void AddGenzaiAuthenticationBearer(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = configuration.GetSection(JwtBearerConfiguration.Section).Get<JwtBearerConfiguration>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }
}