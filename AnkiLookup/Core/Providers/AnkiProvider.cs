using AnkiLookup.Core.Helpers.Formatters;
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
        private readonly HttpClient _client;

        public AnkiProvider(Uri host)
        {
            _client = new HttpClient();
            _client.BaseAddress = host;
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

                var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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

                var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return content.Contains("\"error\": null");
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> MoveDeck(string oldDeckName, string newDeckName = null)
        {
            try
            {
                var existingIds = await GetDeckCards(oldDeckName);
                var exists = existingIds != null && existingIds.Count != 0;
                if (!exists)
                    return false;

                var data = JsonConvert.SerializeObject(new
                {
                    action = "changeDeck",
                    version = 6,
                    @params = new
                    {
                        deck = newDeckName,
                        cards = existingIds
                    }
                });

                var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                //return content == "{\"result\": null, \"error\": null}";

                dynamic deserialized = JsonConvert.DeserializeObject(content);
                return deserialized.error == null && deserialized.result == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private dynamic CreateNote(string deckName, string modelName, string front, string back)
        {
            dynamic note = new ExpandoObject();
            note.deckName = deckName;
            note.modelName = modelName;
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

        public Task<bool> AddNote(string deckName, string modelName, Word word, IWordFormatter formatter, bool checkIfExisting = false)
        {
            var front = word.InputWord;
            var back = word.AsFormatted(formatter);
            return AddNote(deckName, modelName, front, back, checkIfExisting);
        }

        public async Task<List<long>> GetDeckCards(string deckName, string additionalQuery = null)
        {
            var query = $"deck:\"{deckName}\"";
            if (!string.IsNullOrWhiteSpace(additionalQuery))
                query += " " + additionalQuery;
            return await FindCards(query).ConfigureAwait(false);
        }


        public async Task<List<long>> GetDeckNotes(string deckName, string additionalQuery = null)
        {
            var query = $"deck:\"{deckName}\"";
            if (additionalQuery != null)
                query += " " + additionalQuery;
            return await FindNotes(query).ConfigureAwait(false);
        }

        public async Task<bool> AddNote(string deckName, string modelName, string front, string back, bool checkIfExisting = false)
        {
            try
            {
                if (checkIfExisting)
                {
                    var existingIds = await GetDeckNotes(deckName, $"front:\"{front}\"");
                    var exists = existingIds != null && existingIds.Count != 0;
                    if (exists)
                        return await UpdateNoteFields(existingIds[0], front, back).ConfigureAwait(false);
                }

                dynamic note = CreateNote(deckName, modelName, front, back);
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

                var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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

            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content.Contains("\"error\": null");
        }

        private async Task<List<long>> FindCards(string query)
        {
            var postData = new
            {
                action = "findCards",
                version = 6,
                @params = new
                {
                    query
                }
            };
            var data = JsonConvert.SerializeObject(postData);

            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            dynamic deserialized = JsonConvert.DeserializeObject(content);
            if (deserialized.error != null)
                return null;

            var ids = new List<long>();
            foreach (JValue item in deserialized["result"])
                ids.Add((long)item);
            return ids;
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

            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            dynamic deserialized = JsonConvert.DeserializeObject(content);
            if (deserialized.error != null)
                return null;

            var ids = new List<long>();
            foreach (JValue item in deserialized["result"])
                ids.Add((long)item);
            return ids;
        }

        public async Task<(bool Success, List<string> ErrorWords)> AddNotes(string deckName, string modelName, Word[] words, IWordFormatter formatter)
        {
            try
            {
                var notes = new List<dynamic>();
                foreach (var word in words)
                    notes.Add(CreateNote(deckName, modelName, word.InputWord, word.AsFormatted(formatter)));

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

                var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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

        public async Task<bool> AddModel(string name, string front, string back, string style)
        {
            dynamic noteModel = new ExpandoObject();
            noteModel.name = name;
            noteModel.css = style;
            noteModel.tmpls = new[]
            {
                new {
                    name = "Card 1",
                    qfmt = front,
                    afmt = back
                }
            };
            var postData = new
            {
                action = "addModel",
                version = 6,
                @params = new
                {
                    noteModel
                }
            };
            var data = JsonConvert.SerializeObject(postData);
            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(data)).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (content.Contains("\"error\": null"))
                return true;
            if (content.Contains("model already exists"))
                return true;
            return false;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}