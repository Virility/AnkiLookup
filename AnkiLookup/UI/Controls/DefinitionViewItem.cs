using AnkiLookup.Core.Models;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class DefinitionViewItem : ListViewItem
    {
        public int EntryIndex { get; set; } = -1;

        public int DefinitionIndex { get; set; } = -1;

        private Word.Entry _entry;

        private Word _word;
        public Word Word {
            get { return _word; }
            set {
                _word = value;
                Refresh();
            }
        }

        public void Refresh()
        {
            SubItems.Clear();

            if (_word == null || EntryIndex == -1)
            {
                Text = _word.InputWord;
                SubItems.Add("Not given.");
                SubItems.Add("Not given.");
                SubItems.Add("Not given.");
                return;
            }

            _entry = _word.Entries[EntryIndex];

            Text = _entry.ActualWord;
            SubItems.Add(_entry.Label);

            if (DefinitionIndex == -1)
                return;

            var definitionBlock = _entry.Definitions[DefinitionIndex];
            if (definitionBlock.Examples == null)
                SubItems.Add("No examples.");
            else
                SubItems.Add(definitionBlock.Examples.Count.ToString());
            SubItems.Add(definitionBlock.Definition);
        }

        public DefinitionViewItem(Word word, int entryIndex, int definitionIndex)
        {
            EntryIndex = entryIndex;
            DefinitionIndex = definitionIndex;
            Word = word;
        }

        public DefinitionViewItem(Word word)
        {
            Word = word;
        }
    }
}
