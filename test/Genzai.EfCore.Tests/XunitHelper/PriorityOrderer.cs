namespace Genzai.EfCore.Tests.XunitHelper;

/// <summary>
/// Priority orderer class for Xunit.
/// </summary>
public class PriorityOrderer : ITestCaseOrderer
{
    /// <summary>
    /// Method for ordering test cases.
    /// </summary>
    /// <typeparam name="TTestCase">Test case type.</typeparam>
    /// <param name="testCases">Test case list.</param>
    /// <returns>Ordered test cases.</returns>
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

        foreach (TTestCase testCase in testCases)
        {
            int priority = 0;

            foreach (IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes(
                         typeof(TestPriorityAttribute).AssemblyQualifiedName))
            {
                priority = attr.GetNamedArgument<int>("Priority");
            }

            PriorityOrderer.GetOrCreate(sortedMethods, priority).Add(testCase);
        }

        foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
        {
            list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));

            foreach (TTestCase testCase in list)
            {
                yield return testCase;
            }
        }
    }

    private static TValue GetOrCreate<TKey, TValue>(
        IDictionary<TKey, TValue> dictionary, TKey key)
        where TValue : new()
    {
        if (dictionary.TryGetValue(key, out TValue result)) return result;

        result = new TValue();
        dictionary[key] = result;

        return result;
    }
}