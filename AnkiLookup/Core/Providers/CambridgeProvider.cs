using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using AnkiLookup.Core.Models;

namespace AnkiLookup.Core.Providers
{
    public class CambridgeProvider : IDisposable 
    {
        public CambridgeDataSet DataSet { get; set; }

        private readonly HttpClient _client;

        public CambridgeProvider(CambridgeDataSet dataSet)
        {
            DataSet = dataSet;

            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://dictionary.cambridge.org");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:61.0) Gecko/20100101 Firefox/61.0");
        }

        public async Task<Word> GetWord(string wordText)
        {
            var urlEncodedWord = WebUtility.UrlEncode(wordText);
            var response = await _client.GetStringAsync($"/us/dictionary/english/{urlEncodedWord}");
            if (response.Contains("Popular searches"))
                response = await _client.GetStringAsync($"/search/english/direct/?q={urlEncodedWord}");

            var parser = new HtmlParser();
            var document = parser.ParseDocument(response);

            var dataSetSelector = "div.entry";//GetSelectorFromDataSet(DataSet);
            var dataSetElement = document.QuerySelector(dataSetSelector);
            if (dataSetElement == null)
                return null;

            var word = new Word();
            word.InputWord = wordText;
            foreach (var entryElement in dataSetElement.QuerySelectorAll(".entry-body > div"))
            {
                var parsedEntry = ParseEntryFromEntryElement(entryElement);
                if (parsedEntry != null)
                    word.Entries.Add(parsedEntry);
            }
            return word;
        }

        //private static string GetSelectorFromDataSet(CambridgeDataSet dataSet)
        //{
        //    if (dataSet == CambridgeDataSet.British)
        //        return "div#dataset-cald4";
        //    if (dataSet == CambridgeDataSet.American)
        //        return "div#dataset-cacd";
        //    if (dataSet == CambridgeDataSet.Business)
        //        return "div#dataset-business-english";
        //    return string.Empty;
        //}

        private static Word.Entry ParseEntryFromEntryElement(IParentNode entryElement)
        {
            var entry = new Word.Entry();

            var headWordElement = entryElement.QuerySelector(".headword > span.hw");
            if (headWordElement == null)
                headWordElement = entryElement.QuerySelector(".headword > span.phrase");
            if (headWordElement != null)
                entry.ActualWord = headWordElement.TextContent;
            else
                return null;

            var labelElement = entryElement.QuerySelector(".posgram > span.pos");
            if (labelElement != null)
                entry.Label = labelElement.TextContent;

            entry.Definitions = ParseDefBlocksFromEntryElement(entryElement);
            return entry;
        }

        private static List<Word.Block> ParseDefBlocksFromEntryElement(IParentNode entryElement)
        {
            var blocks = new List<Word.Block>();
            foreach (var defBlockElement in entryElement.QuerySelectorAll("div.def-block"))
            {
                var definitionTextElement = defBlockElement.QuerySelector("div.def");
                if (definitionTextElement == null)
                    continue;

                var block = new Word.Block(Regex.Replace(definitionTextElement.TextContent, @"\s+", " "));
                if (block.Definition.EndsWith(": "))
                    block.Definition = block.Definition.Substring(0, block.Definition.Length - 2);

                var exampleElements = defBlockElement.QuerySelectorAll("div.examp");
                if (exampleElements.Length > 0)
                    block.Examples = new List<string>(exampleElements.Select(exampleElement => exampleElement.TextContent.Trim()));

                blocks.Add(block);
            }
            return blocks;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}