using Genzai.Security.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Genzai.Security.Filters;

/// <summary>
/// Filter
/// </summary>
[ExcludeFromCodeCoverage]
public class HasPermissionAttribute : TypeFilterAttribute
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="permission"></param>
    /// <param name="propertyName"></param>
    public HasPermissionAttribute(PermissionTypes permission,string propertyName = "") : base(typeof(HasPermissionFilter))
    {
        Arguments = new object[] { permission, propertyName };
    }



}