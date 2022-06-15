using Genzai.Security.Domain;

namespace Genzai.Security.Model;
/// <summary>
/// AuthorizationUser
/// </summary>
public record AuthorizationUser
{

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="permission"></param>
    /// <param name="listRole"></param>
    /// <param name="listCenter"></param>
    public AuthorizationUser(long permission, IEnumerable<Role> listRole, IEnumerable<Center>? listCenter) => (Permission, ListRole, ListCenter) = (permission,listRole, listCenter);
    /// <summary>
    /// 
    /// </summary>
    public long Permission { get; set; }

    /// <summary>
    /// ListRole
    /// </summary>
    public IEnumerable<Role> ListRole { get; set; } = new List<Role>();


    /// <summary>
    /// ListCenter
    /// </summary>
    public IEnumerable<Center>? ListCenter { get; set; }
}
