﻿using AnkiLookup.UI.Controls;

namespace AnkiLookup.UI.Forms
{
    partial class WordManagementForm
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
                //AnkiProvider.Dispose();
                //CambridgeProvider.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordManagementForm));
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiGetDefinitionsFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetDefinitionsFromCambridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetDefinitionsFromWordNet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetDefinitionsFromAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddWord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddWords = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSelectedWord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSelectedWord = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImports = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadWordList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportToAnki = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearImportStates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportWordsForAnki = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportWordList = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvWords = new AnkiLookup.UI.Controls.WordListView();
            this.chWord = new System.Windows.Forms.ColumnHeader();
            this.chImported = new System.Windows.Forms.ColumnHeader();
            this.chData = new System.Windows.Forms.ColumnHeader();
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
            this.cmsMain.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGetDefinitionsFrom,
            this.tsmiAddWord,
            this.tsmiAddWords,
            this.tsmiEditSelectedWord,
            this.tsmiDeleteSelectedWord,
            this.tsmiImports,
            this.tsmiExport});
            this.cmsMain.Name = "contextMenuStrip1";
            this.cmsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsMain.Size = new System.Drawing.Size(385, 340);
            // 
            // tsmiGetDefinitionsFrom
            // 
            this.tsmiGetDefinitionsFrom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGetDefinitionsFromCambridge,
            this.tsmiGetDefinitionsFromWordNet,
            this.tsmiGetDefinitionsFromAll});
            this.tsmiGetDefinitionsFrom.Name = "tsmiGetDefinitionsFrom";
            this.tsmiGetDefinitionsFrom.Size = new System.Drawing.Size(384, 48);
            this.tsmiGetDefinitionsFrom.Text = "Get Definitions From";
            // 
            // tsmiGetDefinitionsFromCambridge
            // 
            this.tsmiGetDefinitionsFromCambridge.Name = "tsmiGetDefinitionsFromCambridge";
            this.tsmiGetDefinitionsFromCambridge.Size = new System.Drawing.Size(331, 54);
            this.tsmiGetDefinitionsFromCambridge.Text = "Cambridge";
            this.tsmiGetDefinitionsFromCambridge.Click += new System.EventHandler(this.tsmiGetDefinitionsFrom_Click);
            // 
            // tsmiGetDefinitionsFromWordNet
            // 
            this.tsmiGetDefinitionsFromWordNet.Name = "tsmiGetDefinitionsFromWordNet";
            this.tsmiGetDefinitionsFromWordNet.Size = new System.Drawing.Size(331, 54);
            this.tsmiGetDefinitionsFromWordNet.Text = "WordNet";
            // 
            // tsmiGetDefinitionsFromAll
            // 
            this.tsmiGetDefinitionsFromAll.Name = "tsmiGetDefinitionsFromAll";
            this.tsmiGetDefinitionsFromAll.Size = new System.Drawing.Size(331, 54);
            this.tsmiGetDefinitionsFromAll.Text = "All";
            this.tsmiGetDefinitionsFromAll.Click += new System.EventHandler(this.tsmiGetDefinitionsFrom_Click);
            // 
            // tsmiAddWord
            // 
            this.tsmiAddWord.Name = "tsmiAddWord";
            this.tsmiAddWord.Size = new System.Drawing.Size(384, 48);
            this.tsmiAddWord.Text = "Add Word";
            this.tsmiAddWord.Click += new System.EventHandler(this.tsmiAddWord_Click);
            // 
            // tsmiAddWords
            // 
            this.tsmiAddWords.Name = "tsmiAddWords";
            this.tsmiAddWords.Size = new System.Drawing.Size(384, 48);
            this.tsmiAddWords.Text = "Add Words";
            this.tsmiAddWords.Click += new System.EventHandler(this.tsmiAddWords_Click);
            // 
            // tsmiEditSelectedWord
            // 
            this.tsmiEditSelectedWord.Name = "tsmiEditSelectedWord";
            this.tsmiEditSelectedWord.Size = new System.Drawing.Size(384, 48);
            this.tsmiEditSelectedWord.Text = "Edit Selected Word";
            this.tsmiEditSelectedWord.Click += new System.EventHandler(this.tsmiEditSelectedWord_Click);
            // 
            // tsmiDeleteSelectedWord
            // 
            this.tsmiDeleteSelectedWord.Name = "tsmiDeleteSelectedWord";
            this.tsmiDeleteSelectedWord.Size = new System.Drawing.Size(384, 48);
            this.tsmiDeleteSelectedWord.Text = "Delete Selected Word";
            this.tsmiDeleteSelectedWord.Click += new System.EventHandler(this.tsmiDeleteSelectedWord_Click);
            // 
            // tsmiImports
            // 
            this.tsmiImports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoad,
            this.tsmiImportToAnki,
            this.tsmiClearImportStates});
            this.tsmiImports.Name = "tsmiImports";
            this.tsmiImports.Size = new System.Drawing.Size(384, 48);
            this.tsmiImports.Text = "Import";
            // 
            // tsmiLoad
            // 
            this.tsmiLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadWordList,
            this.tsmiLoadDataFile});
            this.tsmiLoad.Name = "tsmiLoad";
            this.tsmiLoad.Size = new System.Drawing.Size(436, 54);
            this.tsmiLoad.Text = "Load From";
            // 
            // tsmiLoadWordList
            // 
            this.tsmiLoadWordList.Name = "tsmiLoadWordList";
            this.tsmiLoadWordList.Size = new System.Drawing.Size(309, 54);
            this.tsmiLoadWordList.Text = "Word List";
            this.tsmiLoadWordList.Click += new System.EventHandler(this.tsmiLoadWordList_Click);
            // 
            // tsmiLoadDataFile
            // 
            this.tsmiLoadDataFile.Name = "tsmiLoadDataFile";
            this.tsmiLoadDataFile.Size = new System.Drawing.Size(309, 54);
            this.tsmiLoadDataFile.Text = "Data File";
            this.tsmiLoadDataFile.Click += new System.EventHandler(this.tsmiLoadDataFile_Click);
            // 
            // tsmiImportToAnki
            // 
            this.tsmiImportToAnki.Name = "tsmiImportToAnki";
            this.tsmiImportToAnki.Size = new System.Drawing.Size(436, 54);
            this.tsmiImportToAnki.Text = "Import To Anki";
            this.tsmiImportToAnki.Click += new System.EventHandler(this.tsmiImportToAnki_Click);
            // 
            // tsmiClearImportStates
            // 
            this.tsmiClearImportStates.Name = "tsmiClearImportStates";
            this.tsmiClearImportStates.Size = new System.Drawing.Size(436, 54);
            this.tsmiClearImportStates.Text = "Clear Import States";
            this.tsmiClearImportStates.Click += new System.EventHandler(this.tsmiClearImportStates_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportWordsForAnki,
            this.tsmiExportWordList});
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(384, 48);
            this.tsmiExport.Text = "Export";
            // 
            // tsmiExportWordsForAnki
            // 
            this.tsmiExportWordsForAnki.Name = "tsmiExportWordsForAnki";
            this.tsmiExportWordsForAnki.Size = new System.Drawing.Size(387, 54);
            this.tsmiExportWordsForAnki.Text = "Words For Anki";
            this.tsmiExportWordsForAnki.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmiExportWordList
            // 
            this.tsmiExportWordList.Name = "tsmiExportWordList";
            this.tsmiExportWordList.Size = new System.Drawing.Size(387, 54);
            this.tsmiExportWordList.Text = "Word List";
            this.tsmiExportWordList.Click += new System.EventHandler(this.tsmiExport_Click);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 725);
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
            this.lvWords.HideSelection = false;
            this.lvWords.Location = new System.Drawing.Point(3, 3);
            this.lvWords.Name = "lvWords";
            this.lvWords.Size = new System.Drawing.Size(774, 537);
            this.lvWords.TabIndex = 6;
            this.lvWords.UseCompatibleStateImageBehavior = false;
            this.lvWords.View = System.Windows.Forms.View.Details;
            this.lvWords.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvWords_ColumnClick);
            this.lvWords.SelectedIndexChanged += new System.EventHandler(this.lvWords_SelectedIndexChanged);
            this.lvWords.DoubleClick += new System.EventHandler(this.lvWords_DoubleClick);
            // 
            // chWord
            // 
            this.chWord.Text = "Word";
            this.chWord.Width = 140;
            // 
            // chImported
            // 
            this.chImported.Text = "Imported";
            this.chImported.Width = 122;
            // 
            // chData
            // 
            this.chData.Text = "Data";
            this.chData.Width = 460;
            // 
            // rtbWordOutput
            // 
            this.rtbWordOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbWordOutput.Location = new System.Drawing.Point(3, 546);
            this.rtbWordOutput.Name = "rtbWordOutput";
            this.rtbWordOutput.ReadOnly = true;
            this.rtbWordOutput.Size = new System.Drawing.Size(774, 176);
            this.rtbWordOutput.TabIndex = 4;
            this.rtbWordOutput.Text = "";
            // 
            // rbText
            // 
            this.rbText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbText.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbText.Checked = true;
            this.rbText.Location = new System.Drawing.Point(648, 743);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(66, 24);
            this.rbText.TabIndex = 6;
            this.rbText.TabStop = true;
            this.rbText.Text = "Text";
            this.rbText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // rbHtml
            // 
            this.rbHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbHtml.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbHtml.Location = new System.Drawing.Point(720, 743);
            this.rbHtml.Name = "rbHtml";
            this.rbHtml.Size = new System.Drawing.Size(72, 24);
            this.rbHtml.TabIndex = 7;
            this.rbHtml.Text = "HTML";
            this.rbHtml.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbHtml.UseVisualStyleBackColor = true;
            // 
            // lbDeckName
            // 
            this.lbDeckName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDeckName.AutoSize = true;
            this.lbDeckName.Location = new System.Drawing.Point(12, 746);
            this.lbDeckName.Name = "lbDeckName";
            this.lbDeckName.Size = new System.Drawing.Size(178, 34);
            this.lbDeckName.TabIndex = 8;
            this.lbDeckName.Text = "Deck Name";
            // 
            // tbDeckName
            // 
            this.tbDeckName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeckName.Location = new System.Drawing.Point(104, 743);
            this.tbDeckName.Name = "tbDeckName";
            this.tbDeckName.Size = new System.Drawing.Size(538, 41);
            this.tbDeckName.TabIndex = 9;
            this.tbDeckName.DoubleClick += new System.EventHandler(this.tbDeckName_DoubleClick);
            this.tbDeckName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDeckName_KeyDown);
            // 
            // WordManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 776);
            this.Controls.Add(this.tbDeckName);
            this.Controls.Add(this.lbDeckName);
            this.Controls.Add(this.rbHtml);
            this.Controls.Add(this.rbText);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WordManagementForm";
            this.Text = "WordManagementForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.WordManagementForm_Load);
            this.cmsMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetDefinitionsFrom;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddWord;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetDefinitionsFromCambridge;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSelectedWord;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadWordList;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadDataFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private WordListView lvWords;
        private System.Windows.Forms.ColumnHeader chWord;
        private System.Windows.Forms.ColumnHeader chData;
        private System.Windows.Forms.RichTextBox rtbWordOutput;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportWordsForAnki;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportWordList;
        private System.Windows.Forms.ColumnHeader chImported;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportToAnki;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearImportStates;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbHtml;
        private System.Windows.Forms.Label lbDeckName;
        private System.Windows.Forms.TextBox tbDeckName;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSelectedWord;
        private System.Windows.Forms.ToolStripMenuItem tsmiImports;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddWords;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetDefinitionsFromWordNet;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetDefinitionsFromAll;
    }
}