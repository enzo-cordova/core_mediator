using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace Genzai.Core.Vault
{
    /// <summary>
    /// Provider for vault configuration
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VaultConfiguration
    {
        private readonly IVaultClient? _client;
        private readonly IConfiguration? _configuration;
        private readonly VaultAuthenticationInfo? _aInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">configuration</param>
        public VaultConfiguration(IConfiguration configuration)
        {
            _aInfo = configuration.GetSection("Vault").Get<VaultAuthenticationInfo>();
            if (_aInfo is not null)
            {
                _client = GetVaultClient(this._aInfo);
            }

            _configuration = configuration;
        }

        /// <summary>
        /// GetVaultClient
        /// </summary>
        /// <param name="aInfo"></param>
        /// <returns></returns>
        private static IVaultClient GetVaultClient(VaultAuthenticationInfo aInfo)
        {
            IAuthMethodInfo authMethod = GetAuthMethodToken(aInfo);
            VaultClientSettings vaultClientSettings = new VaultClientSettings(aInfo.Address, authMethod);
            return new VaultClient(vaultClientSettings);
        }

        private static IAuthMethodInfo GetAuthMethodToken(VaultAuthenticationInfo aInfo)
        {
            return new TokenAuthMethodInfo(aInfo.Token);
        }

        /// <summary>
        /// Load vault configuration
        /// </summary>
        public void Load()
        {
            if (_aInfo is null)
            {
                return;
            }
            var config = ConfigurationExtensions.AsEnumerable(_configuration);
            var secrets = ReadSecretsAsync().Result;
            foreach (var kv in config)
            {
                _configuration!.GetSection(kv.Key).Value = ReplaceString(kv.Value, secrets);
            }
        }

        private static string ReplaceString(string value, Secret<SecretData> secrets)
        {
            var re = new Regex(LocalStrings.PatternVault);
            if (value is not null && re.IsMatch(value))
            {
                var match = re.Match(value);
                var scripted = match.Groups[2].Value;
                value = value.Replace("$vault(" + scripted + ")", ReadSecretValue(scripted, secrets));
                return ReplaceString(value, secrets);
            }
            return value!;
        }

        private static string ReadSecretValue(string value, Secret<SecretData> secrets)
        {
            return secrets.Data!.Data!.ContainsKey(value!) ? secrets.Data!.Data[value!].ToString()! : value;
        }

        private async Task<Secret<SecretData>> ReadSecretsAsync()
        {
            return await this._client!.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: _aInfo!.Path,
                    mountPoint: _aInfo.MountPoint);
        }
    }
}