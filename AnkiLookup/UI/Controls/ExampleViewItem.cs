using AnkiLookup.Core.Models;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class ExampleViewItem : ListViewItem
    {
        public int EntryIndex { get; set; }

        public int DefinitionIndex { get; set; }

        public int ExampleIndex { get; set; }

        private Word word;

        public Word Word {
            get { return word; }
            set
            {
                word = value;
                if (word == null)
                    return;

                var fixedCount = ExampleIndex + 1;
                Text = fixedCount.ToString();

                var entry = word.Entries[EntryIndex];
                if (fixedCount <= entry.Definitions[DefinitionIndex].Examples.Count)
                    SubItems.Add(entry.Definitions[DefinitionIndex].Examples[ExampleIndex]);
            }
        }

        public string Example
        {
            get
            {
                return Word.Entries[EntryIndex].Definitions[DefinitionIndex].Examples[ExampleIndex];
            }
            set
            {
                var examples = Word.Entries[EntryIndex].Definitions[DefinitionIndex].Examples;
                if (examples.Count == 0)
                    examples.Add(value);
                else
                    examples[ExampleIndex] = value;
                Refresh();
            }
        }

        public void Refresh()
        {
            if (word == null)
                return;

            var i = 0;
            Text = Name = (ExampleIndex + 1).ToString();
            i++;

            if (Word.Entries[EntryIndex].Definitions[DefinitionIndex].Examples.Count == ExampleIndex)
                return;

            if (SubItems.Count > i)
                SubItems[i].Text = Example;
            else
                SubItems.Add(Example);
        }

        public ExampleViewItem(DefinitionViewItem definitionViewItem, string example)
        {
            Word = definitionViewItem.Word;
            EntryIndex = definitionViewItem.EntryIndex;
            DefinitionIndex = definitionViewItem.DefinitionIndex;

            Word.Entries[EntryIndex].Definitions[DefinitionIndex].Examples.Add(example);
            ExampleIndex = Word.Entries[EntryIndex].Definitions[DefinitionIndex].Examples.Count - 1;

            Refresh();
        }

        public ExampleViewItem(DefinitionViewItem definitionViewItem, int exampleIndex) 
        {
            Word = definitionViewItem.Word;
            EntryIndex = definitionViewItem.EntryIndex;
            DefinitionIndex = definitionViewItem.DefinitionIndex;
            ExampleIndex = exampleIndex;

            Refresh();
        }
    }
}
