using AnkiLookup.Core.Models;
using AnkiLookup.Core.Providers;
using AnkiLookup.UI.Controls;
using AnkiLookup.UI.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace AnkiLookup.UI.Dialogs
{
    public partial class EditWordForm : Form
    {
        private readonly WordManagementForm _wordManagementForm;
        private readonly string _titleFormat;
        private bool _isCanceling;
        private Job _job;
        private Job _definitionJob = Job.None;

        public Word Word { get; internal set; }

        public bool ChangeMade { get; internal set; }
     
        public EditWordForm(WordManagementForm wordManagementForm, Job job, Word word)
        {
            _wordManagementForm = wordManagementForm;
            _job = job;
            Word = word;

            InitializeComponent();
            Initialize();
            _titleFormat = Text;
        }

        private void Initialize()
        {
            Text = "{0} Word - \"{1}\"";
            bCancelOrDelete.Text = (_job == Job.Add) ? "Cancel" : "Delete";
        }

        private void RefreshWordUI()
        {
            var definitionViewItems = new List<DefinitionViewItem>();
            for (int entryIndex = 0; entryIndex < Word.Entries.Count; entryIndex++)
            {
                var definitions = Word.Entries[entryIndex].Definitions;
                for (int definitionIndex = 0; definitionIndex < definitions.Count; definitionIndex++)
                    definitionViewItems.Add(new DefinitionViewItem(Word, entryIndex, definitionIndex) { Tag = definitionViewItems.Count });
            }
            lvDefinitions.Items.AddRange(definitionViewItems.ToArray());
        }

        private string _inputWord;
        private void EditWordForm_Load(object sender, EventArgs e)
        {
            _inputWord = Word.InputWord;

            tbInputWord.Text = Word.InputWord;
            tbInputWord_TextChanged(null, null);
            if (Word == null)
                return;

            lvDefinitions.Items.Clear();
            lvExamples.Items.Clear();

            RefreshWordUI();
        }

        private void EditWordForm_FormClosing(object sender, FormClosingEventArgs e)
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

            if (_job == Job.Add && _wordManagementForm.ExistsInCorpus(Word.InputWord, true))
            {
                var dialogResult = MessageBox.Show(Properties.Resources.AlreadyExistsInCorpusMessage, Config.ApplicationName, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    DialogResult = DialogResult.Abort;
                    return;
                }
            }

            foreach (var entry in Word.Entries)
            {
                if (entry.Definitions.Count == 0)
                    Word.Entries.Remove(entry);
            }

            DialogResult = DialogResult.OK;
        }

        private void tbInputWord_TextChanged(object sender, EventArgs e)
        {
            var word = tbInputWord.Text.Trim().ToLower();
            if (word.Length == 0)
                return;
            Word.InputWord = word;
            Text = string.Format(_titleFormat, _job, word);
            pFoundState.Visible = _wordManagementForm.ExistsInCorpus(word);
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

            var wordText = tbInputWord.Text.Trim().ToLower();
            try
            {
                Word word;
                foreach (var resolver in Config.Resolvers)
                {
                    word = await resolver.Value.GetWord(wordText);
                    if (word == null)
                    {
                        MessageBox.Show($"\"{wordText}\" doesn't exist in {resolver.Key}.");
                        continue;
                    }

                    pFoundState.BackColor = Color.Green;
                    pFoundState.Visible = true;

                    Word = word;
                    lvDefinitions.Items.Clear();
                    lvExamples.Items.Clear();

                    RefreshWordUI();
                    ChangeMade = true;
                }

                throw new Exception("Doesn't exist in any resolver database.");
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Internal Error:: Word ({wordText}): {exception.Message}");
            }
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            _wordManagementForm.CopiedWord = Word;
        }

        private void bPaste_Click(object sender, EventArgs e)
        {
            if (_wordManagementForm.CopiedWord == null)
                return;

            var copiedWord = _wordManagementForm.CopiedWord;
            Word.Entries = copiedWord.Entries;
            ChangeMade = true;

            RefreshWordUI();
        }

        private void lvEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_definitionJob != Job.None)
                return;
            if (lvDefinitions.SelectedItems.Count == 0)
            {
                lvExamples.Items.Clear();
                rtbOutput.Clear();
                return;
            }

            if (lvExamples.Items.Count > 0)
                lvExamples.Items.Clear();

            var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;
            if (definitionViewItem.EntryIndex == -1 || definitionViewItem.DefinitionIndex == -1)
                return;

            var examples = Word.Entries[definitionViewItem.EntryIndex].Definitions[definitionViewItem.DefinitionIndex].Examples;
            if (examples.Count == 0)
                return;

            var exampleViewItems = new List<ExampleViewItem>();
            for (int exampleIndex = 0; exampleIndex < examples.Count; exampleIndex++)
                exampleViewItems.Add(new ExampleViewItem(definitionViewItem, exampleIndex));
            lvExamples.Items.AddRange(exampleViewItems.ToArray());
        }

        private void RemoveDefinitionViewItem(ref DefinitionViewItem definitionViewItem)
        {
            var definitionIndex = EditWordDefinitionForm.RemoveDefinitionViewItem(definitionViewItem);
            if (lvExamples.Items.Count > 0 && (lvExamples.Items[0] as ExampleViewItem).DefinitionIndex == definitionIndex)
                lvExamples.Items.Clear();

            rtbOutput.Text = string.Empty;
            ChangeMade = true;
        }

        private void AddOrEditDefinition(Job job)
        {
            DefinitionViewItem definitionViewItem = null;

            var word = Word.Clone();
            var entryIndex = -1;
            var definitionIndex = -1;

            _definitionJob = job;

            if (job == Job.Add)
            {
                definitionViewItem = new DefinitionViewItem(Word);
                lvDefinitions.Items.Add(definitionViewItem);
                definitionViewItem.EnsureVisible();
            }
            else if (job == Job.Edit)
            {
                if (lvDefinitions.SelectedItems.Count == 0)
                {
                    _definitionJob = Job.None;
                    return;
                }
                definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;
                word = definitionViewItem.Word.Clone();
                entryIndex = definitionViewItem.EntryIndex;
                definitionIndex = definitionViewItem.EntryIndex;
            }

            using (var dialog = new EditWordDefinitionForm(definitionViewItem, job))
            {
                var dialogResult = dialog.ShowDialog();

                var condition = dialogResult != DialogResult.OK;
                var condition2 = dialogResult == DialogResult.OK && string.IsNullOrWhiteSpace(dialog.Definition);

                if (job == Job.Add && condition || condition2)
                {
                    lvDefinitions.Items.Remove(definitionViewItem);
                    _definitionJob = Job.None;
                    return;
                }
                else if (job == Job.Edit && condition)
                {
                    Word = definitionViewItem.Word = word;
                    definitionViewItem.EntryIndex = entryIndex;
                    definitionViewItem.DefinitionIndex = definitionIndex;
                    definitionViewItem.Refresh();
                    _definitionJob = Job.None;
                    return;
                }
                else if (job == Job.Edit && condition2)
                     RemoveDefinitionViewItem(ref definitionViewItem);
                else
                {
                    var entry = Word.Entries[definitionViewItem.EntryIndex];
                    var block = entry.Definitions[definitionViewItem.DefinitionIndex];
                    block.Definition = dialog.Definition;
                    definitionViewItem.Refresh();
                }
                ChangeMade = true;
                _definitionJob = Job.None;
            }
        }

        private void tsmiAddDefinition_Click(object sender, EventArgs e)
        {
            AddOrEditDefinition(Job.Add);
        }

        private void tsmiEditDefinition_Click(object sender, EventArgs e)
        {
            AddOrEditDefinition(Job.Edit);
        }

        private void lvDefinitions_DoubleClick(object sender, EventArgs e)
        {
            AddOrEditDefinition(Job.Edit);
        }

        private void tsmiDeleteDefinition_Click(object sender, EventArgs e)
        {
            if (lvDefinitions.SelectedItems.Count == 0)
                return;
            var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;
            RemoveDefinitionViewItem(ref definitionViewItem);
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
            
            var definitionViewItem = lvDefinitions.SelectedItems[0] as DefinitionViewItem;

            var exampleViewItem = new ExampleViewItem(definitionViewItem, string.Empty);
            lvExamples.Items.Add(exampleViewItem);

            using (var dialog = new EditMultilineForm("Example"))
            {
                if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.Content))
                {
                    lvExamples.Items.Remove(exampleViewItem);
                    return;
                }

                var examples = dialog.Content.Split(Environment.NewLine.ToCharArray())
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                for (int index = 0; index < examples.Length; index++)
                {
                    if (index == 0)
                        exampleViewItem.Example = examples[0];
                    else
                    {
                        exampleViewItem = new ExampleViewItem(definitionViewItem, examples[index]);
                        lvExamples.Items.Add(exampleViewItem);
                    }
                }

                ChangeMade = true;
            }
        }

        private void tsmiEditExample_Click(object sender, EventArgs e)
        {
            if (lvExamples.SelectedItems.Count == 0)
                return;

            var exampleViewItem = lvExamples.SelectedItems[0] as ExampleViewItem;

            using (var dialog = new EditMultilineForm("Example", exampleViewItem.SubItems[1].Text))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                if (string.IsNullOrWhiteSpace(dialog.Content))
                    return;

                rtbOutput.Text = dialog.Content;
                ChangeMade = true;
            }
        }

        private void tsmiDeleteExample_Click(object sender, EventArgs e)
        {
            if (lvExamples.SelectedItems.Count == 0)
                return;

            var exampleViewItem = lvExamples.SelectedItems[0] as ExampleViewItem;
            var exampleIndex = exampleViewItem.ExampleIndex;

            var definitionEntry = Word.Entries      [exampleViewItem.EntryIndex]
                                          .Definitions  [exampleViewItem.DefinitionIndex];

            definitionEntry.Examples.RemoveAt(exampleViewItem.ExampleIndex);
            lvExamples.Items.Remove(exampleViewItem);

            for (int index = exampleIndex; index < definitionEntry.Examples.Count; index++)
            {
                exampleViewItem = (lvExamples.Items[index] as ExampleViewItem);
                exampleViewItem.ExampleIndex--;
                lvExamples.Items[index].Text = (index + 1).ToString();
            }
            lvDefinitions.SelectedItems[0].SubItems[2].Text = definitionEntry.Examples.Count.ToString();

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