using AnkiLookup.Core.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class WordViewItem : ListViewItem
    {
        private Word word;
        public Word Word {
            get { return word; }
            set {
                word = value;
                Refresh();
            }
        }

        public void Refresh()
        {
            if (word == null)
                return;

            var i = 0;
            Text = Name = word.InputWord;
            i++;

            string data;
            if (word.ImportDate != default)
                data = word.ImportDate.ToShortDateString();
            else
                data = "Not Added";
            if (SubItems.Count > i)
                SubItems[i].Text = data;
            else
                SubItems.Add(data);
            i++;

            data = word.Entries.Count.ToString();
            if (word.Entries.Count != 0)
            {
                data += " ";
                if (word.Entries.Count > 1)
                    data += "entries";
                else if (word.Entries.Count == 1)
                    data += "entry";

                data += " - ";
                var totalDefinitions = word.Entries.ToArray().Sum(entry => entry.Definitions.Count);
                data += totalDefinitions + " definition";
                if (totalDefinitions > 1)
                    data += "s";
            }
            else
                data = "Not Looked Up.";
            if (SubItems.Count > i)
                SubItems[i].Text = data;
            else
                SubItems.Add(data);
            i++;
        }

        public WordViewItem(Word word)
        {
            Word = word;
        }

        public WordViewItem(string wordText)
        {
            var word = new Word();
            word.InputWord = wordText;
            Word = word;

            if (string.IsNullOrWhiteSpace(wordText))
                SubItems[2].Text = "Adding";
        }
    }
}
