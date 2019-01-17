using AnkiLookup.Core.Helpers;
using AnkiLookup.Core.Models;
using AnkiLookup.Core.Providers;
using AnkiLookup.UI.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly string _decksInformationFile = Path.Combine(Config.ApplicationPath, "Decks.json");
        private readonly AnkiProvider _ankiProvider;
        private readonly CambridgeProvider _cambridgeProvider;
        private readonly IWordInfoFormatter _htmlFormatter;
        private readonly IWordInfoFormatter _simpleTextFormatter;
        private readonly IWordInfoFormatter _textFormatter;
        private readonly Comparer<string> _comparer;
       
        private bool _changeMade;

        public MainForm()
        {
            _ankiProvider = new AnkiProvider();
            _cambridgeProvider = new CambridgeProvider(CambridgeDataSet.British);
            _htmlFormatter = new HtmlFormatter();
            _simpleTextFormatter = new SimpleTextFormatter();
            _textFormatter = new TextFormatter();
            _comparer = new OrdinalIgnoreCaseComparer();

            InitializeComponent();
            LoadDecks();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_changeMade)
                SaveDecks();
        }

        private void LoadDecks()
        {
            if (!File.Exists(_decksInformationFile))
                return;

            var content = File.ReadAllText(_decksInformationFile);
            var decks = JsonConvert.DeserializeObject<Deck[]>(content);

            var deckViewItemsList = new List<DeckViewItem>();
            foreach (var deck in decks)
                deckViewItemsList.Add(new DeckViewItem(deck));
            lvDecks.Items.AddRange(deckViewItemsList.ToArray());

            if (string.IsNullOrWhiteSpace(_ankiProvider.LastOpenedDeckName))
                return;

            DeckViewItem lastOpenedDeckViewItem = null;
            foreach (DeckViewItem deckViewItem in lvDecks.Items)
            {
                if (deckViewItem.Deck.Name == _ankiProvider.LastOpenedDeckName)
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
            File.WriteAllText(_decksInformationFile, JsonConvert.SerializeObject(decks));

            Config.ConfigurationFile.IniWriteValue(Config.Section, Config.LastOpenedDeckNameKey, _ankiProvider.LastOpenedDeckName);
        }


        private void HandleDeckManagement(DeckViewItem deckViewItem)
        {
            using (var deckManagementDialog = new DeckManagementForm(deckViewItem.Deck))
            {
                deckManagementDialog.SetDependencies(_ankiProvider, _cambridgeProvider, _htmlFormatter, _simpleTextFormatter, _textFormatter, _comparer);

                Hide();
                var dialogResult = deckManagementDialog.ShowDialog();
                Show();
                if (dialogResult != DialogResult.OK)
                    return;

                _changeMade = true;
                _ankiProvider.LastOpenedDeckName = deckManagementDialog.Deck.Name;
                deckViewItem.Deck = deckManagementDialog.Deck;
            }
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
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var deck = new Deck();
                deck.FilePath = dialog.FileName;
                var deckViewItem = new DeckViewItem(deck);
                lvDecks.Items.Add(deckViewItem);

                HandleDeckManagement(deckViewItem);
                _changeMade = true;
            }
        }

        private void tsmiManageSelected_Click(object sender, EventArgs e)
        {
            if (lvDecks.SelectedItems.Count == 0)
                return;

            var deckViewItem = lvDecks.SelectedItems[0] as DeckViewItem;
            HandleDeckManagement(deckViewItem);
        }

        private void lvDecks_DoubleClick(object sender, EventArgs e)
        {
            tsmiManageSelected_Click(this, null);
        }
    }
}
