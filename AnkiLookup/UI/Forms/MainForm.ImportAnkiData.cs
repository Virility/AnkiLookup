using AnkiLookup.Core.Models;
using AnkiLookup.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.UI.Forms
{
    partial class MainForm
    {
        private void SetWordInfoStates(WordViewItem wordViewItem, bool addedBefore, DateTime dateTime)
        {
            wordViewItem.WordInfo.AddedBefore = addedBefore;
            wordViewItem.WordInfo.ImportedIntoAnki = dateTime;
            wordViewItem.Refresh();
        }

        private void ResetWordInfo(ICollection<WordViewItem> wordViewItems)
        {
            var dateTime = default(DateTime);
            foreach (var wordViewItem in wordViewItems)
                SetWordInfoStates(wordViewItem, false, dateTime);
        }

        private static void CategorizeWordInfos(ICollection<WordViewItem> wordViewItemsToProcess,
            List<CambridgeWordInfo> wordInfos, List<CambridgeWordInfo> addedBeforeWordInfos)
        {
            foreach (WordViewItem wordViewItem in wordViewItemsToProcess)
            {
                if (wordViewItem.WordInfo.Entries.Count == 0)
                    continue;

                if (wordViewItem.WordInfo.AddedBefore)
                    addedBeforeWordInfos.Add(wordViewItem.WordInfo);
                else
                    wordInfos.Add(wordViewItem.WordInfo);
            }
        }

        private void ProcessImportResult(ICollection<WordViewItem> wordViewItemsToProcess, bool result, List<string> errorWords)
        {
            if (result)
            {
                var dateTime = DateTime.Now;
                foreach (var wordViewItem in wordViewItemsToProcess)
                {
                    if (errorWords.Contains(wordViewItem.WordInfo.InputWord))
                        continue;

                    SetWordInfoStates(wordViewItem, true, dateTime);
                    _changeMade = true;
                }

                if (errorWords.Count == 0)
                    MessageBox.Show("Successfully imported into Anki.");
                else
                    MessageBox.Show($"Successfully imported into Anki with some errors:\n{string.Join(Environment.NewLine, errorWords)}.");
            }
            else
                MessageBox.Show("Error importing into Anki.");
        }

        private async void tsmiImportToAnki_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Do you have Anki with AnkiConnect installed running?", "AnkiLookup", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes)
                return;

            var checkIfExisting = true;
            ICollection<WordViewItem> wordViewItemsToProcess;
            dialogResult = MessageBox.Show("Do you want to reset Anki imported words? This will remove any deck progress.", "AnkiLookup", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                checkIfExisting = false;
                if (!await _ankiProvider.DeleteDeck(_ankiProvider.DeckName))
                    return;

                wordViewItemsToProcess = lvWords.Items.Cast<WordViewItem>().ToArray();
                ResetWordInfo(wordViewItemsToProcess);
            }
            else
            {
                wordViewItemsToProcess = lvWords.Items.Cast<WordViewItem>()
                    .Where(item => item.WordInfo.ImportedIntoAnki == default(DateTime)).ToArray();
            }

            if (!await _ankiProvider.CreateDeck(_ankiProvider.DeckName))
            {
                Debug.WriteLine("Could not create deck.");
                return;
            }

            var wordInfos = new List<CambridgeWordInfo>();
            var addedBeforeWordInfos = new List<CambridgeWordInfo>();
            CategorizeWordInfos(wordViewItemsToProcess, wordInfos, addedBeforeWordInfos);
            if (wordInfos.Count == 0 && addedBeforeWordInfos.Count == 0)
                return;

            var result = false;
            var errorWords = new List<string>();

            if (wordInfos.Count == 1)
                result = await _ankiProvider.AddCard(_ankiProvider.DeckName, wordInfos[0], _htmlFormatter, checkIfExisting);
            else if (wordInfos.Count > 1)
            {
                var words = wordInfos.OrderBy(a => a.InputWord, _comparer).ToList();
                var (Success, ErrorWords) = await _ankiProvider.AddCards(_ankiProvider.DeckName, words, _htmlFormatter);
                if (Success)
                {
                    result = true;
                    errorWords.AddRange(ErrorWords);
                    foreach (WordViewItem wordViewItem in wordViewItemsToProcess)
                    {
                        if (errorWords.Contains(wordViewItem.WordInfo.InputWord))
                            wordViewItem.WordInfo.AddedBefore = true;
                    }
                }
            }

            foreach (var wordInfo in addedBeforeWordInfos)
            {
                if (await _ankiProvider.AddCard(_ankiProvider.DeckName, wordInfo, _htmlFormatter, checkIfExisting))
                    result = true;
                else
                    errorWords.Add(wordInfo.InputWord);
            }

            ProcessImportResult(wordViewItemsToProcess, result, errorWords);
        }
    }
}