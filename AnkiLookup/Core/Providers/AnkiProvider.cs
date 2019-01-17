using AnkiLookup.Core.Helpers;
using AnkiLookup.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnkiLookup.Core.Providers
{
    public class AnkiProvider : IDisposable
    {
        private const string DefaultHost = "http://localhost:8765";
        private readonly HttpClient _client;
        
        private Uri Host;

        public string LastOpenedDeckName { get; internal set; }

        public AnkiProvider()
        {
            FindOrCreateConfig();

            _client = new HttpClient();
            _client.BaseAddress = Host;
        }

        private void FindOrCreateConfig()
        {
            if (File.Exists(Config.ConfigurationFilePath))
            {
                Config.ConfigurationFile = new IniFile(Config.ConfigurationFilePath);
                Host = new Uri(Config.ConfigurationFile.IniReadValue(Config.Section, Config.HostKey));
                LastOpenedDeckName = Config.ConfigurationFile.IniReadValue(Config.Section, Config.LastOpenedDeckNameKey);
            }
            else
            {
                var configurationFile = new IniFile(Config.ConfigurationFilePath);
                Host = new Uri(DefaultHost);
                configurationFile.IniWriteValue(Config.Section, Config.HostKey, DefaultHost);
                LastOpenedDeckName = string.Empty;
                configurationFile.IniWriteValue(Config.Section, Config.LastOpenedDeckNameKey, LastOpenedDeckName);
                Config.ConfigurationFile = configurationFile;
            }
        }

    public async Task<bool> CreateDeck(string deckName = null)
        {
            try
            {
                var data = JsonConvert.SerializeObject(new
                {
                    action = "createDeck",
                    version = 6,
                    @params = new
                    {
                        deck = deckName
                    }
                });

                var response = await _client.PostAsync(Host, new StringContent(data));
                var content = await response.Content.ReadAsStringAsync();
                return content.Contains("\"error\": null");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDeck(string deckName = null)
        {
            try
            {
                var data = JsonConvert.SerializeObject(new
                {
                    action = "deleteDecks",
                    version = 6,
                    @params = new
                    {
                        decks = new string[] { deckName },
                        cardsToo = true
                    }
                });

                var response = await _client.PostAsync(Host, new StringContent(data));
                var content = await response.Content.ReadAsStringAsync();
                return content.Contains("\"error\": null");
            }
            catch (Exception)
            {
                return false;
            }
        }

        private dynamic CreateNote(string deckName, string front, string back)
        {
            dynamic note = new ExpandoObject();
            note.deckName = deckName;
            note.modelName = "Basic";
            note.fields = new ExpandoObject();
            note.fields.Front = front;
            note.fields.Back = back;
            note.tags = new[] { string.Empty };
            return note;
        }

        private dynamic CreateNote(long id, string front, string back)
        {
            dynamic note = new ExpandoObject();
            note.id = id;
            note.fields = new ExpandoObject();
            note.fields.Front = front;
            note.fields.Back = back;
            return note;
        }

        public Task<bool> AddNote(string deckName, CambridgeWordInfo wordInfo, IWordInfoFormatter formatter, bool checkIfExisting = false)
        {
            var front = wordInfo.InputWord;
            var back = wordInfo.AsFormatted(formatter);
            return AddNote(deckName, front, back, checkIfExisting);
        }

        public async Task<bool> AddNote(string deckName, string front, string back, bool checkIfExisting = false)
        {
            try
            {
                if (checkIfExisting)
                {
                    var existingIds = await FindNotes($"deck:\"{deckName}\" front:\"{front}\"");
                    if (existingIds != null && existingIds.Count != 0)
                        return await UpdateNoteFields(existingIds[0], front, back);
                }

                dynamic note = CreateNote(deckName, front, back);
                var postData = new
                {
                    action = "addNote",
                    version = 6,
                    @params = new
                    {
                        note
                    }
                };

                var data = JsonConvert.SerializeObject(postData);

                var response = await _client.PostAsync(Host, new StringContent(data));
                var content = await response.Content.ReadAsStringAsync();
                return content.Contains("\"error\": null");
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> UpdateNoteFields(long id, string front, string back)
        {
            dynamic note = CreateNote(id, front, back);
            var postData = new
            {
                action = "updateNoteFields",
                version = 6,
                @params = new
                {
                    note
                }
            };
            var data = JsonConvert.SerializeObject(postData);

            var response = await _client.PostAsync(Host, new StringContent(data));
            var content = await response.Content.ReadAsStringAsync();
            return content.Contains("\"error\": null");
        }

        private async Task<List<long>> FindNotes(string query)
        {
            var postData = new
            {
                action = "findNotes",
                version = 6,
                @params = new
                {
                    query
                }
            };
            var data = JsonConvert.SerializeObject(postData);

            var response = await _client.PostAsync(Host, new StringContent(data));
            var content = await response.Content.ReadAsStringAsync();
            dynamic deserialized = JsonConvert.DeserializeObject(content);
            if (deserialized.error != null)
                return null;

            var ids = new List<long>();
            foreach (JValue item in deserialized["result"])
                ids.Add((long)item);
            return ids;
        }

        public async Task<(bool Success, List<string> ErrorWords)> AddNotes(string deckName, List<CambridgeWordInfo> words, IWordInfoFormatter formatter)
        {
            try
            {
                var notes = new List<dynamic>();
                foreach (var word in words)
                    notes.Add(CreateNote(deckName, word.InputWord, word.AsFormatted(formatter)));

                var postData = new
                {
                    action = "addNotes",
                    version = 6,
                    @params = new
                    {
                        notes
                    }
                };

                var data = JsonConvert.SerializeObject(postData);

                var response = await _client.PostAsync(Host, new StringContent(data));
                var content = await response.Content.ReadAsStringAsync();
                dynamic deserialized = JsonConvert.DeserializeObject(content);
                if (deserialized.error != null)
                    return (Success: false, ErrorWords: null);

                var errorWords = new List<string>();
                var index = 0;
                foreach (JValue item in deserialized["result"])
                {
                    if (item.Value == null)
                        errorWords.Add(words[index].InputWord);
                    index++;
                }
                return (Success: true, ErrorWords: errorWords);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AddNotes: {ex.Message}");
                return (Success: false, ErrorWords: null);
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}