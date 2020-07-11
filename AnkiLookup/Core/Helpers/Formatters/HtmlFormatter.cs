using System;
using System.Text;
using System.Text.RegularExpressions;
using AnkiLookup.Core.Models;
using AnkiLookup.Properties;

namespace AnkiLookup.Core.Helpers.Formatters
{
    public class HtmlFormatter : IWordFormatter
    {
        public string Render(Word word)
        {
            Word.Entry previousEntry = null;

            var blocksHtml = Resources.BlocksFormat;

            var entryBuilder = new StringBuilder();

            for (int index = 0; index < word.Entries.Count; index++)
            {
                var actualWordDoesntEqualInput = !string.Equals(word.Entries[index].ActualWord, word.InputWord,
                    StringComparison.CurrentCultureIgnoreCase);
                var currentActualWordIsPreviousActualWord =
                    previousEntry != null && previousEntry.ActualWord != word.Entries[index].ActualWord;

                var entryHtml = Resources.EntryFormat;

                if (index == 0 && word.InputWord == word.Entries[0].ActualWord)
                    entryHtml = entryHtml.Replace("{{ActualWord}}", string.Empty);
                else if (!actualWordDoesntEqualInput && !currentActualWordIsPreviousActualWord)
                    entryHtml = entryHtml.Replace("{{ActualWord}}", string.Empty);
                else
                    entryHtml = entryHtml.Replace("{{ActualWord}}", $"<div class=\"word\">{word.Entries[index].ActualWord}</div>");

                if (string.IsNullOrWhiteSpace(word.Entries[index].Label))
                    entryHtml = entryHtml.Replace("<p class=\"label\">[{{Label}}]</p>", string.Empty);
                else
                    entryHtml = entryHtml.Replace("{{Label}}", word.Entries[index].Label);

                var scopeBuilder = new StringBuilder();
                foreach (var definition in word.Entries[index].Definitions)
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
                            if (word.Entries[index].ActualWord.StartsWith("-"))
                                offset = 1;

                            var pattern = $"\\b\\w*{word.Entries[index].ActualWord.Substring(offset, word.Entries[index].ActualWord.Length - (2 + offset))}\\w*\\b";
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

                previousEntry = word.Entries[index];
            }

            return HtmlMinifier.Minify(blocksHtml.Replace("{{Blocks}}", entryBuilder.ToString()));
        }
    }
}