using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AnkiLookup.Core.Models;
using AnkiLookup.Properties;

namespace AnkiLookup.Core.Helpers
{
    public class HtmlFormatter : IWordInfoFormatter
    {
        public string Render(CambridgeWordInfo wordInfo)
        {
            CambridgeWordInfo.Entry previousEntry = null;

            var blocksHtml = Resources.BlocksFormat;

            var entryBuilder = new StringBuilder();
            foreach (var entry in wordInfo.Entries)
            {
                var actualWordDoesntEqualInput = !string.Equals(entry.ActualWord, wordInfo.InputWord,
                    StringComparison.CurrentCultureIgnoreCase);
                var currentActualWordIsPreviousActualWord =
                    previousEntry != null && previousEntry.ActualWord != entry.ActualWord;

                var entryHtml = Resources.EntryFormat;

                if (actualWordDoesntEqualInput || currentActualWordIsPreviousActualWord)
                    entryHtml = entryHtml.Replace("{{ActualWord}}", $"<div class=\"word\">{entry.ActualWord}</div>");
                else
                    entryHtml = entryHtml.Replace("{{ActualWord}}", string.Empty);

                if (string.IsNullOrWhiteSpace(entry.Label))
                    entryHtml = entryHtml.Replace("<p class=\"label\">[{{Label}}]</p>", string.Empty);
                else
                    entryHtml = entryHtml.Replace("{{Label}}", entry.Label);

                var scopeBuilder = new StringBuilder();
                foreach (var definition in entry.Definitions)
                {
                    var scopeFormat = Resources.ScopeFormat.Replace("{{Definition}}", definition.Definition);
                    if (definition.Examples == null)
                        scopeFormat = scopeFormat.Replace("<div class=\"examples\">{{Examples}}</div>", string.Empty);
                    else
                    {
                        var examplesBuilder = new StringBuilder();
                        foreach (var example in definition.Examples)
                        {
                            if (string.IsNullOrWhiteSpace(example))
                                continue;

                            int offset = 0;
                            if (entry.ActualWord.StartsWith("-"))
                                offset = 1;

                            var pattern = $"\\b\\w*{entry.ActualWord.Substring(offset, entry.ActualWord.Length - (2 + offset))}\\w*\\b";
                            var replacement = "<span class=\"highlighted\">$&</span>";
                            var formattedExample = Regex.Replace(example, pattern, replacement, RegexOptions.IgnoreCase);
                            examplesBuilder.AppendLine(Resources.ExampleFormat.Replace("{{Example}}", formattedExample));
                        }
                        scopeFormat = scopeFormat.Replace("{{Examples}}", examplesBuilder.ToString());
                    }
                    scopeBuilder.Append(scopeFormat);
                }
                entryHtml = entryHtml.Replace("{{Scope}}", scopeBuilder.ToString());
                entryBuilder.AppendLine(entryHtml);

                previousEntry = entry;
            }
            
            return HtmlMinifier.Minify(blocksHtml.Replace("{{Blocks}}", entryBuilder.ToString()));
        }
    }
}