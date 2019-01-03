using AnkiSpanishDictWordOfTheDay.Core.Models;
using System.IO;
using System.Reflection;

namespace AnkiLookup
{
    public static class Config
    {
        public const string Section = "Anki";
        public const string HostKey = "Host";
        public const string LastUpdatedKey = "LastUpdated";
        public const string DeckNameKey = "DeckName";
        public const string ExportOptionKey = "ExportOption";

        public static IniFile ConfigurationFile;

        private static string _applicationPath;

        public static string ApplicationPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_applicationPath))
                    _applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return _applicationPath;
            }
        }

        public static string ConfigurationFilePath => Path.Combine(ApplicationPath, "Anki.cfg");
    }
}