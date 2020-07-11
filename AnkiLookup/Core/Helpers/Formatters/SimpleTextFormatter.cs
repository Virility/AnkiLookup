using System.Text;
using AnkiLookup.Core.Models;

namespace AnkiLookup.Core.Helpers.Formatters
{
    public class SimpleTextFormatter : IWordFormatter
    {
        public string Render(Word word)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < word.Entries.Count; i++)
            {
                var entry = word.Entries[i];

                if (i != 0 && word.Entries.Count > 1 && word.Entries[i - 1].ActualWord != entry.ActualWord)
                    sb.AppendLine(entry.ActualWord);

                if (!string.IsNullOrWhiteSpace(entry.Label))
                    sb.AppendLine($"[{entry.Label}]");

                foreach (var definition in entry.Definitions)
                {
                    sb.AppendLine(definition.Definition);
                    if (definition.Examples == null)
                        continue;

                    foreach (var example in definition.Examples)
                    {
                        if (!string.IsNullOrWhiteSpace(example))
                            sb.AppendLine($" -> \"{example}\"");
                    }
                }

                sb.AppendLine();
            }

            var sbText = sb.ToString().Replace("\r\n", "<br>");
            return sbText.Substring(0, sbText.Length - 1);
        }
    }
}