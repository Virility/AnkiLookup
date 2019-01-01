using AnkiLookup.Core.Models;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class DefinitionViewItem : ListViewItem
    {
        public int EntryIndex { get; set; }

        public int DefinitionIndex { get; set; }

        private CambridgeWordInfo.Entry _entry;

        private CambridgeWordInfo _wordInfo;
        public CambridgeWordInfo WordInfo {
            get { return _wordInfo; }
            set {
                _wordInfo = value;
                InvalidateRow();
            }
        }

        private void InvalidateRow()
        {
            if (_wordInfo == null)
                return;

            _entry = _wordInfo.Entries[EntryIndex];
            SubItems.Clear();

            Text = _entry.ActualWord;    
            SubItems.Add(_entry.Label);

            var definitionBlock = _entry.Definitions[DefinitionIndex];
            if (definitionBlock.Examples == null)
                SubItems.Add("No examples.");
            else
                SubItems.Add(definitionBlock.Examples.Count.ToString());
            SubItems.Add(definitionBlock.Definition);
        }

        public DefinitionViewItem(CambridgeWordInfo wordInfo, int entryIndex, int definitionIndex)
        {
            EntryIndex = entryIndex;
            DefinitionIndex = definitionIndex;
            WordInfo = wordInfo;
        }

        public DefinitionViewItem()
        {
       
        }
    }
}
