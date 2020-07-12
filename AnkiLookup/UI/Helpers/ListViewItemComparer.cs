using AnkiLookup.UI.Controls;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace AnkiLookup.Core.Helpers
{
    public class ListViewItemComparer : IComparer
    {
        public int Column { get; set; }

        private readonly ListView _listView;

        public ListViewItemComparer()
        {
            Column = 0;
        }

        public ListViewItemComparer(ListView listView, int column = 0)
        {
            Column = column;
            _listView = listView;
        }

        public int Compare(object x, object y)
        {
            int returnVal;

            var text = ((ListViewItem)x).SubItems[Column].Text;
            var text2 = ((ListViewItem)y).SubItems[Column].Text;

            returnVal = 0;

            if (Column == 1 && DateTime.TryParse(text, out var date) && DateTime.TryParse(text, out var date2))
                returnVal = DateTime.Compare(date, date2);
            else if (Column == 0 || Column == 1)
                returnVal = string.Compare(text, text2);
            else if (Column == 2 && x is WordViewItem && y is WordViewItem)
            {
                var x2 = x as WordViewItem;
                var y2 = y as WordViewItem;

                if (x2.Word.Entries != null && y2.Word.Entries != null)
                {
                    if (x2.Word.Entries.Count == y2.Word.Entries.Count)
                    {
                        var x2DefinitionsCount = x2.Word.Entries.Sum(entry => entry.Definitions.Count);
                        var y2DefinitionsCount = y2.Word.Entries.Sum(entry => entry.Definitions.Count);
                        returnVal = x2DefinitionsCount.CompareTo(y2DefinitionsCount);
                    } else
                        returnVal = x2.Word.Entries.Count.CompareTo(y2.Word.Entries.Count);
                }
            }

            if (_listView.Sorting == SortOrder.Descending)
                returnVal *= -1;
            return returnVal;
        }
    }
}