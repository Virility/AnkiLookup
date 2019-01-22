using System;
using System.Text;
using System.Text.RegularExpressions;
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

            for (int index = 0; index < wordInfo.Entries.Count; index++)
            {
                var actualWordDoesntEqualInput = !string.Equals(wordInfo.Entries[index].ActualWord, wordInfo.InputWord,
                    StringComparison.CurrentCultureIgnoreCase);
                var currentActualWordIsPreviousActualWord =
                    previousEntry != null && previousEntry.ActualWord != wordInfo.Entries[index].ActualWord;

                var entryHtml = Resources.EntryFormat;

                if (index == 0 && wordInfo.InputWord == wordInfo.Entries[0].ActualWord)
                    entryHtml = entryHtml.Replace("{{ActualWord}}", string.Empty);
                else if (!actualWordDoesntEqualInput && !currentActualWordIsPreviousActualWord)
                    entryHtml = entryHtml.Replace("{{ActualWord}}", string.Empty);
                else
                    entryHtml = entryHtml.Replace("{{ActualWord}}", $"<div class=\"word\">{wordInfo.Entries[index].ActualWord}</div>");

                if (string.IsNullOrWhiteSpace(wordInfo.Entries[index].Label))
                    entryHtml = entryHtml.Replace("<p class=\"label\">[{{Label}}]</p>", string.Empty);
                else
                    entryHtml = entryHtml.Replace("{{Label}}", wordInfo.Entries[index].Label);

                var scopeBuilder = new StringBuilder();
                foreach (var definition in wordInfo.Entries[index].Definitions)
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
                            if (wordInfo.Entries[index].ActualWord.StartsWith("-"))
                                offset = 1;

                            var pattern = $"\\b\\w*{wordInfo.Entries[index].ActualWord.Substring(offset, wordInfo.Entries[index].ActualWord.Length - (2 + offset))}\\w*\\b";
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

                previousEntry = wordInfo.Entries[index];
            }

            return HtmlMinifier.Minify(blocksHtml.Replace("{{Blocks}}", entryBuilder.ToString()));
        }
    }
}