using Genzai.Core.Domain.Model;
using Genzai.Security.Enums;

namespace Genzai.Security.Domain
{
    /// <summary>
    /// Permission Class
    /// </summary>
    public class Permission : AuditableEntity<Permission, long>, IAuditable
    {
        //Default constructor
        public Permission()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">name of permission</param>
        public Permission(string name)
        {
            Name = name;

            var pType = Enum.Parse<PermissionTypes>(name, ignoreCase: true);
            Value = (long)pType;

            Roles = new HashSet<Role>();
        }

        /// <summary>
        /// Permission
        /// </summary>
        /// <param name="id"></param>
        public Permission(long id)
        {
            Id = id;
            CreatedInformation("AUTOMATIC");
        }

        /// <summary>
        /// List of roles assigned to this permission.
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Name of permission
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// bitwise value of permission
        /// </summary>
        public long Value { get; set; }
    }
}