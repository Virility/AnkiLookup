using System.Text;
using System.Web;
using AnkiLookup.Core.Models;

namespace AnkiLookup.Core.Helpers
{
    public class SimpleTextFormatter : IWordInfoFormatter
    {
        public string Render(CambridgeWordInfo wordInfo)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < wordInfo.Entries.Count; i++)
            {
                var entry = wordInfo.Entries[i];

                if (i != 0 && wordInfo.Entries.Count > 1 && wordInfo.Entries[i - 1].ActualWord != entry.ActualWord)
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