using Genzai.Core.Attributes;
using Genzai.Core.Domain.Model;

namespace Genzai.Security.Domain;
public class Center : AuditableEntity<Center, long>, IAuditable
{

    //Default constructor
    public Center()
    {
    }

    public Center(long id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Active
    /// </summary>
    public bool Active { get; set; }


    /// <summary>
    /// Customer identifier
    /// </summary>
    public long? CustomerId { get; set; }

 
    /// <summary>
    /// List of users having access to this Center.
    /// </summary>
    public virtual ICollection<User> Users { get; set; }

    /// <summary>
    /// Name of center
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// CIF
    /// </summary>
    public string CenterCode { get; set; }

    /// <summary>
    /// Type of Address
    /// </summary>
    public string AddressStreetType { get; set; }

    /// <summary>
    /// Address Type Code
    /// </summary>
    public string AddresStreettypeCode { get; set; }

    /// <summary>
    /// Address Province Name
    /// </summary>
    public string AddressProvinceName { get; set; }

    /// <summary>
    /// Address Population Name
    /// </summary>
    public string AddressPopulationName { get; set; }

    /// <summary>
    /// Address Floor
    /// </summary>
    public string AddressFloor { get; set; }

    /// <summary>
    /// Address Number
    /// </summary>
    public string AddressNumber { get; set; }

    /// <summary>
    /// Adreess Street Name
    /// </summary>
    public string AddressStreetName { get; set; }

    /// <summary>
    /// Address Post Code
    /// </summary>
    public string AdrressPostCode { get; set; }

    /// <summary>
    /// CRM identifier
    /// </summary>
    public string CrmId { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Is center ready to receive alarms
    /// </summary>
    public bool Assigned { get; set; } = false;
}