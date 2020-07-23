using System;
using System.Collections.Generic;
using System.Text;
using AnkiLookup.Core.Helpers.Formatters;
using Newtonsoft.Json;

namespace AnkiLookup.Core.Models
{
    public class Word
    {
        public class Entry
        {
            public string ActualWord { get; set; }

            public string Label { get; set; }

            public List<Block> Definitions { get; set; } = new List<Block>();

            public Entry() { }

            public Entry(string actualWord, string label, Block block = null)
            {
                ActualWord = actualWord;
                Label = label;
                if (block != null)
                    Definitions.Add(block);
            }
        }

        public class Block
        {
            public string Definition { get; set; }

            public List<string> Examples { get; set; } = new List<string>();

            public Block(string definition)
            {
                Definition = definition;
            }
        }

        public string InputWord { get; set; }

        public List<Entry> Entries { get; set; } = new List<Entry>();

        public DateTime ImportDate { get; set; }

        public string AsFormatted(IWordFormatter formatter)
        {
            var entryBuilder = new StringBuilder();
            if (!(formatter is HtmlFormatter))
            {
                entryBuilder.Append(!string.IsNullOrWhiteSpace(InputWord) ? InputWord : Entries[0].ActualWord);
                entryBuilder.Append("\t");
            }
            entryBuilder.AppendLine(formatter.Render(this));

            return entryBuilder.ToString();
        }

        public Word Clone()
        {
            var cloned = new Word
            {
                InputWord = InputWord,
                ImportDate = ImportDate
            };
            cloned.Entries.AddRange(Entries);
            return cloned;
        }

        public static string Serialize(Word[] words)
        {
            return JsonConvert.SerializeObject(words, Config.JsonFormatting);
        }

        public static Word[] Deserialize(string content)
        {
            return JsonConvert.DeserializeObject<Word[]>(content);
        }
    }
}