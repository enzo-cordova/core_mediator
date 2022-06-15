namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Aes.
/// </summary>
public class AesConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "Aes";

    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Iv
    /// </summary>
    public string IV { get; set; }
}