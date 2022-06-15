using Genzai.Core.Domain.Model;

namespace Genzai.Security.Domain
{
    /// <summary>
    /// User class
    /// </summary>
    public class User : AuditableEntity<User, long>, IAuditable
    {
        /// <summary>
        /// Enum Type Authenticated
        /// </summary>
        public enum Authenticated : byte
        {
            /// <summary>
            /// public client
            /// </summary>
            Public = 0,

            /// <summary>
            /// client ID and client secret are used
            /// </summary>
            Secret = 1,

            /// <summary>
            /// certificate was used
            /// </summary>
            Certificate = 2
        }

        /// <summary>
        /// Constructor empty
        /// </summary>
        public User()
        { 
        }
        public User(long id)
        {
            Id = id;
            CreatedInformation("AUTOMATIC");
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="active"></param>
        /// <param name="name"></param>
        /// <param name="familyName"></param>
        /// <param name="code"></param>
        /// <param name="email"></param>
        /// <param name="typeAuthenticated"></param>
        public User(long roleId, bool active, string name, string familyName, string code, string email, Authenticated typeAuthenticated)
        {
            RoleId = roleId;
            Active = active;
            Name = name;
            FamilyName = familyName;
            Code = code;
            Email = email;
            TypeAuthenticated = typeAuthenticated;
            CreatedInformation($"{name} {familyName}");
        }

        /// <summary>
        /// RoleId
        /// </summary>
        public Authenticated TypeAuthenticated { get; private set; }

        /// <summary>
        /// RoleId
        /// </summary>
        public long RoleId { get; private set; }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Family Name
        /// </summary>
        public string FamilyName { get; private set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Rol
        /// </summary>
        public virtual Role Role { get; private set; }

        /// <summary>
        /// Update Rol Method
        /// </summary>
        /// <param name="rol"></param>
        public void UpdateRole(Role rol)
        {
            RoleId = rol.Id;
            Role = rol;
        }


        /// <summary>
        /// Centers
        /// </summary>
        public virtual ICollection<Center> Centers { get; private set; } = new List<Center>();
    }
}