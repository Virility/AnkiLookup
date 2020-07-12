 using System;
using System.Collections.Generic;
using System.Text;
using AnkiLookup.Core.Providers;
using System.Windows.Forms;
using AnkiLookup.Core.Helpers;
using System.IO;
using System.Threading.Tasks;
using AnkiLookup.Core.Models;
using System.Diagnostics;
using AnkiLookup.UI.Dialogs;
using System.Linq;
using AnkiLookup.Core.Extensions;
using AnkiLookup.UI.Controls;

namespace AnkiLookup.UI.Forms
{
    public partial class WordManagementForm : Form
    {
        public Word CopiedWord { get; set; }

        private readonly Deck _backupDeck;
        public Deck Deck { get; set; }

        private readonly int _maxConcurrentLookups = 10;
        private bool _changeMade;

        public WordManagementForm(Deck deck)
        {
            Deck = deck;
            _backupDeck = deck;

            InitializeComponent();
            lvWords.ListViewItemSorter = new ListViewItemComparer(lvWords);

            InitializeAnkiStates();
            LoadDataFile(deck.FilePath);
        }

        private void WordManagementForm_Load(object sender, EventArgs e)
        {
            //lvWords.Focus();
            //lvWords.Select();
            //lvWords.Items[0].Focused = true;
            //lvWords.Items[0].Selected = true;

            //tsmiEditWord_Click(null, null);
        }

        private void InitializeAnkiStates()
        {
            Text = Config.ApplicationName + " - Word Management: " + Deck.Name;
            tbDeckName.Text = Deck.Name;

            if (Deck.ExportOption == "Text")
                rbText.Checked = true;
            else
                rbHtml.Checked = true;
        }

        private void LoadDataFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            var words = DeckManagementForm.GetWordsFromDeck(Deck);
            if (words.Length == 0)
                return;

            var listViewItems = new List<WordViewItem>();
            foreach (var word in words)
                listViewItems.Add(new WordViewItem(word));
            lvWords.Items.AddRange(listViewItems.ToArray());
            RefreshWordColumn();
        }

        private void RemoveEmptyEntries(Word[] words)
        {
            foreach (var word in words)
            {
                for (int index = 0; index < word.Entries.Count; index++)
                {
                    if (word.Entries[index].Definitions.Count == 0)
                        word.Entries.RemoveAt(index);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbDeckName.Text))
            {
                MessageBox.Show("Deck name cannot be empty.");
                return;
            }

            Deck.Name = tbDeckName.Text;
            if (_backupDeck.Name != Deck.Name)
            {
                File.Delete(_backupDeck.FilePath);
                _changeMade = true;
            }

            Deck.ExportOption = rbText.Checked ? "Text" : "HTML";
            if (_backupDeck.ExportOption != Deck.ExportOption)
                _changeMade = true;

            if (!_changeMade)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            var words = lvWords.Items.Cast<WordViewItem>()
                .Select(wordViewItem => wordViewItem.Word)
                .OrderBy(word => word.InputWord, Config.Comparer).ToArray();
            RemoveEmptyEntries(words);

            Deck.DateModified = DateTime.Now;
            DeckManagementForm.SaveDeck(Deck, words);
            Config.LastOpenedDeckName = Deck.Name;

            DialogResult = DialogResult.OK;
        }

        private async void lvWords_DoubleClick(object sender, EventArgs e)
        {
            if (lvWords.SelectedItems.Count == 0)
                return;

            var word = lvWords.SelectedItems[0].Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(word))
                return;

            var wordViewItem = lvWords.SelectedItems[0] as WordViewItem;
            if ((wordViewItem.Word == null || wordViewItem.Word.Entries.Count == 0) && await LookUpWord(wordViewItem) == null)
                return;

            rtbWordOutput.Text = Config.TextFormatter.Render(wordViewItem.Word);
        }

        private void lvWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvWords.SelectedItems.Count == 0)
                return;
            var word = lvWords.SelectedItems[0].Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(word))
                return;
            var wordViewItem = (lvWords.SelectedItems[0] as WordViewItem);
            if (wordViewItem.Word == null)
                return;

            if (wordViewItem.Word.Entries.Count > 0)
                rtbWordOutput.Text = Config.TextFormatter.Render(wordViewItem.Word);
        }

        private void RefreshWordColumn()
        {
            if (lvWords.Items.Count == 0)
                lvWords.Columns[0].Text = "Word";
            else if (lvWords.Items.Count == 1)
                lvWords.Columns[0].Text = "Word (1)";
            else
                lvWords.Columns[0].Text = $"Words ({lvWords.Items.Count})";
        }

        private void tsmiLoadWordList_Click(object sender, EventArgs e)
        {
            if (lvWords.Items.Count > 0)
            {
                var dialogResult = MessageBox.Show(
                    "Do you want to start new?\n" +
                    "-> Yes removes all previous words without saving.\n" +
                    "-> No adds new words to existing list."
                    , Config.ApplicationName, MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                    lvWords.Items.Clear();
                else if (dialogResult == DialogResult.Cancel)
                    return;
            }
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var lines = File.ReadAllLines(dialog.FileName).Distinct().ToArray();
                if (lines.Length == 0)
                    return;

                var listViewItems = new List<WordViewItem>();
                foreach (var line in lines)
                {
                    var word = line.Trim().ToLower();
                    if (!lvWords.Items.ContainsKey(word))
                        listViewItems.Add(new WordViewItem(line));
                }
                lvWords.Items.AddRange(listViewItems.ToArray());
                RefreshWordColumn();
                _changeMade = false;
            }
        }

        private void tsmiLoadDataFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                lvWords.Items.Clear();
                LoadDataFile(dialog.FileName);
                _changeMade = true;
            }
        }

        private async Task<Word> LookUpWord(WordViewItem wordViewItem = null)
        {
            var wordText = wordViewItem.Text.Trim().ToLower();
            Word word = null;
            try
            {
                word = await CambridgeProvider.GetWord(wordText);
                if (word == null)
                {
                    Invoke(new Action(() => wordViewItem.SubItems[2].Text = "Cannot find word."));
                    Debug.WriteLine($"Error:: Word ({word}): Cannot find word.");
                } else {
                    Invoke(new Action(() => wordViewItem.Word = word));
                    _changeMade = true;
                    await Task.Delay(1000);
                    return word;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Internal Error:: Word ({word}): {exception.Message}");
            }
            return null;
        }

        private async void tsmiGetDefinitionsFromCambridge_Click(object sender, EventArgs e)
        {
            if (lvWords.Items.Count == 0)
                return;

            var trackViewItems = lvWords.Items.Cast<WordViewItem>().ToArray()
                .Where(wordViewItem => wordViewItem.Word.Entries.Count == 0);
            await Task.Run(() => trackViewItems.ForEachAsync(_maxConcurrentLookups,
                async wordViewItem => await LookUpWord(wordViewItem)));
        }

        private void tsmiExport_Click(object sender, EventArgs e)
        {
            if (lvWords.Items.Count == 0)
                return;

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var senderName = (sender as ToolStripMenuItem).Name;
                var entries = new List<string>();
                foreach (WordViewItem wordViewItem in lvWords.Items)
                {
                    if (senderName == "tsmiExportWordList")
                        entries.Add(wordViewItem.Word.InputWord);
                    else if (senderName == "tsmiExportWordsForAnki")
                    {
                        if (wordViewItem.Word.Entries.Count != 0)
                            entries.Add(wordViewItem.Word.AsFormatted(Config.HtmlFormatter));
                    }
                }
                var sortedEntries = entries.ToArray();
                Array.Sort(sortedEntries, Config.Comparer);

                var entriesBuilder = new StringBuilder();
                foreach (var entry in sortedEntries)
                    entriesBuilder.AppendLine(entry);
                if (entriesBuilder.Length > 2)
                    entriesBuilder.Length -= 2;
                rtbWordOutput.Text = entriesBuilder.ToString();

                var fileName = "Words";
                if (senderName == "tsmiExportWordList")
                    fileName += "-WordList";
                else if (senderName == "tsmiExportWordsForAnki")
                    fileName += "-AnkiImportReady";
                fileName = Path.Combine(dialog.SelectedPath, fileName + ".txt");
                File.WriteAllText(fileName, entriesBuilder.ToString());
            }
        }

        private void tsmiDeleteSelectedWord_Click(object sender, EventArgs e)
        {
            bool localChangesMade = false;
            foreach (ListViewItem selectedItem in lvWords.SelectedItems)
            {
                lvWords.Items.Remove(selectedItem);
                localChangesMade = true;
                _changeMade = true;
            }
            if (!localChangesMade)
                return;

            RefreshWordColumn();
            rtbWordOutput.Text = string.Empty;
        }

        private void tsmiAddWord_Click(object sender, EventArgs e)
        {
            var wordViewItem = new WordViewItem(string.Empty);
            lvWords.Items.Add(wordViewItem);
            var words = lvWords.GetAsWordList();

            using (var dialog = new EditWordForm(this, Job.Add, CambridgeProvider, wordViewItem.Word, ref words))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    RemoveItemByWord(wordViewItem.Word, false);
                    return;
                }
                else
                    wordViewItem.Word = dialog.Word;
            }

            lvWords.SelectedItems.Clear();
            lvWords.Sort();
            wordViewItem.Selected = true;
            wordViewItem.EnsureVisible();

            RefreshWordColumn();
            _changeMade = true;
        }

        private void tsmiAddWords_Click(object sender, EventArgs e)
        {
            using (var dialog = new EditMultilineForm("Words"))
            {
                if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.Content))
                    return;

                var words = dialog.Content.Split(Environment.NewLine.ToCharArray())
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                for (int index = 0; index < words.Length; index++)
                {
                    var wordViewItem = new WordViewItem(words[index]);
                    wordViewItem.Refresh();
                    lvWords.Items.Add(wordViewItem);
                }
                RefreshWordColumn();
                _changeMade = true;
            }
        }

        private void tsmiEditSelectedWord_Click(object sender, EventArgs e)
        {
            if (lvWords.SelectedItems.Count == 0)
                return;

            var wordViewItem = lvWords.SelectedItems[0] as WordViewItem;
            var words = lvWords.GetAsWordList();

            using (var dialog = new EditWordForm(this, Job.Edit, CambridgeProvider, wordViewItem.Word, ref words))
            {
                var dialogResult = dialog.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    RemoveItemByWord(wordViewItem.Word, true);
                    return;
                }
                else if (dialogResult != DialogResult.OK)
                    return;
                wordViewItem.Word = dialog.Word;
            }
            rtbWordOutput.Text = Config.TextFormatter.Render(wordViewItem.Word);
            _changeMade = true;
        }

        private void lvWords_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvWords.SelectedItems.Count > 0)
                tsmiEditSelectedWord_Click(null, null);
        }

        public void RemoveItemByWord(Word word, bool monitorChangesMade)
        {
            bool localChangesMade = false;
            foreach (WordViewItem wordViewItem in lvWords.Items)
            {
                if (wordViewItem.Word != word)
                    continue;

                lvWords.Items.Remove(wordViewItem);
                localChangesMade = true;
                if (monitorChangesMade)
                    _changeMade = true;
            }
            if (!localChangesMade)
                return;

            RefreshWordColumn();
            rtbWordOutput.Text = string.Empty;
        }

        private void LvWords_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewItem selectedListViewItem = null;
            if (lvWords.SelectedItems.Count != 0)
                selectedListViewItem = lvWords.SelectedItems[0];

            var comparer = lvWords.ListViewItemSorter as ListViewItemComparer;
            if (e.Column != comparer.Column)
                comparer.Column = e.Column;
            lvWords.Sorting = lvWords.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            if (selectedListViewItem != null)
                selectedListViewItem.EnsureVisible();
        }

        private async void tbDeckName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && tbDeckName.Text != Deck.Name)
                {
                    var importedWordsExists = lvWords.Items.Cast<WordViewItem>().Any(item => item.Word.ImportDate != default);
                    if (importedWordsExists)
                    {
                        var dialogResult = MessageBox.Show("The entered deck name is different than the deck name in Anki. Would you like to move all imported cards under this new deck name?", Config.ApplicationName, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            var oldDeckName = Deck.Name;
                            Deck.Name = tbDeckName.Text;

                            if (!await Config.AnkiProvider.MoveDeck(oldDeckName, Deck.Name))
                                throw new Exception($"Could not move cards from deck \"{oldDeckName}\" to \"{Deck.Name}\".");
                            if (!await Config.AnkiProvider.DeleteDeck(oldDeckName))
                                throw new Exception($"Could not delete deck \"{oldDeckName}\".");

                            MessageBox.Show($"Successfully renamed deck \"{oldDeckName}\" to \"{Deck.Name}\".");
                            return;
                        }
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show("No words are imported. Moving zero words is not possible. Would you like to import words under this new deck name?", Config.ApplicationName, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.OK)
                        {
                            Deck.Name = tbDeckName.Text;
                            tsmiImportToAnki_Click(this, null);
                            return;
                        }
                    }

                    // If import wasn't successful, just reset the deck name.
                    tbDeckName.Text = Deck.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}