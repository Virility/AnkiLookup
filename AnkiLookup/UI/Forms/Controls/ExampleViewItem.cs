using AnkiLookup.Core.Models;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class ExampleViewItem : ListViewItem
    {
        public int EntryIndex { get; set; }

        public int DefinitionIndex { get; set; }

        public int ExampleIndex { get; set; }


        private CambridgeWordInfo wordInfo;
        public CambridgeWordInfo WordInfo {
            get { return wordInfo; }
            set {
                wordInfo = value;
                if (wordInfo != null)
                {
                    var fixedCount = ExampleIndex + 1;
                    Text = fixedCount.ToString();

                    var entry = wordInfo.Entries[EntryIndex];
                    if (fixedCount <= entry.Definitions[DefinitionIndex].Examples.Count)
                        SubItems.Add(entry.Definitions[DefinitionIndex].Examples[ExampleIndex]);
                }
            }
        }

        public ExampleViewItem(CambridgeWordInfo wordInfo, int entryIndex, int definitionIndex, int exampleIndex = -1)
        {
            EntryIndex = entryIndex;
            DefinitionIndex = definitionIndex;
            ExampleIndex = exampleIndex;
            WordInfo = wordInfo;
        }

        public ExampleViewItem()
        {

        }
    }
}
