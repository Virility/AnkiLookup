using AnkiLookup.Core.Helpers.Formatters;
using AnkiLookup.Core.Models;
using AnkiLookup.Core.Providers;
using AnkiLookup.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnkiLookup.UI.Forms
{
    partial class WordManagementForm
    {
        private void SetWordCollectionImportDate(ICollection<WordViewItem> wordViewItems, DateTime importDate)
        {
            void UpdateWordImportDate(WordViewItem wordViewItem)
            {
                wordViewItem.Word.ImportDate = importDate;
                wordViewItem.Refresh();
                _changeMade = true;
            }

            lvWords.BeginUpdate();
            foreach (var wordViewItem in wordViewItems)
                UpdateWordImportDate(wordViewItem);
            lvWords.EndUpdate();

            lvWords.Invalidate();
        }

        private async void tsmiImportToAnki_Click(object sender, EventArgs e)
        {
            var wordViewItemsToImport = lvWords.GetAsWordViewItemList();

            var wordsToImport = wordViewItemsToImport.Select(wordViewItem => wordViewItem.Word).ToArray();
            (Word[] SuccessfulWords, List<string> ErrorWordStrings) = await DeckManagementForm.ImportDeck(Deck, wordsToImport);
            if (SuccessfulWords == null)
                return;

            lvWords.BeginUpdate();
            foreach (var wordViewItem in wordViewItemsToImport)
            {
                var successfulWord = SuccessfulWords.FirstOrDefault(word => word.InputWord == wordViewItem.Word.InputWord);
                if (successfulWord == null)
                    continue;
                wordViewItem.Word = successfulWord;
                wordViewItem.Refresh();
            }
            lvWords.EndUpdate();

            MessageBox.Show($"Successfully imported {(ErrorWordStrings.Count == 0 ? "all" : "some")} words from deck \"{Deck.Name}\" into Anki.");
            if (ErrorWordStrings.Count != 0)
                MessageBox.Show($"Errors:\n{string.Join(Environment.NewLine, ErrorWordStrings)}.");
        }

        private void tsmiClearImportStates_Click(object sender, EventArgs e)
        {
            SetWordCollectionImportDate(lvWords.GetAsWordViewItemList(), default);
        }
    }
}