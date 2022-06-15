namespace Genzai.Core.Inflector;

/// <summary>
/// Class for apply regular expressions in inflector class.
/// </summary>
internal class Rule
{
    /// <summary>
    /// Regular Expression.
    /// </summary>
    private readonly Regex regex;

    /// <summary>
    /// Replacement String.
    /// </summary>
    private readonly string replacement;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rule"/> class.
    /// </summary>
    /// <param name="pattern">String pattern.</param>
    /// <param name="replacement">String Replacement.</param>
    public Rule(string pattern, string replacement)
    {
        this.regex = new Regex(pattern, RegexOptions.IgnoreCase);
        this.replacement = replacement;
    }

    /// <summary>
    /// Method tha apply regular expression.
    /// </summary>
    /// <param name="word">string to analyze.</param>
    /// <returns>Returns string.</returns>
    public string Apply(string word)
    {
        if (!this.regex.IsMatch(word))
        {
            return null;
        }

        return this.regex.Replace(word, this.replacement);
    }
}