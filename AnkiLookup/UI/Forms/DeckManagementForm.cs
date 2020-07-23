using AnkiLookup.Core.Models;
using AnkiLookup.UI.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnkiLookup.UI.Forms
{
    public partial class DeckManagementForm : Form
    {
        private static bool _changeMade;
        private static string _lastOpenedDeckName;

        public DeckManagementForm()
        {
            InitializeComponent();
            Text = Config.ApplicationName + " - Deck Management";
            LoadDecks();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_changeMade)
                SaveDecks();
        }

        private void PopulateListViewFromExistingDecks()
        {
            var deckViewItems = new List<DeckViewItem>();
            var directory = Path.Combine(Config.ApplicationPath, Deck.DefaultDecksPath);
            if (!Directory.Exists(directory))
                return;

            var deckFilePaths = Directory.GetFiles(directory, "*." + Config.DefaultDeckFileExtension);

            foreach (var dataFilePath in deckFilePaths)
            {
                var deck = new Deck(Path.GetFileNameWithoutExtension(dataFilePath));
                deckViewItems.Add(new DeckViewItem(deck));
                _changeMade = true;
            }
            lvDecks.Items.AddRange(deckViewItems.ToArray());
        }

        private void LoadDecks()
        {
            if (!File.Exists(Config.DeckRepositoryFilePath))
            {
                PopulateListViewFromExistingDecks();
                return;
            }

            var deckViewItems = new List<DeckViewItem>();
            var content = File.ReadAllText(Config.DeckRepositoryFilePath);
            foreach (var deck in JsonConvert.DeserializeObject<Deck[]>(content))
                deckViewItems.Add(new DeckViewItem(deck));
            lvDecks.Items.AddRange(deckViewItems.ToArray());

            if (string.IsNullOrWhiteSpace(Config.LastOpenedDeckName))
                return;

            DeckViewItem lastOpenedDeckViewItem = null;
            foreach (DeckViewItem deckViewItem in lvDecks.Items)
            {
                if (deckViewItem.Deck.Name == Config.LastOpenedDeckName)
                {
                    lastOpenedDeckViewItem = deckViewItem;
                    break;
                }
            }
            if (lastOpenedDeckViewItem != null)
                HandleDeckManagement(lastOpenedDeckViewItem);
        }

        private void SaveDecks()
        {
            var decks = lvDecks.Items.Cast<DeckViewItem>()
                .Select(deckViewItem => deckViewItem.Deck).ToArray();
            File.WriteAllText(Config.DeckRepositoryFilePath, JsonConvert.SerializeObject(decks, Config.JsonFormatting));

            Config.LastOpenedDeckName = _lastOpenedDeckName;
        }

        private void HandleDeckManagement(DeckViewItem deckViewItem)
        {
            using var deckManagementDialog = new WordManagementForm(deckViewItem.Deck);
            Hide();
            var dialogResult = deckManagementDialog.ShowDialog();
            Show();
            if (dialogResult != DialogResult.OK)
                return;

            _changeMade = true;
            deckViewItem.Deck = deckManagementDialog.Deck;
            _lastOpenedDeckName = deckViewItem.Deck.Name;
        }

        private void tsmiAddDeckNew_Click(object sender, EventArgs e)
        {
            var deckViewItem = new DeckViewItem();
            lvDecks.Items.Add(deckViewItem);

            HandleDeckManagement(deckViewItem);
            _changeMade = true;
        }

        private void tsmiAddDeckFromFile_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var deckViewItem = new DeckViewItem(Deck.FromFilePath(dialog.FileName));
            lvDecks.Items.Add(deckViewItem);
            HandleDeckManagement(deckViewItem);
            _changeMade = true;
        }

        private void tsmiManageSelected_Click(object sender, EventArgs e)
        {
            if (lvDecks.SelectedItems.Count == 0)
                return;

            var deckViewItem = lvDecks.SelectedItems[0] as DeckViewItem;
            HandleDeckManagement(deckViewItem);
        }

        private void tsmiDeleteSelected_Click(object sender, EventArgs e)
        {
            if (lvDecks.SelectedItems.Count == 0)
                return;

            lvDecks.Items.Remove(lvDecks.SelectedItems[0]);
            _changeMade = true;
        }

        private void lvDecks_DoubleClick(object sender, EventArgs e)
        {
            tsmiManageSelected_Click(this, null);
        }

        private async void tsmiImportToAnki_Click(object sender, EventArgs e)
        {
            if (lvDecks.SelectedItems.Count == 0)
                return;

            var startTime = DateTime.Now;

            var deckImportErrorCount = 0;
            var sb = new StringBuilder();
            foreach (DeckViewItem deckViewItem in lvDecks.SelectedItems)
            {
                deckViewItem.SubItems[2].Text = "Importing..";

                var deck = deckViewItem.Deck;
                var words = GetWordsFromDeck(deckViewItem.Deck);
                if (words == null || words.Length == 0)
                    continue;

                (Word[] SuccessfulWords, List<string> ErrorWordStrings) = await ImportDeck(deck, words);
                if (SuccessfulWords == null)
                {
                    sb.AppendLine($"Failed importing all words from deck \"{deck.Name}\".");
                    deckImportErrorCount++;

                    if (deckImportErrorCount == 2)
                    {
                        var dialogResult = MessageBox.Show("Multiple decks failed to import. Would you like to stop importing the other decks?",
                            Config.ApplicationName + $" - Importing \"{deck.Name}\"",
                            MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                            return;
                    }

                    deckViewItem.Refresh();
                    continue;
                }
                for (int index = 0; index < words.Length; index++)
                {
                    var successfulWord = SuccessfulWords.FirstOrDefault(word => words[index].InputWord == word.InputWord);
                    if (successfulWord != null)
                        words[index] = successfulWord;
                }

                sb.AppendLine($"Successfully imported {(ErrorWordStrings.Count == 0 ? "all" : "some")} words from deck \"{deck.Name}\".");
                if (ErrorWordStrings.Count != 0)
                {
                    var plural = ErrorWordStrings.Count != 1 ? "s" : string.Empty;
                    sb.AppendLine($"Error{plural}:\n{string.Join(Environment.NewLine, ErrorWordStrings)}");
                }
                SaveDeck(deck, words);
                deckViewItem.Refresh();
            }
            _changeMade = true;

            if (sb.Length > 1)
                sb.Length--;
            MessageBox.Show(sb.ToString());

            var date = startTime.ToString("G").Replace("/", "-").Replace(":", ".");
            var logFileName = "Import Log (" + date + ").txt";
            File.WriteAllText(logFileName, sb.ToString());
        }

        private void lvDecks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.A || !e.Control)
                return;
            foreach (ListViewItem item in lvDecks.Items)
                item.Selected = true;
        }

        public static Word[] GetWordsFromDeck(Deck deck)
        {
            if (!File.Exists(deck.FilePath))
                return null;

            var content = File.ReadAllText(deck.FilePath);
            return Word.Deserialize(content);
        }

        public static async Task<(Word[] SuccessfulWords, List<string> ErrorWordStrings)> ImportDeck(Deck deck, Word[] wordsToImport)
        {
            var formatter = deck.ExportOption == "Text" ? Config.SimpleTextFormatter : Config.HtmlFormatter;
            var modelName = deck.ExportOption == "Text" ? "Basic" : Config.DefaultHtmlCardModel;

            var ankiProcess = Process.GetProcessesByName("anki").FirstOrDefault();
            if (ankiProcess == null)
            {
                MessageBox.Show("Anki is not running. Make sure Anki (with AnkiConnect installed) is running.");
                return (null, null);
            }

            try
            {
                (bool checkIfExisting, Word[] wordsToProcessFiltered) = await GetItemsToProcess();
                if (wordsToProcessFiltered.Length == 0)
                    throw new Exception("No items left to process.");
                if (!await Config.AnkiProvider.CreateDeck(deck.Name))
                    throw new Exception("Could not create new deck.");
                if (deck.ExportOption == "HTML" && !await Config.AnkiProvider.AddModel(Config.DefaultHtmlCardModel, Properties.Resources.HtmlFront, Properties.Resources.HtmlBack, Properties.Resources.HtmlCss))
                    throw new Exception("Could not create html card model.");
                return await ImportToAnki(checkIfExisting, wordsToProcessFiltered);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return (null, null);

            static void SetWordCollectionImportDate(Word[] words, DateTime importDate)
            {
                foreach (var word in words)
                    word.ImportDate = importDate;
            }

            async Task<(bool, Word[])> GetItemsToProcess()
            {
                var checkIfExisting = true;

                var wordsToProcess = wordsToImport;
                var existingIds = await Config.AnkiProvider.GetDeckNotes(deck.Name);
                if (existingIds != null && existingIds.Count != 0 &&
                    MessageBox.Show("Deck with words already exists in Anki. Do you want to reset the imported words? This will remove any deck progress.", Config.ApplicationName, MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    checkIfExisting = false;
                    if (await Config.AnkiProvider.DeleteDeck(deck.Name))
                        SetWordCollectionImportDate(wordsToProcess, default);
                    else
                        throw new Exception("Could not delete existing deck.");
                }
                else
                    wordsToProcess = wordsToProcess.Where(word => word.ImportDate == default).ToArray();
                return (checkIfExisting, wordsToProcess.Where(word => word.Entries.Count > 0).ToArray());
            }

            async Task<(Word[], List<string> errorWordStrings)> ImportToAnki(bool checkIfExisting, Word[] words)
            {
                var success = false;
                var errorWordStrings = new List<string>();

                if (words.Length == 1)
                {
                    success = await Config.AnkiProvider.AddNote(deck.Name, modelName, words[0], formatter, checkIfExisting);
                    if (!success)
                        errorWordStrings.Add(words[0].InputWord);
                }
                else if (words.Length > 1)
                {
                    words = words.OrderBy(a => a.InputWord, Config.Comparer).ToArray();
                    var (result, ErrorWords) = await Config.AnkiProvider.AddNotes(deck.Name, modelName, words, formatter);
                    if (result)
                    {
                        success = true;
                        errorWordStrings.AddRange(ErrorWords);
                    }
                }

                if (!success)
                {
                    MessageBox.Show("Error importing into Anki.");
                    return (null, null);
                }

                var successfulWords = words.Where(word => !errorWordStrings.Contains(word.InputWord)).ToArray();
                SetWordCollectionImportDate(successfulWords, DateTime.Now);
                return (successfulWords, errorWordStrings);
            }
        }

        public static void SaveDeck(Deck deck, Word[] words)
        {
            deck.DateModified = DateTime.Now;
            Directory.CreateDirectory(Deck.DefaultDecksPath);
            File.WriteAllText(deck.FilePath, Word.Serialize(words));
        }
    }
}