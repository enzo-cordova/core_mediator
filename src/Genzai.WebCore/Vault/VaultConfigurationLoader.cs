using Genzai.Core.Model.Configuration.Mapping;
using Genzai.Core.Vault;
using Genzai.EfCore.Constants;
using Microsoft.Extensions.Configuration;

namespace Genzai.WebCore.Vault;

public static class VaultConfigurationLoader
{
    /// <summary>
    /// Load
    /// </summary>
    /// <param name="configuration">Configuration</param>
    public static void Load(IConfiguration configuration)
    {
        new VaultConfiguration(configuration).Load();
        var aesConfig = configuration.GetSection(AesConfiguration.Section).Get<AesConfiguration>();
        Environment.SetEnvironmentVariable(EncryptionConstants.AesProvideKey, aesConfig.Key);
        Environment.SetEnvironmentVariable(EncryptionConstants.AesProvideIv, aesConfig.IV);
    }
}
