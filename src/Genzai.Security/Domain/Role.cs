using Genzai.Core.Domain.Model;

namespace Genzai.Security.Domain
{
    /// <summary>
    /// Role class
    /// </summary>
    public class Role : AuditableEntity<Role, long>, IAuditable 
    {

        //Default constructor
        public Role()
        {
        }

        public Role(long id)
        {
            Id = id;
            CreatedInformation("AUTOMATIC");
        }

        /// <summary>
        /// List of permissions assigned to this role.
        /// </summary> 
        public virtual ICollection<Permission> Permissions { get; set; }

        /// <summary>
        /// PermissionList
        /// </summary>
        /// <returns></returns>
        public List<string> PermissionList()
        {
            List<string> list = new();

            foreach (Permission p in Permissions)
            {
                list.Add(p.Name);
            }

            return list;
        }

        /// <summary>
        /// List of users assigned to this role.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// isDefaultRole
        /// </summary>
        /// <returns></returns>
        public bool isDefaultRole()
        {
            return isDefaultRole(Id);
        }

        /// <summary>
        /// isDefaultRole
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isDefaultRole(long id)
        {
            switch (id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Name of role
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code of role
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// description of role
        /// </summary>
        public string Description { get; set; }
    }
}