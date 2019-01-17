using System;
using System.IO;

namespace AnkiLookup.Core.Models
{
    public class Deck
    {
#if DEBUG
        private const string DefaultDeckName = "Vocabulary-TEST";
#else
        private const string DefaultDeckName = "Vocabulary";
#endif

        private static string DefaultFilePath = GetFilePathFromBaseDirectory();
        private const string DefaultExportOption = "Text";

        public string Name { get; set; } = DefaultDeckName;

        public string FilePath { get; set; } = DefaultFilePath;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; }

        public string ExportOption { get; set; } = DefaultExportOption;


        public static string GetFilePathFromBaseDirectory()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultDeckName + ".dat");
        }
    }
}