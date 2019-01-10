using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Similar to String.Format() but allows usage of named keys and takes a Dictionary to fill the placeholders.
/// If a placeholder does not have a key in the dictionary it will remain unchanged in the result.
/// </summary>
static class AdvancedStringFormatting {

    public static string format(string template, Dictionary<string, string> dictionary) {
        string formatted = template;
        foreach (string key in dictionary.Keys) {
            formatted = Regex.Replace(formatted, "{" + key + "}", dictionary[key]);
        }

        return formatted;
    }
}