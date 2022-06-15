using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genzai.WebCore.Initializers;

/// <summary>
/// Database initializer
/// </summary>
public class DatabaseInitializer
{
    private const string InitialData = "resources/InitialData/data.sql";

    /// <summary>
    /// Constructor
    /// </summary>
    protected DatabaseInitializer()
    {
    }

    /// <summary>
    /// t executes actions on init
    /// </summary>
    /// <param name="app">App</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="context">Database context</param>
    public static void Init(IApplicationBuilder app, IConfiguration configuration, DbContext context)
    {
        if (configuration.GetValue<bool>("UpgradeDatabase"))
        {
            UpgradeDatabase(app, context);
        }
        InsertInitalDatabaseData(app, context);
    }

    private static void UpgradeDatabase(IApplicationBuilder app, DbContext context)
    {
        using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
        {
            if (context != null && context.Database != null)
            {
                context.Database.Migrate();
            }
        }
    }

    private static void InsertInitalDatabaseData(IApplicationBuilder app, DbContext context)
    {
        using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
        {
            if (context != null && context.Database != null)
            {
                IEnumerable<string> sqlLines = GetInitialSql();
                if (sqlLines == null)
                {
                    return;
                }
                foreach (string line in sqlLines)
                {
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("--"))
                    {
                        context.Database.ExecuteSqlRaw(line);

                    }
                }
            }
        }
    }
    private static IEnumerable<string> GetInitialSql()
    {
        string basePath = Directory.GetCurrentDirectory();
        string path = basePath + Path.DirectorySeparatorChar + InitialData;
        if (File.Exists(path))
        {
            return File.ReadLines(path);
        }
        return new List<string>();
    }
}
