using System.Windows.Forms;

namespace AnkiLookup.UI.Dialogs
{
    public partial class EditMultilineForm : Form
    {
        public string Content { get { return rtbContent.Text; } set { rtbContent.Text = value; } }

        public EditMultilineForm(string label, string content = null)
        {
            InitializeComponent();
            lbLabel.Text = label;
            if (!string.IsNullOrWhiteSpace(content))
                Content = content;
        }
    }
}