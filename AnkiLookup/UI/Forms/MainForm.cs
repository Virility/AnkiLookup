using AnkiLookup.Core.Helpers;
using AnkiLookup.Core.Models;
using AnkiLookup.Core.Providers;
using AnkiLookup.UI.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly AnkiProvider _ankiProvider;
        private readonly CambridgeProvider _cambridgeProvider;
        private readonly IWordInfoFormatter _htmlFormatter;
        private readonly IWordInfoFormatter _simpleTextFormatter;
        private readonly IWordInfoFormatter _textFormatter;
        private readonly Comparer<string> _comparer;

        private string _lastOpenedDeckName;
        private bool _changeMade;

        public MainForm()
        {
            _ankiProvider = new AnkiProvider(FindOrCreateConfig());
            _cambridgeProvider = new CambridgeProvider(CambridgeDataSet.British);
            _htmlFormatter = new HtmlFormatter();
            _simpleTextFormatter = new SimpleTextFormatter();
            _textFormatter = new TextFormatter();
            _comparer = new OrdinalIgnoreCaseComparer();

            InitializeComponent();
            LoadDecks();
        }
    
        private Uri FindOrCreateConfig()
        {
            Uri host;

            Config.ConfigurationFile = new IniFile(Config.ConfigurationFilePath);

            if (File.Exists(Config.ConfigurationFilePath))
            {
                host = new Uri(Config.ConfigurationFile.IniReadValue(Config.Section, Config.HostKey));
                _lastOpenedDeckName = Config.ConfigurationFile.IniReadValue(Config.Section, Config.LastOpenedDeckNameKey);
            }
            else
            {
                host = new Uri(Config.DefaultHost);
                Config.ConfigurationFile.IniWriteValue(Config.Section, Config.HostKey, Config.DefaultHost);

                _lastOpenedDeckName = string.Empty;
                Config.ConfigurationFile.IniWriteValue(Config.Section, Config.LastOpenedDeckNameKey, _lastOpenedDeckName);

            }
            return host;
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_changeMade)
                SaveDecks();
        }

        private void LoadDecks()
        {
            var deckViewItemsList = new List<DeckViewItem>();
            if (!File.Exists(Config.DeckInformationFilePath))
            {
                foreach (var dataFilePath in Directory.GetFiles(Config.ApplicationPath, "*.dat"))
                {
                    var deck = new Deck
                    {
                        Name = Path.GetFileNameWithoutExtension(dataFilePath),
                        FilePath = dataFilePath
                    };
                    deckViewItemsList.Add(new DeckViewItem(deck));
                    _changeMade = true;
                }
                lvDecks.Items.AddRange(deckViewItemsList.ToArray());
                return;
            }

            var content = File.ReadAllText(Config.DeckInformationFilePath);
            foreach (var deck in JsonConvert.DeserializeObject<Deck[]>(content))
                deckViewItemsList.Add(new DeckViewItem(deck));
            lvDecks.Items.AddRange(deckViewItemsList.ToArray());

            if (string.IsNullOrWhiteSpace(_lastOpenedDeckName))
                return;

            DeckViewItem lastOpenedDeckViewItem = null;
            foreach (DeckViewItem deckViewItem in lvDecks.Items)
            {
                if (deckViewItem.Deck.Name == _lastOpenedDeckName)
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
            File.WriteAllText(Config.DeckInformationFilePath, JsonConvert.SerializeObject(decks));

            Config.ConfigurationFile.IniWriteValue(Config.Section, Config.LastOpenedDeckNameKey, _lastOpenedDeckName);
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
                _lastOpenedDeckName = deckManagementDialog.Deck.Name;
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

                var deck = new Deck
                {
                    Name = Path.GetFileNameWithoutExtension(dialog.FileName),
                    FilePath = dialog.FileName
                };
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
    }
}