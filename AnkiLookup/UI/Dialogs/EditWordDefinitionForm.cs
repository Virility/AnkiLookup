using AnkiLookup.Core.Models;
using System.Windows.Forms;
using AnkiLookup.UI.Controls;
using System;
using System.Linq;

namespace AnkiLookup.UI.Dialogs
{
    public partial class EditWordDefinitionForm : Form
    {
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
            cbLabel.SelectedValueChanged += new EventHandler(cbLabel_SelectedValueChanged);
        }

        private (int entryIndex, int count) GetLabelInfo(string label)
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

        private void nudDefinitionIndex_ValueChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(cbLabel.Text))
            //{
            //    MessageBox.Show("A label must be selected to classify this word.");
            //    return;
            //}
        }

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

        private void cbLabel_SelectedValueChanged(object sender, EventArgs e)
        {
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

            var oldEntry = _word.Entries[_definitionViewItem.EntryIndex];
            var wordString = oldEntry.ActualWord;
            var block = oldEntry.Definitions[_definitionViewItem.DefinitionIndex];

            var listView = _definitionViewItem.ListView;
            listView.BeginUpdate();

            // Remove
            RemoveDefinitionViewItem(_definitionViewItem);

            // Reorganize
            var label = cbLabel.SelectedItem.ToString();
            var (labelEntryIndex, count) = GetLabelInfo(label);
            if (count == 0)
            {
                _word.Entries.Add(new Word.Entry(wordString, label, block));
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
        }
    }
}
