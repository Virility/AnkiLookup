using System;
using System.IO;

namespace AnkiLookup.Core.Models
{
    public class Deck
    {
        public static Deck DefaultDeck = new Deck("Vocabulary");

        private const string DefaultExportOption = "Text";
        public const string DefaultDecksPath = "Decks/";

        public string Name { get; set; }

        public string FilePath { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; }

        public string ExportOption { get; set; } = DefaultExportOption;

        public static string GetDeckFilePathFromDeckName(string deckName)
        {
            return Path.Combine(DefaultDecksPath, deckName + "." + Config.DefaultDeckFileExtension);
        }

        public Deck(string name, string filePath = null)
        {
            Name = name;
            FilePath = string.IsNullOrWhiteSpace(filePath) ? GetDeckFilePathFromDeckName(name) : filePath;
        }
    }
}