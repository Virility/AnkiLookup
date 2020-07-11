using System.Windows.Forms;

namespace AnkiLookup.UI.Dialogs
{
    public partial class EditExampleForm : Form
    {
        public string Example { get { return rtbDefinition.Text; } set { rtbDefinition.Text = value; } }

        public EditExampleForm(string example = null)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(example))
                Example = example;
        }
    }
}