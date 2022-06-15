using System.Reflection;

namespace Genzai.WebCore.Utils;

/// <summary>
/// Reflection utils
/// </summary>
public static class ReflectionUtils
{

    /// <summary>
    /// It returns solution assemblies
    /// </summary>
    /// <returns>Solution assemblies</returns>
    public static Assembly[] GetSolutionAssemblies()
    {
        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
        List<Assembly> assemblies = new List<Assembly>();
        foreach (string file in files)
        {
            try
            {
                assemblies.Add(Assembly.Load(AssemblyName.GetAssemblyName(file)));
            }
            catch (Exception)
            {
                //Ignore
            }
        }
        return assemblies.ToArray();
    }
}
