namespace Genzai.Core.Inflector;

/// <summary>
/// String Transformations.
/// </summary>
public static class Inflector
{
    /// <summary>
    /// List of plurals.
    /// </summary>
    private static readonly List<Rule> Plurals = new List<Rule>();

    /// <summary>
    /// list of Singulars.
    /// </summary>
    private static readonly List<Rule> Singulars = new List<Rule>();

    /// <summary>
    /// List of uncountables.
    /// </summary>
    private static readonly List<string> Uncountables = new List<string>();

    /// <summary>
    /// Initializes static members of the <see cref="Inflector"/> class.
    /// </summary>
    static Inflector()
    {
        AddPlural("$", "s");
        AddPlural("s$", "s");
        AddPlural("(ax|test)is$", "$1es");
        AddPlural("(octop|vir)us$", "$1i");
        AddPlural("(alias|status)$", "$1es");
        AddPlural("(bu)s$", "$1ses");
        AddPlural("(buffal|tomat)o$", "$1oes");
        AddPlural("([ti])um$", "$1a");
        AddPlural("sis$", "ses");
        AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
        AddPlural("(hive)$", "$1s");
        AddPlural("([^aeiouy]|qu)y$", "$1ies");
        AddPlural("(x|ch|ss|sh)$", "$1es");
        AddPlural("(matr|vert|ind)ix|ex$", "$1ices");
        AddPlural("([m|l])ouse$", "$1ice");
        AddPlural("^(ox)$", "$1en");
        AddPlural("(quiz)$", "$1zes");

        AddSingular("s$", string.Empty);
        AddSingular("(n)ews$", "$1ews");
        AddSingular("([ti])a$", "$1um");
        AddSingular("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
        AddSingular("(^analy)ses$", "$1sis");
        AddSingular("([^f])ves$", "$1fe");
        AddSingular("(hive)s$", "$1");
        AddSingular("(tive)s$", "$1");
        AddSingular("([lr])ves$", "$1f");
        AddSingular("([^aeiouy]|qu)ies$", "$1y");
        AddSingular("(s)eries$", "$1eries");
        AddSingular("(m)ovies$", "$1ovie");
        AddSingular("(x|ch|ss|sh)es$", "$1");
        AddSingular("([m|l])ice$", "$1ouse");
        AddSingular("(bus)es$", "$1");
        AddSingular("(o)es$", "$1");
        AddSingular("(shoe)s$", "$1");
        AddSingular("(cris|ax|test)es$", "$1is");
        AddSingular("(octop|vir)i$", "$1us");
        AddSingular("(alias|status)es$", "$1");
        AddSingular("^(ox)en", "$1");
        AddSingular("(vert|ind)ices$", "$1ex");
        AddSingular("(matr)ices$", "$1ix");
        AddSingular("(quiz)zes$", "$1");

        AddIrregular("person", "people");
        AddIrregular("man", "men");
        AddIrregular("child", "children");
        AddIrregular("sex", "sexes");
        AddIrregular("move", "moves");

        AddUncountable("equipment");
        AddUncountable("information");
        AddUncountable("rice");
        AddUncountable("money");
        AddUncountable("species");
        AddUncountable("series");
        AddUncountable("fish");
        AddUncountable("sheep");
    }

    /// <summary>
    /// Formats the string in Camel case.
    /// </summary>
    /// <param name="lowercaseAndUnderscoredWord">string. The word to format in Camel case.</param>
    /// <returns>string. The word in Camel case.</returns>
    public static string Camelize(string lowercaseAndUnderscoredWord)
    {
        return Uncapitalize(Pascalize(lowercaseAndUnderscoredWord));
    }

    /// <summary>
    /// Capitalizes the word.
    /// </summary>
    /// <param name="word">string. The word to capitalize.</param>
    /// <returns>The Capitalized word.</returns>
    public static string Capitalize(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return string.Empty;
        }
        else
        {
            return word.Substring(0, 1).ToUpperInvariant() + word.Substring(1);
        }
    }

    /// <summary>
    /// Replaces underscores with dashes in the string.
    /// </summary>
    /// <param name="underscoredWord">string. The word to dasherize.</param>
    /// <returns>The word with dashes instead of underscores.</returns>
    public static string Dasherize(string underscoredWord)
    {
        if (string.IsNullOrEmpty(underscoredWord))
        {
            return string.Empty;
        }
        else
        {
            return underscoredWord.Replace('_', '-');
        }
    }

    /// <summary>
    /// Capitalizes the first word and turns underscores into spaces and strips _id. Formats the word into
    /// human readable string.
    /// </summary>
    /// <param name="lowercaseAndUnderscoredWord">string. The word to humanize.</param>
    /// <returns>The humanized word.</returns>
    public static string Humanize(string lowercaseAndUnderscoredWord)
    {
        return Capitalize(Regex.Replace(lowercaseAndUnderscoredWord, "_", " "));
    }

    /// <summary>
    /// Ordinalize turns a number into an ordinal string used to denote the position in an ordered
    /// sequence such as 1st, 2nd, 3rd, 4th.
    /// </summary>
    /// <param name="number">string. The number to ordinalize.</param>
    /// <returns>string. The ordinalized number.</returns>
    public static string Ordinalize(string number)
    {
        int localNumber = int.Parse(number, NumberStyles.Integer, CultureInfo.InvariantCulture);
        int localNumberMod100 = localNumber % 100;

        if (localNumberMod100 >= 11 && localNumberMod100 <= 13)
        {
            return number + "th";
        }

        return (localNumber % 10) switch
        {
            1 => number + "st",
            2 => number + "nd",
            3 => number + "rd",
            _ => number + "th",
        };
    }

    /// <summary>
    /// Formats the string in pascal case.
    /// </summary>
    /// <param name="lowercaseAndUnderscoredWord">string. The word to Pascal case.</param>
    /// <returns>The word in Pascal case.</returns>
    public static string Pascalize(string lowercaseAndUnderscoredWord)
    {
        return Regex.Replace(
            lowercaseAndUnderscoredWord,
            "(?:^|_)(.)",
            (match) => match.Groups[1].Value.ToUpperInvariant());
    }

    /// <summary>
    /// Returns the plural form of the word in the string.
    /// </summary>
    /// <param name="word">string. The word to pluralize.</param>
    /// <returns>The pluralized word.</returns>
    public static string Pluralize(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return string.Empty;
        }
        else
        {
            return ApplyRules(Plurals, word);
        }
    }

    /// <summary>
    /// The reverse of <see cref="Pluralize"/>, returns the singular form of a word in a string.
    /// </summary>
    /// <param name="word">string. The word to singularize.</param>
    /// <returns>The singluralized word.</returns>
    public static string Singularize(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return string.Empty;
        }
        else
        {
            return ApplyRules(Singulars, word);
        }
    }

    /// <summary>
    /// Capitalizes all the words and replaces some characters in the string to create a nicer looking title.
    /// </summary>
    /// <param name="word">string. The word to titleize.</param>
    /// <returns>The titlized word.</returns>
    public static string Titleize(string word)
    {
        return Regex.Replace(
            Humanize(Underscore(word)),
            @"\b([a-z])",
            (match) => match.Captures[0].Value.ToUpperInvariant());
    }

    /// <summary>
    /// Revers of <see cref="Capitalize"/>.
    /// </summary>
    /// <param name="word">string. The word to un-capitalize.</param>
    /// <returns>Uncapitalized string.</returns>
    public static string Uncapitalize(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return string.Empty;
        }
        else
        {
            return word.Substring(0, 1).ToLowerInvariant() + word.Substring(1);
        }
    }

    /// <summary>
    /// Makes an underscored form from the expression in the string.
    /// </summary>
    /// <param name="pascalCasedWord">string. The word to underscore.</param>
    /// <returns>string. The word with underscore seperators.</returns>
    public static string Underscore(string pascalCasedWord)
    {
        return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2"), @"[-\s]", "_").ToLowerInvariant();
    }

    /// <summary>
    /// Add irregular method.
    /// </summary>
    /// <param name="singular">word in singular.</param>
    /// <param name="plural">word in plural.</param>
    private static void AddIrregular(string singular, string plural)
    {
        AddPlural("(" + singular[0] + ")" + singular.Substring(1) + "$", "$1" + plural.Substring(1));
        AddSingular("(" + plural[0] + ")" + plural.Substring(1) + "$", "$1" + singular.Substring(1));
    }

    /// <summary>
    /// Add plural in list.
    /// </summary>
    /// <param name="rule">string regex rule.</param>
    /// <param name="replacement">string replacement.</param>
    private static void AddPlural(string rule, string replacement)
    {
        Plurals.Add(new Rule(rule, replacement));
    }

    /// <summary>
    /// Add singular in list.
    /// </summary>
    /// <param name="rule">string regex rule.</param>
    /// <param name="replacement">string replacement.</param>
    private static void AddSingular(string rule, string replacement)
    {
        Singulars.Add(new Rule(rule, replacement));
    }

    /// <summary>
    /// Add uncountable words.
    /// </summary>
    /// <param name="word">string word.</param>
    private static void AddUncountable(string word)
    {
        Uncountables.Add(word.ToLowerInvariant());
    }

    /// <summary>
    /// Apply rules.
    /// </summary>
    /// <param name="rules">Rules list.</param>
    /// <param name="word">Word to find.</param>
    /// <returns>string result.</returns>
    private static string ApplyRules(List<Rule> rules, string word)
    {
        string result = word;

        if (!Uncountables.Contains(word.ToLowerInvariant()))
        {
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                if ((result = rules[i].Apply(word)) != null)
                {
                    break;
                }
            }
        }

        return result;
    }
}