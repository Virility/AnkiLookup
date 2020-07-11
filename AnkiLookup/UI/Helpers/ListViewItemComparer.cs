using System;
using System.Collections;
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
            if (_listView.Sorting == SortOrder.Descending)
                returnVal *= -1;

            return returnVal;
        }
    }
}