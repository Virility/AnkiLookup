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
            foreach (var item in Items)
                wordInfos.Add((item as WordViewItem)?.WordInfo);
            return wordInfos;
        }
    }
}