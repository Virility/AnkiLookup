using AnkiLookup.UI.Forms.Controls;

namespace AnkiLookup.UI.Forms
{
    partial class MainForm
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
                components.Dispose();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadWordList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.getDefinitionsFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetDefinitionsFromCambridge = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportWordsForAnki = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportWordList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportToAnki = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearImportStates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddWord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditWord = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvWords = new AnkiLookup.UI.Forms.Controls.WordListView();
            this.chWord = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chImported = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtbWordOutput = new System.Windows.Forms.RichTextBox();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbHtml = new System.Windows.Forms.RadioButton();
            this.lbDeckName = new System.Windows.Forms.Label();
            this.tbDeckName = new System.Windows.Forms.TextBox();
            this.cmsMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoad,
            this.getDefinitionsFromToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.tsmiImportToAnki,
            this.tsmiClearImportStates,
            this.tsmiDeleteSelected,
            this.tsmiAddWord,
            this.tsmiEditWord});
            this.cmsMain.Name = "contextMenuStrip1";
            this.cmsMain.Size = new System.Drawing.Size(184, 180);
            // 
            // tsmiLoad
            // 
            this.tsmiLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadWordList,
            this.tsmiLoadDataFile});
            this.tsmiLoad.Name = "tsmiLoad";
            this.tsmiLoad.Size = new System.Drawing.Size(183, 22);
            this.tsmiLoad.Text = "Load From";
            // 
            // tsmiLoadWordList
            // 
            this.tsmiLoadWordList.Name = "tsmiLoadWordList";
            this.tsmiLoadWordList.Size = new System.Drawing.Size(124, 22);
            this.tsmiLoadWordList.Text = "Word List";
            this.tsmiLoadWordList.Click += new System.EventHandler(this.tsmiLoadWordList_Click);
            // 
            // tsmiLoadDataFile
            // 
            this.tsmiLoadDataFile.Name = "tsmiLoadDataFile";
            this.tsmiLoadDataFile.Size = new System.Drawing.Size(124, 22);
            this.tsmiLoadDataFile.Text = "Data File";
            this.tsmiLoadDataFile.Click += new System.EventHandler(this.tsmiLoadDataFile_Click);
            // 
            // getDefinitionsFromToolStripMenuItem
            // 
            this.getDefinitionsFromToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGetDefinitionsFromCambridge});
            this.getDefinitionsFromToolStripMenuItem.Name = "getDefinitionsFromToolStripMenuItem";
            this.getDefinitionsFromToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.getDefinitionsFromToolStripMenuItem.Text = "Get Definitions From";
            // 
            // tsmiGetDefinitionsFromCambridge
            // 
            this.tsmiGetDefinitionsFromCambridge.Name = "tsmiGetDefinitionsFromCambridge";
            this.tsmiGetDefinitionsFromCambridge.Size = new System.Drawing.Size(133, 22);
            this.tsmiGetDefinitionsFromCambridge.Text = "Cambridge";
            this.tsmiGetDefinitionsFromCambridge.Click += new System.EventHandler(this.tsmiGetDefinitionsFromCambridge_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportWordsForAnki,
            this.tsmiExportWordList});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // tsmiExportWordsForAnki
            // 
            this.tsmiExportWordsForAnki.Name = "tsmiExportWordsForAnki";
            this.tsmiExportWordsForAnki.Size = new System.Drawing.Size(155, 22);
            this.tsmiExportWordsForAnki.Text = "Words For Anki";
            this.tsmiExportWordsForAnki.Click += new System.EventHandler(this.tsmiExportWordsForAnki_Click);
            // 
            // tsmiExportWordList
            // 
            this.tsmiExportWordList.Name = "tsmiExportWordList";
            this.tsmiExportWordList.Size = new System.Drawing.Size(155, 22);
            this.tsmiExportWordList.Text = "Word List";
            this.tsmiExportWordList.Click += new System.EventHandler(this.tsmiExportWordList_Click);
            // 
            // tsmiImportToAnki
            // 
            this.tsmiImportToAnki.Name = "tsmiImportToAnki";
            this.tsmiImportToAnki.Size = new System.Drawing.Size(183, 22);
            this.tsmiImportToAnki.Text = "Import To Anki";
            this.tsmiImportToAnki.Click += new System.EventHandler(this.tsmiImportToAnki_Click);
            // 
            // tsmiClearImportStates
            // 
            this.tsmiClearImportStates.Name = "tsmiClearImportStates";
            this.tsmiClearImportStates.Size = new System.Drawing.Size(183, 22);
            this.tsmiClearImportStates.Text = "Clear Import States";
            this.tsmiClearImportStates.Click += new System.EventHandler(this.tsmiClearImportStates_Click);
            // 
            // tsmiDeleteSelected
            // 
            this.tsmiDeleteSelected.Name = "tsmiDeleteSelected";
            this.tsmiDeleteSelected.Size = new System.Drawing.Size(183, 22);
            this.tsmiDeleteSelected.Text = "Delete Selected";
            this.tsmiDeleteSelected.Click += new System.EventHandler(this.tsmiDeleteSelected_Click);
            // 
            // tsmiAddWord
            // 
            this.tsmiAddWord.Name = "tsmiAddWord";
            this.tsmiAddWord.Size = new System.Drawing.Size(183, 22);
            this.tsmiAddWord.Text = "Add Word";
            this.tsmiAddWord.Click += new System.EventHandler(this.tsmiAddWord_Click);
            // 
            // tsmiEditWord
            // 
            this.tsmiEditWord.Name = "tsmiEditWord";
            this.tsmiEditWord.Size = new System.Drawing.Size(183, 22);
            this.tsmiEditWord.Text = "Edit Word";
            this.tsmiEditWord.Click += new System.EventHandler(this.tsmiEditWord_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lvWords, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbWordOutput, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 438);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // lvWords
            // 
            this.lvWords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chWord,
            this.chImported,
            this.chData});
            this.lvWords.ContextMenuStrip = this.cmsMain;
            this.lvWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWords.FullRowSelect = true;
            this.lvWords.Location = new System.Drawing.Point(3, 3);
            this.lvWords.Name = "lvWords";
            this.lvWords.Size = new System.Drawing.Size(564, 322);
            this.lvWords.TabIndex = 6;
            this.lvWords.UseCompatibleStateImageBehavior = false;
            this.lvWords.View = System.Windows.Forms.View.Details;
            this.lvWords.SelectedIndexChanged += new System.EventHandler(this.lvWords_SelectedIndexChanged);
            this.lvWords.DoubleClick += new System.EventHandler(this.lvWords_DoubleClick);
            this.lvWords.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvWords_MouseDoubleClick);
            // 
            // chWord
            // 
            this.chWord.Text = "Word";
            this.chWord.Width = 122;
            // 
            // chImported
            // 
            this.chImported.Text = "Imported";
            this.chImported.Width = 122;
            // 
            // chData
            // 
            this.chData.Text = "Data";
            this.chData.Width = 420;
            // 
            // rtbWordOutput
            // 
            this.rtbWordOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbWordOutput.Location = new System.Drawing.Point(3, 331);
            this.rtbWordOutput.Name = "rtbWordOutput";
            this.rtbWordOutput.ReadOnly = true;
            this.rtbWordOutput.Size = new System.Drawing.Size(564, 104);
            this.rtbWordOutput.TabIndex = 4;
            this.rtbWordOutput.Text = "";
            // 
            // rbText
            // 
            this.rbText.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbText.Checked = true;
            this.rbText.Location = new System.Drawing.Point(438, 456);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(66, 21);
            this.rbText.TabIndex = 6;
            this.rbText.TabStop = true;
            this.rbText.Text = "Text";
            this.rbText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // rbHtml
            // 
            this.rbHtml.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbHtml.Location = new System.Drawing.Point(510, 456);
            this.rbHtml.Name = "rbHtml";
            this.rbHtml.Size = new System.Drawing.Size(72, 21);
            this.rbHtml.TabIndex = 7;
            this.rbHtml.Text = "HTML";
            this.rbHtml.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbHtml.UseVisualStyleBackColor = true;
            // 
            // lbDeckName
            // 
            this.lbDeckName.AutoSize = true;
            this.lbDeckName.Location = new System.Drawing.Point(12, 459);
            this.lbDeckName.Name = "lbDeckName";
            this.lbDeckName.Size = new System.Drawing.Size(73, 13);
            this.lbDeckName.TabIndex = 8;
            this.lbDeckName.Text = "Deck Name";
            // 
            // tbDeckName
            // 
            this.tbDeckName.Location = new System.Drawing.Point(91, 456);
            this.tbDeckName.Name = "tbDeckName";
            this.tbDeckName.Size = new System.Drawing.Size(341, 21);
            this.tbDeckName.TabIndex = 9;
            this.tbDeckName.TextChanged += new System.EventHandler(this.tbDeckName_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 489);
            this.Controls.Add(this.tbDeckName);
            this.Controls.Add(this.lbDeckName);
            this.Controls.Add(this.rbHtml);
            this.Controls.Add(this.rbText);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.cmsMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
        private System.Windows.Forms.ToolStripMenuItem getDefinitionsFromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddWord;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetDefinitionsFromCambridge;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditWord;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadWordList;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSelected;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WordListView lvWords;
        private System.Windows.Forms.ColumnHeader chWord;
        private System.Windows.Forms.ColumnHeader chData;
        private System.Windows.Forms.RichTextBox rtbWordOutput;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportWordsForAnki;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportWordList;
        private System.Windows.Forms.ColumnHeader chImported;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportToAnki;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearImportStates;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbHtml;
        private System.Windows.Forms.Label lbDeckName;
        private System.Windows.Forms.TextBox tbDeckName;
    }
}