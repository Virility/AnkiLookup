using System.Windows.Forms;

namespace AnkiLookup.UI.Models
{
    public partial class EditWordDefinitionForm : Form
    {
        public int EntryIndex { get { return (int) nudEntryIndex.Value; } set { nudEntryIndex.Value = value; } }

        public string Word { get { return tbWord.Text; } set { tbWord.Text = value; } }

        public string Label { get { return cbLabel.Text; } set { cbLabel.Text = value; } }

        public string Definition { get { return rtbDefinition.Text; } set { rtbDefinition.Text = value; } }

        public EditWordDefinitionForm(int entryCount, string word, string label = null, string definition = null)
        {
            InitializeComponent();

            nudEntryIndex.Maximum = entryCount;
            if (entryCount != 0)
                nudEntryIndex.Value = entryCount - 1;

            Word = word;
            if (!string.IsNullOrWhiteSpace(definition))
                Definition = definition;

            if (string.IsNullOrWhiteSpace(label))
                cbLabel.SelectedIndex = 0;
            else
                cbLabel.SelectedItem = label;
        }
    }
}