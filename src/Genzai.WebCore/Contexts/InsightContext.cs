namespace Genzai.WebCore.Contexts;

/// <summary>
/// Insight context
/// </summary>
public class InsightContext
{
    /// <summary>
    /// Message context
    /// </summary>
    /// <param name="time">Time</param>
    /// <param name="rootId">Root Id</param>
    /// <param name="operationId">Operation Id</param>
    /// <param name="correlationId">Correlation Id</param>
    public InsightContext(long time, string rootId, string operationId, string correlationId)
    {
        Time = time;
        RootId = rootId;
        OperationId = operationId != null ? operationId : GetOperationId(rootId);
        CorrelationId = correlationId;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public InsightContext()
    {
        Time = DateTime.UtcNow.Millisecond;
        RootId = Guid.NewGuid().ToString();
        OperationId = GetOperationId(RootId);
    }

    /// <summary>
    /// Root id
    /// </summary>
    public string RootId { get; set; }
    /// <summary>
    /// Operation id
    /// </summary>
    public string OperationId { get; set; }

    /// <summary>
    /// Correlation id
    /// </summary>
    public string CorrelationId { get; set; }
    /// <summary>
    /// Time
    /// </summary>
    public long Time { get; set; }


    /// <summary>
    /// Get operation id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Operation Id</returns>
    public static string GetOperationId(string id)
    {
        // Returns the root ID from the '|' to the first '.' if any.
        int rootEnd = id.IndexOf('.');
        if (rootEnd < 0)
            rootEnd = id.Length;

        int rootStart = id[0] == '|' ? 1 : 0;
        return id.Substring(rootStart, rootEnd - rootStart);
    }
}
