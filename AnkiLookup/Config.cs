using AnkiLookup.Core.Models;
using System.IO;
using System.Reflection;

namespace AnkiLookup
{
    public static class Config
    {
        public const string Section = "Anki";
        public const string HostKey = "Host";
        public const string LastOpenedDeckNameKey = "LastOpenedDeckName";

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

        public static string DefaultHost = "";

        public static string ConfigurationFilePath = Path.Combine(ApplicationPath, "Anki.cfg");

        public static string DeckInformationFilePath = Path.Combine(ApplicationPath, "Decks.json");
    }
}