namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Mock for test enum extensions
/// </summary>
[DataContract]
public enum CarsEnumTest
{
    /// <summary>
    /// Hatchback option.
    /// </summary>
    [EnumMember(Value = "Hatchback")]
    HatchBack = 0,

    /// <summary>
    /// CrossOver option.
    /// </summary>
    [EnumMember(Value = "CrossOver")]
    CrossOver = 1,

    /// <summary>
    /// Coupe Option (With empty value).
    /// </summary>
    [EnumMember(Value = "")]
    Coupe = 2,

    /// <summary>
    /// Sedan option (With null value).
    /// </summary>
    Sedan = 3,
}