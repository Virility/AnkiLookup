using AnkiLookup.Core.Helpers.Formatters;
using AnkiLookup.Core.Models;
using AnkiLookup.Core.Providers;
using AnkiLookup.UI.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AnkiLookup
{
    public static class Config
    {
        private const string Section = "Anki";
        private const string HostKey = "Host";
        private const string LastOpenedDeckNameKey = "LastOpenedDeckName";

        private static readonly string ConfigurationFilePath = Path.Combine(ApplicationPath, "Anki.cfg");
        private static readonly IniFile ConfigurationFile = new IniFile(ConfigurationFilePath);

        public const string ApplicationName = "AnkiLookup";
        public static readonly string DefaultHost = "http://localhost:8765";
        public static readonly string DefaultHtmlCardModel = "AnkiLookupHtml";
        public static readonly string DefaultDeckFileExtension = "json";
        public static readonly string DeckRepositoryFilePath = Path.Combine(ApplicationPath, "Decks." + DefaultDeckFileExtension);
        public static readonly Formatting JsonFormatting = Formatting.Indented;
        public static readonly AnkiProvider AnkiProvider;
        public static readonly IWordFormatter HtmlFormatter;
        public static readonly IWordFormatter SimpleTextFormatter;
        public static readonly IWordFormatter TextFormatter;
        public static readonly Comparer<string> Comparer;

        static Config()
        {
            AnkiProvider = new AnkiProvider(AnkiHost);
            HtmlFormatter = new HtmlFormatter();
            SimpleTextFormatter = new SimpleTextFormatter();
            TextFormatter = new TextFormatter();
            Comparer = new OrdinalIgnoreCaseComparer();
        }

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

        private static Uri _ankiHost;
        public static Uri AnkiHost
        {
            get
            {
                if (_ankiHost != null)
                    return _ankiHost;

                if (File.Exists(ConfigurationFilePath) && File.ReadAllText(ConfigurationFilePath).Contains("HostKey"))
                    _ankiHost = new Uri(ConfigurationFile.IniReadValue(Section, HostKey));
                else
                    AnkiHost = new Uri(DefaultHost);
                return _ankiHost;
            }
            set
            {
                _ankiHost = value;
                ConfigurationFile.IniWriteValue(Section, HostKey, _ankiHost.ToString());
            }
        }

        private static string _lastOpenedDeckName;

        public static string LastOpenedDeckName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_lastOpenedDeckName))
                    return _lastOpenedDeckName;

                if (File.Exists(ConfigurationFilePath) && File.ReadAllText(ConfigurationFilePath).Length > 0)
                    _lastOpenedDeckName = ConfigurationFile.IniReadValue(Section, LastOpenedDeckNameKey);
                else
                    LastOpenedDeckName = string.Empty;
                return _lastOpenedDeckName;
            }
            set
            {
                _lastOpenedDeckName = value;
                ConfigurationFile.IniWriteValue(Section, LastOpenedDeckNameKey, _lastOpenedDeckName);
            }
        }
    }
}