using Genzai.Core.Vault;

namespace Genzai.Core.Tests.Vault
{
    /// <summary>
    /// VaultConfigurationTest
    /// </summary>
    public class VaultConfigurationTest
    {
        /// <summary>
        /// VaultConfiguration_when_GetConfigurationIsEmpty_throwError
        /// </summary>
        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void VaultConfiguration_when_GetConfigurationIsEmpty_throwError()
        {
            var mockIConfiguration = new Mock<IConfiguration>();
            Assert.Throws<ArgumentNullException>(() => new VaultConfiguration(mockIConfiguration.Object));
        }

        /// <summary>
        /// VaultConfiguration_when_GetValues_is_OK
        /// </summary>
        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void VaultConfiguration_when_GetValues_is_OK()
        {
            var appSettings = new Dictionary<string, string> {
                {"Vault:Address", "https://gcloud.prosegur.dev/"},
                {"Vault:Token", "eqweqwewq"},
                {"Vault:MountPoint", "gedge-nonprod"},
                {"Vault:Path", "gedge"},
                {"Vault:RenewTokenInDays", "365"},
                {"Vault:RetryRenewTokenInHours", "1000"},
                {"ConnectionStrings:AppConnection","Server=emaz-gedge-l-mysql-01.mysql.database.azure.com;UserID=gedge;Password=$vault(database_password);Database=gedge;SslMode=1;"},
                {"ConnectionStrings:Redis","EMAZ-GEDGE-L-REDIS-01.redis.cache.windows.net:6380,password=$vault(redis_password),ssl=True,abortConnect=False"},
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettings)
                .Build();
            Assert.NotNull(new VaultConfiguration(configuration));
        }

        /// <summary>
        /// VaultConfiguration_when_GetValues_is_OK
        /// </summary>
        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void VaultConfiguration_when_Load_is_OK()
        {
            var appSettings = new Dictionary<string, string> {
                {"Vault:Address", "https://gcloud.prosegur.dev/"},
                {"Vault:Token", "s.fgoawQvPNyGngMgqVmsMrWEu"},
                {"Vault:MountPoint", "gedge-nonprod"},
                {"Vault:Path", "gedge"},
                {"Vault:RenewTokenInDays", "365"},
                {"Vault:RetryRenewTokenInHours", "1000"},
                {"ConnectionStrings:AppConnection","Server=emaz-gedge-l-mysql-01.mysql.database.azure.com;UserID=gedge;Password=$vault(database_password);Database=gedge;SslMode=1;"},
                {"ConnectionStrings:Redis","EMAZ-GEDGE-L-REDIS-01.redis.cache.windows.net:6380,password=$vault(redis_password),ssl=True,abortConnect=False"},
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettings)
                .Build();
            var vaultAuthenticationHelper = new VaultConfiguration(configuration);

            void Act() => vaultAuthenticationHelper.Load();

            Assert.NotNull((Action)Act);
        }

        /// <summary>
        /// VaultConfiguration_when_GetValues_is_OK
        /// </summary>
        [Fact]
        [Trait("TestCategory", "UnitTest")]
        public void VaultConfiguration_when_ReadSecretsAsync_is_OK()
        {
            var appSettings = new Dictionary<string, string> {
                {"Vault:Address", "https://gcloud.prosegur.dev/"},
                {"Vault:Token", "s.fgoawQvPNyGngMgqVmsMrWEu"},
                {"Vault:RenewTokenInDays", "365"},
                {"Vault:RetryRenewTokenInHours", "1000"},
                {"ConnectionStrings:AppConnection","Server=emaz-gedge-l-mysql-01.mysql.database.azure.com;UserID=gedge;Password=$vault(database_password);Database=gedge;SslMode=1;"},
                {"ConnectionStrings:Redis","EMAZ-GEDGE-L-REDIS-01.redis.cache.windows.net:6380,password=$vault(redis_password),ssl=True,abortConnect=False"},
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettings)
                .Build();
            var vaultAuthenticationHelper = new VaultConfiguration(configuration);
            Assert.Throws<AggregateException>(() => vaultAuthenticationHelper.Load());
        }
    }
}