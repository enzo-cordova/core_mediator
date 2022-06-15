namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Send grid configuration map.
/// </summary>
public class SendGridConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "SendGrid";

    /// <summary>
    /// Api key.
    /// </summary>
    public string ApiKey { get; set; }
}