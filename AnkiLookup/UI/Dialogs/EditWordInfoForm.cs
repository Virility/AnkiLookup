using AnkiLookup.Core.Models;
using AnkiLookup.Core.Providers;
using AnkiLookup.UI.Controls;
using AnkiLookup.UI.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.UI.Models
{
    public partial class EditWordInfoForm : Form
    {
        public Job Job { get; set; }

        public CambridgeWordInfo WordInfo { get; internal set; }

        public bool ChangeMade { get; internal set; }

        private readonly DeckManagementForm _sender;
        private readonly string _titleFormat;
        private readonly CambridgeProvider _cambridgeProvider;
        private readonly List<CambridgeWordInfo> _wordInfos;
        private int _lastIndex = -1;
        private bool _isCanceling;

        public EditWordInfoForm(DeckManagementForm sender, Job job, CambridgeProvider cambridgeProvider,
             CambridgeWordInfo wordInfo, ref List<CambridgeWordInfo> wordInfos)
        {
            _sender = sender;
            Job = job;
            _cambridgeProvider = cambridgeProvider;
            WordInfo = wordInfo;
            _wordInfos = wordInfos;

            InitializeComponent();
            Initialize();
            _titleFormat = Text;
        }

        private void Initialize()
        {
            bCancelOrDelete.Text = (Job == Job.Add) ? "Cancel" : "Delete";
        }

        private void RefreshWordInfoUI()
        {
            var definitionViewItems = new List<DefinitionViewItem>();
            for (int entryIndex = 0; entryIndex < WordInfo.Entries.Count; entryIndex++)
            {
                var definitions = WordInfo.Entries[entryIndex].Definitions;
                for (int definitionIndex = 0; definitionIndex < definitions.Count; definitionIndex++)
                    definitionViewItems.Add(new DefinitionViewItem(WordInfo, entryIndex, definitionIndex) { Tag = definitionViewItems.Count });
            }
            lvDefinitions.Items.AddRange(definitionViewItems.ToArray());
        }

        private string _inputWord;

        private void EditWordInfoForm_Load(object sender, EventArgs e)
        {
            _inputWord = WordInfo.InputWord;

            tbInputWord.Text = WordInfo.InputWord;
            tbInputWord_TextChanged(null, null);
            if (WordInfo == null)
                return;

            lvDefinitions.Items.Clear();
            lvExamples.Items.Clear();

            RefreshWordInfoUI();
        }

        private void EditWordInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isCanceling)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            if (tbInputWord.Text != _inputWord)
                ChangeMade = true;

            if (!ChangeMade)
            {
                DialogResult = DialogResult.Abort;
                return;
            }

            if (Job == Job.Add && _wordInfos.Any(wordInfo => wordInfo.InputWord == WordInfo.InputWord))
            {
                var dialogResult = MessageBox.Show(
                    "This word already exists in the database.\n" +
                    "Would you like to add anyway?\n" +
                    "Duplicates will not sync."
                    , "AnkiLookup", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.No || dialogResult == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Abort;
                    return;
                }
            }
            
            DialogResult = DialogResult.OK;
        }

        private void tbInputWord_TextChanged(object sender, EventArgs e)
        {
            WordInfo.InputWord = tbInputWord.Text.Trim().ToLower();
            Text = string.Format(_titleFormat, Job, WordInfo.InputWord);
        }

        private void tbInputWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bLookUp_Click(this, null);
        }

        private async void bLookUp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbInputWord.Text))
                return;

            var word = tbInputWord.Text.Trim().ToLower();
            try
            {
                var wordInfo = await _cambridgeProvider.GetWordInfo(word);
                if (wordInfo == null)
                {
                    MessageBox.Show($"\"{word}\" doesn't exist.");
                    return;
                }

                WordInfo = wordInfo;
                lvDefinitions.Items.Clear();
                lvExamples.Items.Clear();

                RefreshWordInfoUI();
                ChangeMade = true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Internal Error:: Word ({word}): {exception.Message}");
            }
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            _sender.CopiedWordInfo = WordInfo;
        }

        private void bPaste_Click(object sender, EventArgs e)
        {
            if (_sender.CopiedWordInfo == null)
                return;

            var pastedWordInfo = _sender.CopiedWordInfo;
            WordInfo.Entries = pastedWordInfo.Entries;
            ChangeMade = true;

            RefreshWordInfoUI();
        }

        private void lvEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDefinitions.SelectedItems.Count == 0)
            {
                lvExamples.Items.Clear();
                rtbOutput.Clear();
                return;
            }

            var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;
            if (_lastIndex == (int)definitionViewItem.Tag)
                return;
            _lastIndex = (int)definitionViewItem.Tag;

            if (lvExamples.Items.Count > 0)
                lvExamples.Items.Clear();

            var entryIndex = definitionViewItem.EntryIndex;
            var definitionIndex = definitionViewItem.DefinitionIndex;
            var examples = WordInfo.Entries[entryIndex].Definitions[definitionIndex].Examples;
            if (examples == null || examples.Count == 0)
                return;

            var exampleViewItems = new List<ExampleViewItem>();
            for (int exampleIndex = 0; exampleIndex < examples.Count; exampleIndex++)
                exampleViewItems.Add(new ExampleViewItem(WordInfo, entryIndex, definitionIndex, exampleIndex));
            lvExamples.Items.AddRange(exampleViewItems.ToArray());
        }

        private void ProcessEditWordDefinitionDialog(EditWordDefinitionForm dialog)
        {
            CambridgeWordInfo.Entry entry;
            if (WordInfo.Entries.Count != 0 && WordInfo.Entries.Count >= dialog.EntryIndex + 1)
            {
                entry = WordInfo.Entries[dialog.EntryIndex];
                if (entry.ActualWord == dialog.Word && entry.Label == dialog.Label)
                    return;
            }

            entry = new CambridgeWordInfo.Entry();
            entry.ActualWord = dialog.Word;
            entry.Definitions = new List<CambridgeWordInfo.Block>();
            entry.Label = dialog.Label;
            WordInfo.Entries.Add(entry);
        }

        private void RemoveDefinitionViewItem(DefinitionViewItem definitionViewItem)
        {
            var entryIndex = definitionViewItem.EntryIndex;
            var definitionIndex = definitionViewItem.DefinitionIndex;

            WordInfo.Entries[entryIndex].Definitions.RemoveAt(definitionIndex);
            lvDefinitions.Items.Remove(definitionViewItem);

            if (WordInfo.Entries[entryIndex].Definitions.Count == 0)
                WordInfo.Entries.RemoveAt(entryIndex);

            for (int currentIndex = 0; currentIndex < lvDefinitions.Items.Count; currentIndex++)
            {
                definitionViewItem = lvDefinitions.Items[currentIndex] as DefinitionViewItem;

                var currentEntryIndex = definitionViewItem.EntryIndex;
                if (currentEntryIndex > entryIndex)
                    definitionViewItem.EntryIndex--;

                var currentDefinitionIndex = definitionViewItem.DefinitionIndex;
                if (currentDefinitionIndex > definitionIndex)
                    definitionViewItem.DefinitionIndex--;
            }

            if (lvExamples.Items.Count > 0 && (lvExamples.Items[0] as ExampleViewItem).DefinitionIndex == definitionIndex)
                lvExamples.Items.Clear();

            rtbOutput.Text = string.Empty;
            ChangeMade = true;
        }

        private void tsmiAddDefinition_Click(object sender, EventArgs e)
        {
            var definitionViewItem = new DefinitionViewItem();
            lvDefinitions.Items.Add(definitionViewItem);

            using (var dialog = new EditWordDefinitionForm(WordInfo.Entries.Count, tbInputWord.Text))
            {
                if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.Definition))
                {
                    RemoveDefinitionViewItem(definitionViewItem);
                    return;
                }

                ProcessEditWordDefinitionDialog(dialog);

                var definition = new CambridgeWordInfo.Block() { Definition = dialog.Definition };
                WordInfo.Entries[dialog.EntryIndex].Definitions.Add(definition);
                WordInfo.Entries[dialog.EntryIndex].Label = dialog.Label;

                definitionViewItem.EntryIndex = dialog.EntryIndex;
                definitionViewItem.DefinitionIndex = WordInfo.Entries[dialog.EntryIndex].Definitions.Count - 1;
                definitionViewItem.Tag = lvDefinitions.Items.Count - 1;
                definitionViewItem.WordInfo = WordInfo;
                ChangeMade = true;
            }
        }

        private void tsmiEditDefinition_Click(object sender, EventArgs e)
        {
            if (lvDefinitions.SelectedItems.Count == 0)
                return;

            var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;
            using (var dialog = new EditWordDefinitionForm(WordInfo.Entries.Count, definitionViewItem.SubItems[0].Text, definitionViewItem.SubItems[1].Text, definitionViewItem.SubItems[3].Text))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                if (string.IsNullOrWhiteSpace(dialog.Definition))
                {
                    RemoveDefinitionViewItem(definitionViewItem);
                    return;
                }

                WordInfo.Entries[definitionViewItem.EntryIndex].ActualWord = dialog.Word;
                WordInfo.Entries[definitionViewItem.EntryIndex].Label = dialog.Label;
                WordInfo.Entries[definitionViewItem.EntryIndex].Definitions[definitionViewItem.DefinitionIndex].Definition = dialog.Definition;

                definitionViewItem.Text = dialog.Word;
                definitionViewItem.SubItems[1].Text = dialog.Label;
                definitionViewItem.SubItems[3].Text = dialog.Definition;
                definitionViewItem.WordInfo.Entries[definitionViewItem.EntryIndex].Definitions[definitionViewItem.DefinitionIndex].Definition = dialog.Definition;
                ChangeMade = true;
            }
        }

        private void tsmiDeleteDefinition_Click(object sender, EventArgs e)
        {
            var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;
            RemoveDefinitionViewItem(definitionViewItem);
        }

        private void lvExamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvExamples.SelectedItems.Count == 0)
                return;

            var exampleViewItem = lvExamples.SelectedItems[0] as ExampleViewItem;
            rtbOutput.Text = exampleViewItem.SubItems[1].Text;
        }

        private void tsmiAddExample_Click(object sender, EventArgs e)
        {
            if (lvDefinitions.SelectedItems.Count == 0)
                return;

            var exampleViewItem = new ExampleViewItem();
            lvExamples.Items.Add(exampleViewItem);

            using (var dialog = new EditExampleForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.Example))
                {
                    lvExamples.Items.Remove(exampleViewItem);
                    return;
                }

                var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;

                var examples = dialog.Example.Split(Environment.NewLine.ToCharArray())
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                for (int index = 0; index < examples.Length; index++)
                {
                    if (index > 0)
                    {
                        exampleViewItem = new ExampleViewItem();
                        lvExamples.Items.Add(exampleViewItem);
                    }

                    exampleViewItem.EntryIndex = definitionViewItem.EntryIndex;
                    exampleViewItem.DefinitionIndex = definitionViewItem.DefinitionIndex;

                    if (WordInfo.Entries[exampleViewItem.EntryIndex].Definitions[exampleViewItem.DefinitionIndex].Examples == null)
                        WordInfo.Entries[exampleViewItem.EntryIndex].Definitions[exampleViewItem.DefinitionIndex].Examples = new List<string>();
                    exampleViewItem.ExampleIndex = WordInfo.Entries[exampleViewItem.EntryIndex].Definitions[exampleViewItem.DefinitionIndex].Examples.Count;
                    exampleViewItem.Text = (exampleViewItem.ExampleIndex + 1).ToString();

                    var example = examples[index];
                    exampleViewItem.SubItems.Add(example);
                    WordInfo.Entries[exampleViewItem.EntryIndex].Definitions[exampleViewItem.DefinitionIndex].Examples.Add(example);
                }
                definitionViewItem.WordInfo = WordInfo;
                ChangeMade = true;
            }
        }

        private void tsmiEditExample_Click(object sender, EventArgs e)
        {
            if (lvExamples.SelectedItems.Count == 0)
                return;

            var exampleViewItem = lvExamples.SelectedItems[0] as ExampleViewItem;

            using (var dialog = new EditExampleForm(exampleViewItem.SubItems[1].Text))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                if (string.IsNullOrWhiteSpace(dialog.Example))
                    return;

                exampleViewItem.SubItems[1].Text = dialog.Example;
                WordInfo.Entries[exampleViewItem.EntryIndex].Definitions[exampleViewItem.DefinitionIndex].Examples[exampleViewItem.ExampleIndex] = dialog.Example;
                rtbOutput.Text = dialog.Example;
                ChangeMade = true;
            }
        }

        private void tsmiDeleteExample_Click(object sender, EventArgs e)
        {
            var exampleViewItem = lvExamples.SelectedItems[0] as ExampleViewItem;
            var exampleIndex = exampleViewItem.ExampleIndex;
            var definitionIndex = lvDefinitions.SelectedIndices[0];

            var definitionEntry = WordInfo.Entries      [exampleViewItem.EntryIndex]
                                          .Definitions  [exampleViewItem.DefinitionIndex];

            definitionEntry.Examples.RemoveAt(exampleViewItem.ExampleIndex);
            lvExamples.Items.Remove(exampleViewItem);

            for (int index = exampleIndex; index < definitionEntry.Examples.Count; index++)
            {
                exampleViewItem = (lvExamples.Items[index] as ExampleViewItem);
                exampleViewItem.ExampleIndex--;
                lvExamples.Items[index].Text = (index + 1).ToString();
            }

            lvDefinitions.Items[definitionIndex].SubItems[2].Text = definitionEntry.Examples.Count.ToString();

            rtbOutput.Text = string.Empty;
            ChangeMade = true;
        }

        private void bCancelOrDelete_Click(object sender, EventArgs e)
        {
            _isCanceling = true;
            Close();
        }
    }
}