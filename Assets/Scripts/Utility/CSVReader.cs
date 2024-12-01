using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public static CSVReader instance;

    // ---------------------------- //

    public List<Character> characterList = new List<Character>();

    // ---------------------------- //

    // [SerializeField] private TextAsset csvFile;

    // ---------------------------- //

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Character> TranslateCSVtoData(TextAsset csvFile)
    {
        string[] lines = csvFile.text.Split('\n');

        string[] headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++) // Skip header row
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue; // Skip empty lines

            string[] values = SplitCsvLine(lines[i]);

            Identity identity = (Identity)Enum.Parse(typeof(Identity), GetValue(headers, values, "identity"));

            Document doc = new()
            {
                name = GetValue(headers, values, "doc.name"),
                birth = GetValue(headers, values, "doc.birth"),
                nationality = GetValue(headers, values, "doc.nationality"),
                id = GetValue(headers, values, "doc.id"),
                contact = GetValue(headers, values, "doc.contact"),
                checkInDate = GetValue(headers, values, "doc.checkInDate"),
                checkOutDate = GetValue(headers, values, "doc.checkOutDate"),
                roomType = GetValue(headers, values, "doc.roomType"),
                request = GetValue(headers, values, "doc.request"),
                signature = bool.Parse(GetValue(headers, values, "doc.signature")),
                mark = bool.Parse(GetValue(headers, values, "doc.mark"))
            };

            Character character = new()
            {
                number = int.Parse(GetValue(headers, values, "number")),
                id = GetValue(headers, values, "id"),
                fullName = GetValue(headers, values, "fullName"),
                identity = identity,
                doc = doc
            };

            characterList.Add(character);
        }

        return characterList;
    }

    string GetValue(string[] headers, string[] values, string key)
    {
        int index = Array.FindIndex(headers, h => h.Trim() == key.Trim());

        return index >= 0 && index < values.Length ? values[index] : string.Empty;
    }

    private string[] SplitCsvLine(string line)
    {
        List<string> result = new List<string>();
        bool insideQuotes = false;
        StringBuilder currentValue = new StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
            {
                // Toggle the insideQuotes flag when encountering a quote
                insideQuotes = !insideQuotes;
            }
            else if (c == ',' && !insideQuotes)
            {
                // If not inside quotes and a comma is found, finalize the current value
                result.Add(currentValue.ToString());
                currentValue.Clear();
            }
            else
            {
                // Add characters to the current value
                currentValue.Append(c);
            }
        }

        // Add the last value
        result.Add(currentValue.ToString());

        return result.ToArray();
    }

}
