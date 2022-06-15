namespace Genzai.Core.Model.Request.Base;

/// <summary>
/// Api error result model.
/// </summary>
public class ApiResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult"/> class.
    /// </summary>
    /// <param name="resultCode">Result Code</param>
    /// <param name="message">Message</param>
    public ApiResult(int resultCode, string message)
    {
        this.ResultCode = resultCode;
        this.Message = message;
    }

    /// <summary>
    /// Gets or sets ResultCode.
    /// </summary>
    public int ResultCode { get; private set; }

    /// <summary>
    /// Gets or sets message to respond.
    /// </summary>
    public string Message { get; private set; }
}