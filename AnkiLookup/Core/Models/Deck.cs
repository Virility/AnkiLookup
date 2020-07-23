using System;
using System.IO;

namespace AnkiLookup.Core.Models
{
    public class Deck
    {
        public const string DefaultDecksPath = "Decks\\";
        
        private const string DefaultExportOption = "Text";

        public static Deck DefaultDeck = new Deck("Vocabulary");

        public string Name { get; set; }

        public string FilePath { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; }

        public string ExportOption { get; set; } = DefaultExportOption;

        public Deck(string name, string filePath = null)
        {
            Name = name;
            FilePath = string.IsNullOrWhiteSpace(filePath) ? GetDeckFilePathFromDeckName(name) : filePath;
        }

        public static Deck FromFilePath(string filePath)
        {
            return new Deck(Path.GetFileNameWithoutExtension(filePath), filePath);
        }

        public static string GetDeckFilePathFromDeckName(string deckName)
        {
            return Path.Combine(DefaultDecksPath, deckName + "." + Config.DefaultDeckFileExtension);
        }
    }
}