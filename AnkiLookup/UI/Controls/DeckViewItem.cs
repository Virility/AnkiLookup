using AnkiLookup.Core.Models;
using System;
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

            var i = 0;
            Text = Name = _deck.Name;
            i++;

            var data = _deck.DateCreated.ToShortDateString();
            if (SubItems.Count > i)
                SubItems[i].Text = data;
            else
                SubItems.Add(data);
            i++;

            if (_deck.DateModified != default)
                data = _deck.DateModified.ToShortDateString();
            else
                data = "Not Modified";
            if (SubItems.Count > i)
                SubItems[i].Text = data;
            else
                SubItems.Add(data);
            i++;
        }

        public DeckViewItem(Deck deck = null)
        {
            Deck = deck ?? Deck.DefaultDeck;
        }
    }
}
