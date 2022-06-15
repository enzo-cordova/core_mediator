namespace Genzai.Core.Vault
{
    /// <summary>
    /// Bean with authentication information
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VaultAuthenticationInfo
    {
        /// <summary>
        /// Hostname of vault server
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Token for access vault server
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Path of secrets
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Mount point of secrets
        /// </summary>
        public string MountPoint { get; set; }

        /// <summary>
        /// Expired days token
        /// </summary>
        public int RenewTokenInDays { get; set; }

        /// <summary>
        /// Retry tome to renew token
        /// </summary>
        public int RetryRenewTokenInHours { get; set; }
    }
}