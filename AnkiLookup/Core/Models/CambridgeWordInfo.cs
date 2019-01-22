using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using AnkiLookup.Core.Helpers;

namespace AnkiLookup.Core.Models
{
    public class CambridgeWordInfo
    {
        public class Entry
        {
            public string ActualWord { get; set; }

            public string Label { get; set; }

            public List<Block> Definitions { get; set; }
        }

        public class Block
        {
            public string Definition { get; set; }

            public List<string> Examples { get; set; }
        }

        public string InputWord { get; set; }

        public List<Entry> Entries { get; set; }

        public DateTime ImportedIntoAnki { get; set; }

        public bool AddedBefore { get; set; }

        public CambridgeWordInfo()
        {
            Entries = new List<Entry>();
        }

        public static byte[] Serialize(CambridgeWordInfo[] wordInfos)
        {
            var objs = new List<object>();
            objs.Add(wordInfos.Length);

            for (int wordInfoIndex = 0; wordInfoIndex < wordInfos.Length; wordInfoIndex++)
            {
                objs.Add(wordInfos[wordInfoIndex].InputWord);
                objs.Add(wordInfos[wordInfoIndex].AddedBefore);
                objs.Add(wordInfos[wordInfoIndex].ImportedIntoAnki);

                objs.Add(wordInfos[wordInfoIndex].Entries.Count);
                foreach (var entry in wordInfos[wordInfoIndex].Entries)
                {
                    objs.Add(entry.ActualWord);

                    if (string.IsNullOrWhiteSpace(entry.Label))
                        objs.Add(string.Empty);
                    else
                        objs.Add(entry.Label);

                    objs.Add(entry.Definitions.Count);
                    foreach (var definition in entry.Definitions)
                    {
                        objs.Add(definition.Definition);

                        var examplesCount = definition.Examples == null ? 0 : definition.Examples.Count;
                        objs.Add(examplesCount);
                        if (examplesCount == 0)
                            continue;

                        foreach (var example in definition.Examples)
                            objs.Add(example);
                    }
                }
            }
            return Serializer.Serialize(objs.ToArray());
        }

        public static CambridgeWordInfo[] Deserialize(byte[] data)
        {
            var objs = Serializer.Deserialize(data);

            var objOffsetIndex = 0;
            var wordInfosCount = (int)objs[objOffsetIndex];

            var wordInfos = new CambridgeWordInfo[wordInfosCount];
            for (int i = 0; i < wordInfosCount; i++)
            {
                var cambridgeWordInfo = new CambridgeWordInfo();

                objOffsetIndex++;
                cambridgeWordInfo.InputWord = objs[objOffsetIndex].ToString();

                objOffsetIndex++;
                cambridgeWordInfo.AddedBefore = (bool)objs[objOffsetIndex];

                objOffsetIndex++;
                cambridgeWordInfo.ImportedIntoAnki = (DateTime) objs[objOffsetIndex];

                objOffsetIndex++;
                var entriesCount = (int)objs[objOffsetIndex];
                for (int entryIndex = 0; entryIndex < entriesCount; entryIndex++)
                {
                    var entry = new Entry();

                    objOffsetIndex++;
                    entry.ActualWord = objs[objOffsetIndex].ToString();
                    objOffsetIndex++;
                    entry.Label = objs[objOffsetIndex].ToString();

                    entry.Definitions = new List<Block>();

                    objOffsetIndex++;
                    var blocksCount = (int)objs[objOffsetIndex];
                    for (int blockIndex = 0; blockIndex < blocksCount; blockIndex++)
                    {
                        var block = new Block();

                        objOffsetIndex++;
                        block.Definition = objs[objOffsetIndex].ToString();

                        var examples = new List<string>();
                        objOffsetIndex++;
                        var examplesCount = (int)objs[objOffsetIndex];
                        for (int exampleIndex = 0; exampleIndex < examplesCount; exampleIndex++)
                        {
                            objOffsetIndex++;
                            examples.Add(objs[objOffsetIndex].ToString());
                        }
                        block.Examples = examples;
                        entry.Definitions.Add(block);
                    }

                    cambridgeWordInfo.Entries.Add(entry);
                }

                wordInfos[i] = cambridgeWordInfo;
            }
            return wordInfos;
        }

        public string AsFormatted(IWordInfoFormatter formatter)
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
    }
}