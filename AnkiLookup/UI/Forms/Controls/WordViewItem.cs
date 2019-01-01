using AnkiLookup.Core.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class WordViewItem : ListViewItem
    {
        private CambridgeWordInfo wordInfo;
        public CambridgeWordInfo WordInfo {
            get { return wordInfo; }
            set {
                wordInfo = value;
                Refresh();
            }
        }

        public void Refresh()
        {
            if (wordInfo == null)
                return;

            Text = wordInfo.InputWord;
            Name = wordInfo.InputWord;

            string data;
            if (wordInfo.ImportedIntoAnki != default(DateTime))
                data = wordInfo.ImportedIntoAnki.ToShortDateString();
            else
                data = "Not Added";
            if (SubItems.Count > 1)
                SubItems[1].Text = data;
            else
                SubItems.Add(data);

            data = wordInfo.Entries.Count.ToString();
            if (wordInfo.Entries.Count != 0)
            {
                data += " ";
                if (wordInfo.Entries.Count > 1)
                    data += "entries";
                else if (wordInfo.Entries.Count == 1)
                    data += "entry";

                data += " - ";
                var totalDefinitions = wordInfo.Entries.ToArray().Sum(entry => entry.Definitions.Count);
                data += totalDefinitions + " definition";
                if (totalDefinitions > 1)
                    data += "s";
            }
            else
                data = "Not Looked Up.";
            if (SubItems.Count > 2)
                SubItems[2].Text = data;
            else
                SubItems.Add(data);
        }

        public WordViewItem(CambridgeWordInfo wordInfo)
        {
            WordInfo = wordInfo;
        }

        public WordViewItem(string word)
        {
            var wordInfo = new CambridgeWordInfo();
            wordInfo.InputWord = word;
            WordInfo = wordInfo;

            if (string.IsNullOrWhiteSpace(word))
                SubItems[2].Text = "Adding";
        }
    }
}
