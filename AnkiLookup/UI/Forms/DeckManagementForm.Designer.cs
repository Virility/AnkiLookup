namespace AnkiLookup.UI.Forms
{
    partial class DeckManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeckManagementForm));
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDeckNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDeckFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportToAnki = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDecks = new AnkiLookup.UI.Controls.WordListView();
            this.chDeckName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMain
            // 
            this.cmsMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddDeck,
            this.tsmiManageSelected,
            this.tsmiDeleteSelected,
            this.tsmiImportToAnki});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(236, 100);
            // 
            // tsmiAddDeck
            // 
            this.tsmiAddDeck.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddDeckNew,
            this.tsmiAddDeckFromFile});
            this.tsmiAddDeck.Name = "tsmiAddDeck";
            this.tsmiAddDeck.Size = new System.Drawing.Size(235, 24);
            this.tsmiAddDeck.Text = "Add Deck";
            // 
            // tsmiAddDeckNew
            // 
            this.tsmiAddDeckNew.Name = "tsmiAddDeckNew";
            this.tsmiAddDeckNew.Size = new System.Drawing.Size(153, 26);
            this.tsmiAddDeckNew.Text = "New";
            this.tsmiAddDeckNew.Click += new System.EventHandler(this.tsmiAddDeckNew_Click);
            // 
            // tsmiAddDeckFromFile
            // 
            this.tsmiAddDeckFromFile.Name = "tsmiAddDeckFromFile";
            this.tsmiAddDeckFromFile.Size = new System.Drawing.Size(153, 26);
            this.tsmiAddDeckFromFile.Text = "From File";
            this.tsmiAddDeckFromFile.Click += new System.EventHandler(this.tsmiAddDeckFromFile_Click);
            // 
            // tsmiManageSelected
            // 
            this.tsmiManageSelected.Name = "tsmiManageSelected";
            this.tsmiManageSelected.Size = new System.Drawing.Size(235, 24);
            this.tsmiManageSelected.Text = "Manage Selected";
            this.tsmiManageSelected.Click += new System.EventHandler(this.tsmiManageSelected_Click);
            // 
            // tsmiDeleteSelected
            // 
            this.tsmiDeleteSelected.Name = "tsmiDeleteSelected";
            this.tsmiDeleteSelected.Size = new System.Drawing.Size(235, 24);
            this.tsmiDeleteSelected.Text = "Delete Selected";
            this.tsmiDeleteSelected.Click += new System.EventHandler(this.tsmiDeleteSelected_Click);
            // 
            // tsmiImportToAnki
            // 
            this.tsmiImportToAnki.Name = "tsmiImportToAnki";
            this.tsmiImportToAnki.Size = new System.Drawing.Size(235, 24);
            this.tsmiImportToAnki.Text = "Import Selected to Anki";
            this.tsmiImportToAnki.Click += new System.EventHandler(this.tsmiImportToAnki_Click);
            // 
            // lvDecks
            // 
            this.lvDecks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDecks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDeckName,
            this.chCreated,
            this.chModified});
            this.lvDecks.ContextMenuStrip = this.cmsMain;
            this.lvDecks.FullRowSelect = true;
            this.lvDecks.HideSelection = false;
            this.lvDecks.Location = new System.Drawing.Point(12, 12);
            this.lvDecks.Name = "lvDecks";
            this.lvDecks.Size = new System.Drawing.Size(520, 321);
            this.lvDecks.TabIndex = 6;
            this.lvDecks.UseCompatibleStateImageBehavior = false;
            this.lvDecks.View = System.Windows.Forms.View.Details;
            this.lvDecks.DoubleClick += new System.EventHandler(this.lvDecks_DoubleClick);
            this.lvDecks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvDecks_KeyDown);
            // 
            // chDeckName
            // 
            this.chDeckName.Text = "Deck Name";
            this.chDeckName.Width = 234;
            // 
            // chCreated
            // 
            this.chCreated.Text = "Created";
            this.chCreated.Width = 122;
            // 
            // chModified
            // 
            this.chModified.Text = "Modified";
            this.chModified.Width = 122;
            // 
            // DeckManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 345);
            this.Controls.Add(this.lvDecks);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeckManagementForm";
            this.Text = "DeckManagementForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.WordListView lvDecks;
        private System.Windows.Forms.ColumnHeader chDeckName;
        private System.Windows.Forms.ColumnHeader chCreated;
        private System.Windows.Forms.ColumnHeader chModified;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDeck;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDeckNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDeckFromFile;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportToAnki;
    }
}