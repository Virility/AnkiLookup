using AnkiLookup.Core.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AnkiLookup.UI.Controls
{
    public class DeckViewItem : ListViewItem
    {
        private Deck _deck;
        public Deck Deck {
            get { return _deck; }
            set {
                _deck = value;
                Refresh();
            }
        }

        public void Refresh()
        {
            if (_deck == null)
                return;

            Text = _deck.Name;
            Name = _deck.Name;

            var data = _deck.DateCreated.ToShortDateString();
            if (SubItems.Count > 1)
                SubItems[1].Text = data;
            else
                SubItems.Add(data);

            if (_deck.DateModified != default(DateTime))
                data = _deck.DateModified.ToShortDateString();
            else
                data = "Not Modified";
            if (SubItems.Count > 2)
                SubItems[2].Text = data;
            else
                SubItems.Add(data);
        }

        public DeckViewItem(Deck deck = null)
        {
            Deck = (deck == null) ? new Deck() : deck;
            Debug.WriteLine(_deck.Name);
        }
    }
}
