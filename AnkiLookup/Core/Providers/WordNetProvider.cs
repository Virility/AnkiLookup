using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AnkiLookup.Core.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnkiLookup.Core.Providers
{
    public class WordNetProvider : IDisposable, IDictionaryResolver
    {
        private const string DefinitionPatten = "[(][\\w,]{1,3}[)]\\s(.+?) [(](.+?)[)]\\s?(\".+\")?";
        private readonly HttpClient _client;

        public WordNetProvider()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://wordnetweb.princeton.edu/");
        }
        
        public async Task<Word> GetWord(string wordText)
        {
            var urlEncodedWord = WebUtility.UrlEncode(wordText);
            var response = await _client.GetStringAsync($"/perl/webwn?s={urlEncodedWord}");

            if (response.Contains("Your search did not return any results."))
                return null;

            var parser = new HtmlParser();
            var document = parser.ParseDocument(response);

            var word = new Word();
            word.InputWord = wordText;

            var regex = new Regex(DefinitionPatten);
            foreach (var entryElement in document.QuerySelectorAll("ul"))
            {
                var parsedEntry = ParseEntryFromEntryElement(entryElement, regex);
                if (parsedEntry != null)
                    word.Entries.Add(parsedEntry);
            }
            return word;
        }

        private static Word.Entry ParseEntryFromEntryElement(IElement entryElement, Regex regex)
        {
            var entry = new Word.Entry();
            entry.ActualWord = entryElement.QuerySelector("b").TextContent;
            entry.Label = entryElement.PreviousElementSibling.TextContent.ToLower();

            foreach (var definitionEntry in entryElement.QuerySelectorAll("li"))
            {
                if (!regex.IsMatch(definitionEntry.TextContent))
                    continue;
                var groups = regex.Match(definitionEntry.TextContent).Groups;
                if (groups.Count == 0)
                    continue;

                var block = new Word.Block(groups[2].Value);
                if (groups.Count > 2)
                    block.Examples.AddRange(groups[3].Value.Replace("\"", string.Empty).Split("; "));
                entry.Definitions.Add(block);
            }
            return entry;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}