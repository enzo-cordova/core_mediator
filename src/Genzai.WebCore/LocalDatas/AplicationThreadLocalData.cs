namespace Genzai.WebCore.LocalDatas;

/// <summary>
/// Applicaton thread local data
/// </summary>
public class AplicationThreadLocalData
{
    static ThreadLocal<AplicationThreadData> _ThreadLocal = new ThreadLocal<AplicationThreadData>(() => new AplicationThreadData());

    /// <summary>
    /// Constructor
    /// </summary>
    protected AplicationThreadLocalData()
    {
        //Empty constructor
    }
    /// <summary>
    /// Thread local data
    /// </summary>
    public static AplicationThreadData ThreadLocal
    {
        get { return _ThreadLocal.Value!; }
    }
}
