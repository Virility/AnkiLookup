﻿using AnkiLookup.Core.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class WordListView : ListView
    {
        public List<Word> GetAsWordList()
        {
            var words = new List<Word>();
            foreach (var wordViewItem in Items)
                words.Add((wordViewItem as WordViewItem)?.Word);
            return words;
        }
    }
}