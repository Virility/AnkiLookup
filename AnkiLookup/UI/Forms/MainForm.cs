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
using AnkiLookup.UI.Models;
using AnkiLookup.UI.Controls;
using System.Linq;
using AnkiLookup.Core.Extensions;

namespace AnkiLookup.UI.Forms
{
    public partial class MainForm : Form
    {
        public CambridgeWordInfo CopiedWordInfo { get; set; }

        private readonly AnkiProvider _ankiProvider;
        private readonly CambridgeProvider _cambridgeProvider;
        private readonly IWordInfoFormatter _htmlFormatter;
        private readonly IWordInfoFormatter _textFormatter;
        private readonly Comparer<string> _comparer;
        private readonly string _wordsDataPath;
        private readonly int _maxConcurrentLookups = 10;

        private bool _changeMade;

        public MainForm()
        {
            _ankiProvider = new AnkiProvider();
            _cambridgeProvider = new CambridgeProvider(CambridgeDataSet.British);

            _htmlFormatter = new HtmlFormatter();
            _textFormatter = new TextFormatter();
            _comparer = new OrdinalIgnoreCaseComparer();

            InitializeComponent();
            LoadDataFile(_wordsDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Words.dat"));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_changeMade)
                return;

            var wordInfos = lvWords.Items.Cast<WordViewItem>()
                .Select(wordViewItem => wordViewItem.WordInfo)
                .Where(wordInfo => wordInfo != null)
                .OrderBy(a => a.InputWord, _comparer).ToArray();

            File.WriteAllBytes(_wordsDataPath, CambridgeWordInfo.Serialize(wordInfos));
        }

        private async void lvWords_DoubleClick(object sender, EventArgs e)
        {
            var word = lvWords.SelectedItems[0].Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(word))
                return;

            var wordViewItem = (lvWords.SelectedItems[0] as WordViewItem);
            if (wordViewItem.WordInfo.Entries.Count == 0)
            {
                wordViewItem.WordInfo = await _cambridgeProvider.GetWordInfo(word);
                if (wordViewItem.WordInfo == null)
                    return;
            }
            rtbWordOutput.Text = _textFormatter.Render(wordViewItem.WordInfo);
        }

        private void lvWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvWords.SelectedItems.Count == 0)
                return;
            var word = lvWords.SelectedItems[0].Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(word))
                return;
            var wordViewItem = (lvWords.SelectedItems[0] as WordViewItem);
            if (wordViewItem.WordInfo == null)
                return;

            if (wordViewItem.WordInfo.Entries.Count > 0)
                rtbWordOutput.Text = _textFormatter.Render(wordViewItem.WordInfo);
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
                    , "AnkiLookup", MessageBoxButtons.YesNoCancel);
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

        private void LoadDataFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            var readData = File.ReadAllBytes(fileName);
            var deserialized = CambridgeWordInfo.Deserialize(readData);
            if (deserialized.Length == 0)
                return;

            var listViewItems = new List<WordViewItem>();
            foreach (var wordInfo in deserialized)
                listViewItems.Add(new WordViewItem(wordInfo));
            lvWords.Items.AddRange(listViewItems.ToArray());
            RefreshWordColumn();
        }

        private void tsmiLoadDataFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                lvWords.Items.Clear();
                LoadDataFile(dialog.FileName);
                _changeMade = false;
            }
        }

        private async Task LookUpWord(WordViewItem wordViewItem = null, string word = null)
        {
            word = wordViewItem.Text.Trim().ToLower();
            CambridgeWordInfo wordInfo = null;
            try
            {
                wordInfo = await _cambridgeProvider.GetWordInfo(word);
                if (wordInfo == null)
                {
                    Invoke(new Action(() => wordViewItem.SubItems[2].Text = "Can't find word."));
                    Debug.WriteLine($"Error:: Word ({word}): Can't find word.");
                    return;
                }
                Invoke(new Action(() => wordViewItem.WordInfo = wordInfo));
                _changeMade = true;
                await Task.Delay(1000);
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Internal Error:: Word ({word}): {exception.Message}");
            }
        }

        private async void tsmiGetDefinitionsFromCambridge_Click(object sender, EventArgs e)
        {
            if (lvWords.Items.Count == 0)
                return;

            var trackViewItems = lvWords.Items.Cast<WordViewItem>().ToArray()
                .Where(wordViewItem => wordViewItem.WordInfo.Entries.Count == 0);
            await Task.Run(() => trackViewItems.ForEachAsync(_maxConcurrentLookups,
                async wordViewItem => await LookUpWord(wordViewItem)));
        }

        private void tsmiExportWordsForAnki_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                if (lvWords.Items.Count == 0)
                    return;

                var entries = new List<string>();
                foreach (WordViewItem wordViewItem in lvWords.Items)
                {
                    if (wordViewItem.WordInfo.Entries.Count != 0)
                        entries.Add(wordViewItem.WordInfo.FormatForAnkiImport(_htmlFormatter));
                }
                var sortedEntries = entries.ToArray();
                Array.Sort(sortedEntries, new OrdinalIgnoreCaseComparer());

                var entriesBuilder = new StringBuilder();
                foreach (var entry in sortedEntries)
                    entriesBuilder.Append(entry);

                if (entriesBuilder.Length > 2)
                    entriesBuilder.Length -= 2;

                rtbWordOutput.Text = entriesBuilder.ToString();
                File.WriteAllText(Path.Combine(dialog.SelectedPath, "Words-AnkiImportReady.txt"), entriesBuilder.ToString());
            }
        }

        private void tsmiExportWordList_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
                if (lvWords.Items.Count == 0)
                    return;

                var entries = new List<string>();
                foreach (WordViewItem wordViewItem in lvWords.Items)
                    entries.Add(wordViewItem.WordInfo.InputWord);
                var sortedEntries = entries.ToArray();
                Array.Sort(sortedEntries, new OrdinalIgnoreCaseComparer());

                var entriesBuilder = new StringBuilder();
                foreach (var entry in sortedEntries)
                    entriesBuilder.AppendLine(entry);

                if (entriesBuilder.Length > 2)
                    entriesBuilder.Length -= 2;

                rtbWordOutput.Text = entriesBuilder.ToString();
                File.WriteAllText(Path.Combine(dialog.SelectedPath, "Words-WordList.txt"), entriesBuilder.ToString());
            }
        }

        private void tsmiDeleteSelected_Click(object sender, EventArgs e)
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
            var wordInfos = lvWords.GetAsWordInfoList();

            var wordViewItem = new WordViewItem(string.Empty);
            lvWords.Items.Add(wordViewItem);

            using (var dialog = new EditWordInfoForm(this, Job.Add, _cambridgeProvider, wordViewItem.WordInfo, ref wordInfos))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    RemoveItemByWordInfo(wordViewItem.WordInfo, false);
                    return;
                }

                wordViewItem.WordInfo = dialog.WordInfo;
                RefreshWordColumn();
                _changeMade = true;
            }
        }

        private void tsmiEditWord_Click(object sender, EventArgs e)
        {
            if (lvWords.SelectedItems.Count == 0)
                return;

            var wordViewItem = lvWords.SelectedItems[0] as WordViewItem;
            var wordInfo = wordViewItem.WordInfo;

            var wordInfos = lvWords.GetAsWordInfoList();

            using (var dialog = new EditWordInfoForm(this, Job.Edit, _cambridgeProvider, wordInfo, ref wordInfos))
            {
                var dialogResult = dialog.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    RemoveItemByWordInfo(wordViewItem.WordInfo, true);
                    return;
                }
                else if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                dialog.WordInfo.ImportedIntoAnki = default(DateTime);
                wordViewItem.WordInfo = dialog.WordInfo;
                rtbWordOutput.Text = _textFormatter.Render(dialog.WordInfo);
                _changeMade = true;
            }
        }

        private void tsmiClearImportStates_Click(object sender, EventArgs e)
        {
            foreach (WordViewItem wordViewItem in lvWords.Items)
            {
                wordViewItem.WordInfo.AddedBefore = false;
                wordViewItem.WordInfo.ImportedIntoAnki = default(DateTime);
                wordViewItem.Refresh();

                _changeMade = true;
            }
        }

        private void lvWords_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvWords.SelectedItems.Count > 0)
                tsmiEditWord_Click(null, null);
        }

        public void RemoveItemByWordInfo(CambridgeWordInfo wordInfo, bool monitorChangesMade)
        {
            bool localChangesMade = false;
            foreach (WordViewItem item in lvWords.Items)
            {
                if (item.WordInfo != wordInfo)
                    continue;

                lvWords.Items.Remove(item);
                localChangesMade = true;
                if (monitorChangesMade)
                    _changeMade = true;
            }
            if (!localChangesMade)
                return;

            RefreshWordColumn();
            rtbWordOutput.Text = string.Empty;
        }
    }
}