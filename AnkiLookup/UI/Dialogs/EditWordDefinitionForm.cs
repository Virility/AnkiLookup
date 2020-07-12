using AnkiLookup.Core.Models;
using System.Windows.Forms;
using AnkiLookup.UI.Controls;
using System;

namespace AnkiLookup.UI.Dialogs
{
    public partial class EditWordDefinitionForm : Form
    {
        public static int RemoveDefinitionViewItem(DefinitionViewItem definitionViewItem)
        {
            var listView = definitionViewItem.ListView;
            var word = definitionViewItem.Word;
            var entryIndex = definitionViewItem.EntryIndex;
            var definitionIndex = definitionViewItem.DefinitionIndex;

            var entryDeleted = false;
            if (word.Entries.Count > 0)
            {
                word.Entries[entryIndex].Definitions.RemoveAt(definitionIndex);

                if (word.Entries[entryIndex].Definitions.Count == 0)
                {
                    word.Entries.RemoveAt(entryIndex);
                    entryDeleted = true;
                }
            }
            listView.Items.Remove(definitionViewItem);

            for (int currentIndex = 0; currentIndex < listView.Items.Count; currentIndex++)
            {
                definitionViewItem = listView.Items[currentIndex] as DefinitionViewItem;
                var currentEntryIndex = definitionViewItem.EntryIndex;
                var currentDefinitionIndex = definitionViewItem.DefinitionIndex;

                if (entryDeleted && currentEntryIndex > entryIndex)
                    definitionViewItem.EntryIndex--;
                else if (entryIndex == currentEntryIndex && currentDefinitionIndex > definitionIndex)
                    definitionViewItem.DefinitionIndex--;
            }

            return definitionIndex;
        }
        
        private readonly Word _word;
        private readonly DefinitionViewItem _definitionViewItem;

        public readonly Word BackupWord;

        public string Definition { get { return rtbDefinition.Text; } set { rtbDefinition.Text = value; } }

        public EditWordDefinitionForm(DefinitionViewItem definitionViewItem, Job job)
        {
            InitializeComponent();

            _word = definitionViewItem.Word;
            _definitionViewItem = definitionViewItem;

            tbWord.Text = _word.InputWord;

            Word.Entry entry;
            if (job == Job.Add)
            {
                if (_word.Entries.Count == 0)
                {
                    definitionViewItem.EntryIndex = 0;
                    definitionViewItem.DefinitionIndex = 0;
                    entry = new Word.Entry(_word.InputWord, cbLabel.Items[0].ToString());
                    _word.Entries.Add(entry);
                }
                else
                {
                    definitionViewItem.EntryIndex = 0;
                    entry = _word.Entries[definitionViewItem.EntryIndex];
                    definitionViewItem.DefinitionIndex = entry.Definitions.Count;
                }
                entry.Definitions.Add(new Word.Block(string.Empty));
            }
            else
            {
                entry = _word.Entries[definitionViewItem.EntryIndex];
                rtbDefinition.Text = entry.Definitions[definitionViewItem.DefinitionIndex].Definition;
            }

            cbLabel.SelectedItem = entry.Label;
        }

        private void cbLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            
            var oldEntry = _word.Entries[_definitionViewItem.EntryIndex];
            var newLabel = cbLabel.Text.Trim();
            if (string.IsNullOrWhiteSpace(newLabel) || oldEntry.Label == newLabel)
                return;

            var wordString = oldEntry.ActualWord;
            var block = oldEntry.Definitions[_definitionViewItem.DefinitionIndex];

            var listView = _definitionViewItem.ListView;
            listView.BeginUpdate();

            // Remove
            RemoveDefinitionViewItem(_definitionViewItem);

            // Reorganize
            var (labelEntryIndex, count) = GetLabelInfo(newLabel);
            if (count == 0)
            {
                _word.Entries.Add(new Word.Entry(wordString, newLabel, block));
                _definitionViewItem.EntryIndex = labelEntryIndex - 1;
            }
            else
            {
                _word.Entries[labelEntryIndex].Definitions.Add(block);
                _definitionViewItem.EntryIndex = labelEntryIndex;
            }
            _definitionViewItem.DefinitionIndex = count;
            _definitionViewItem.Refresh();

            // Add
            var destination = GetLastDefinitionIndexFromEntryIndex(labelEntryIndex);
            listView.Items.Insert(destination, _definitionViewItem);
            _definitionViewItem.EnsureVisible();

            listView.EndUpdate();

            (int entryIndex, int count) GetLabelInfo(string label)
            {
                int entryIndex = 0;
                for (; entryIndex < _word.Entries.Count; entryIndex++)
                {
                    var entry = _word.Entries[entryIndex];
                    if (entry.Label == label)
                        return (entryIndex, count: entry.Definitions.Count);
                }
                return (entryIndex: entryIndex + 1, count: 0);
            }

            int GetLastDefinitionIndexFromEntryIndex(int entryIndex)
            {
                var index = 0;
                for (; index < _word.Entries.Count; index++)
                {
                    index += _word.Entries[index].Definitions.Count;
                    if (index == entryIndex)
                        break;
                }
                return index - 1;
            }
        }
    }
}