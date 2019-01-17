using AnkiLookup.Core.Models;
using AnkiLookup.UI.Controls;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnkiLookup.UI.Forms.Controls
{
    public class WordListView : ListView
    {
        public List<CambridgeWordInfo> GetAsWordInfoList()
        {
            var wordInfos = new List<CambridgeWordInfo>();
            foreach (var wordViewItem in Items)
                wordInfos.Add((wordViewItem as WordViewItem)?.WordInfo);
            return wordInfos;
        }
    }
}