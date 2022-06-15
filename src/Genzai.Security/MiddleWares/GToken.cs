using System.Diagnostics.CodeAnalysis;

namespace Genzai.Security.MiddleWares
{
    /// <summary>
    /// Token
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GToken
    {
        /// <summary>
        /// Value containing all the permissions set
        /// </summary>
        public long PermissionValue { get; set; }

        /// <summary>
        /// RoleCode
        /// </summary>
        public IEnumerable<string> RoleCode { get; set; } = new List<string>();


        /// <summary>
        /// Centers
        /// </summary>
        public IEnumerable<long>? Centers { get; set; }  


        /// <summary>
        /// IsAdmin
        /// </summary>
        public bool IsAdmin { get; set; } = false;


        /// <summary>
        /// User name
        /// </summary>
        public string? User { get; set; } 
    }
}