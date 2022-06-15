using Genzai.WebCore.Contexts;
using Genzai.WebCore.LocalDatas;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog.Enrichers;
using System.Diagnostics;

namespace Genzai.WebCore.RequestFilters;

/// <summary>
/// Correlation Id filter
/// </summary>
public class CorrelationIdFilter : IActionFilter, IOrderedFilter
{
    private static readonly string CorrelationHeader = typeof(CorrelationIdEnricher).Name + "+CorrelationId";

    public int Order { get; } = int.MaxValue - 10;

    /// <inheritdoc/>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        string correlationId = (string)context.HttpContext!.Items[CorrelationHeader];
        string rootId = Activity.Current!.RootId!;
        if (correlationId == null)
        {
            correlationId = Guid.NewGuid().ToString();
            context.HttpContext!.Items[CorrelationHeader] = correlationId;
        }
        AplicationThreadLocalData.ThreadLocal.InsightContext = new InsightContext(DateTime.UtcNow.Millisecond, rootId, null, correlationId);
    }

    /// <inheritdoc/>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        AplicationThreadLocalData.ThreadLocal.InsightContext = null;
    }


}
