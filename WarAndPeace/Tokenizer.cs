using System.Text.RegularExpressions;

namespace WarAndPeace;

public class Tokenizer
{ 
    public static IEnumerable<string> Tokenize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Enumerable.Empty<string>();
        }

        // Convert text to lower case
        string lowerText = text.ToLowerInvariant();

        // Define a regular expression pattern to match words
        const string pattern = @"\b[a-zA-Z]+(?:['-][a-zA-Z]+)*\b";

        // Use Regex to find matches
        MatchCollection matches = Regex.Matches(lowerText, pattern);

        // Use LINQ to select the matched words
        IEnumerable<string> words = matches.Cast<Match>()
            .Select(match => match.Value);

        return words;
    }
}