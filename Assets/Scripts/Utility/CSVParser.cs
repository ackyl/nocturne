using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CsvParser
{
    public List<string[]> ParseCsv(string csvContent)
    {
        var lines = csvContent.Split('\n');
        var rows = new List<string[]>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            // Regular expression to match CSV values, including quoted strings with commas
            var matches = Regex.Matches(line, @"(?<=^|,)(?:""([^""]*)""|([^,]*))");

            var row = new List<string>();
            foreach (Match match in matches)
            {
                // Use the captured group for quoted or unquoted values
                row.Add(match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value);
            }

            rows.Add(row.ToArray());
        }

        return rows;
    }
}
