namespace AnkiLookup.UI.Models
{
    partial class EditWordInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditWordInfoForm));
            this.lbInputWord = new System.Windows.Forms.Label();
            this.tbInputWord = new System.Windows.Forms.TextBox();
            this.lvDefinitions = new System.Windows.Forms.ListView();
            this.chActualWord = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExampleCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDefinition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsEntries = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.lvExamples = new System.Windows.Forms.ListView();
            this.chExampleNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExample = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsExamples = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddExample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditExample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteExample = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.bLookUp = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bCopy = new System.Windows.Forms.Button();
            this.bPaste = new System.Windows.Forms.Button();
            this.bCancelOrDelete = new System.Windows.Forms.Button();
            this.cmsEntries.SuspendLayout();
            this.cmsExamples.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbInputWord
            // 
            this.lbInputWord.AutoSize = true;
            this.lbInputWord.Location = new System.Drawing.Point(9, 10);
            this.lbInputWord.Name = "lbInputWord";
            this.lbInputWord.Size = new System.Drawing.Size(70, 13);
            this.lbInputWord.TabIndex = 0;
            this.lbInputWord.Text = "Input Word";
            // 
            // tbInputWord
            // 
            this.tbInputWord.Location = new System.Drawing.Point(12, 26);
            this.tbInputWord.Name = "tbInputWord";
            this.tbInputWord.Size = new System.Drawing.Size(175, 21);
            this.tbInputWord.TabIndex = 1;
            this.tbInputWord.TextChanged += new System.EventHandler(this.tbInputWord_TextChanged);
            this.tbInputWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInputWord_KeyDown);
            // 
            // lvDefinitions
            // 
            this.lvDefinitions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActualWord,
            this.chLabel,
            this.chExampleCount,
            this.chDefinition});
            this.lvDefinitions.ContextMenuStrip = this.cmsEntries;
            this.lvDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDefinitions.FullRowSelect = true;
            this.lvDefinitions.Location = new System.Drawing.Point(3, 3);
            this.lvDefinitions.Name = "lvDefinitions";
            this.lvDefinitions.Size = new System.Drawing.Size(638, 177);
            this.lvDefinitions.TabIndex = 3;
            this.lvDefinitions.UseCompatibleStateImageBehavior = false;
            this.lvDefinitions.View = System.Windows.Forms.View.Details;
            this.lvDefinitions.SelectedIndexChanged += new System.EventHandler(this.lvEntries_SelectedIndexChanged);
            // 
            // chActualWord
            // 
            this.chActualWord.Text = "Actual Word";
            this.chActualWord.Width = 95;
            // 
            // chLabel
            // 
            this.chLabel.Text = "Label";
            this.chLabel.Width = 75;
            // 
            // chExampleCount
            // 
            this.chExampleCount.Text = "Example Count";
            this.chExampleCount.Width = 100;
            // 
            // chDefinition
            // 
            this.chDefinition.Text = "Definition";
            this.chDefinition.Width = 386;
            // 
            // cmsEntries
            // 
            this.cmsEntries.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddDefinition,
            this.tsmiEditDefinition,
            this.tsmiDeleteDefinition});
            this.cmsEntries.Name = "contextMenuStrip1";
            this.cmsEntries.Size = new System.Drawing.Size(152, 70);
            // 
            // tsmiAddDefinition
            // 
            this.tsmiAddDefinition.Name = "tsmiAddDefinition";
            this.tsmiAddDefinition.Size = new System.Drawing.Size(151, 22);
            this.tsmiAddDefinition.Text = "Add Definition";
            this.tsmiAddDefinition.Click += new System.EventHandler(this.tsmiAddDefinition_Click);
            // 
            // tsmiEditDefinition
            // 
            this.tsmiEditDefinition.Name = "tsmiEditDefinition";
            this.tsmiEditDefinition.Size = new System.Drawing.Size(151, 22);
            this.tsmiEditDefinition.Text = "Edit Definition";
            this.tsmiEditDefinition.Click += new System.EventHandler(this.tsmiEditDefinition_Click);
            // 
            // tsmiDeleteDefinition
            // 
            this.tsmiDeleteDefinition.Name = "tsmiDeleteDefinition";
            this.tsmiDeleteDefinition.Size = new System.Drawing.Size(151, 22);
            this.tsmiDeleteDefinition.Text = "Delete";
            this.tsmiDeleteDefinition.Click += new System.EventHandler(this.tsmiDeleteDefinition_Click);
            // 
            // lvExamples
            // 
            this.lvExamples.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chExampleNumber,
            this.chExample});
            this.lvExamples.ContextMenuStrip = this.cmsExamples;
            this.lvExamples.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvExamples.FullRowSelect = true;
            this.lvExamples.Location = new System.Drawing.Point(3, 186);
            this.lvExamples.Name = "lvExamples";
            this.lvExamples.Size = new System.Drawing.Size(638, 177);
            this.lvExamples.TabIndex = 3;
            this.lvExamples.UseCompatibleStateImageBehavior = false;
            this.lvExamples.View = System.Windows.Forms.View.Details;
            this.lvExamples.SelectedIndexChanged += new System.EventHandler(this.lvExamples_SelectedIndexChanged);
            // 
            // chExampleNumber
            // 
            this.chExampleNumber.Text = "Example #";
            this.chExampleNumber.Width = 75;
            // 
            // chExample
            // 
            this.chExample.Text = "Example";
            this.chExample.Width = 484;
            // 
            // cmsExamples
            // 
            this.cmsExamples.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddExample,
            this.tsmiEditExample,
            this.tsmiDeleteExample});
            this.cmsExamples.Name = "contextMenuStrip1";
            this.cmsExamples.Size = new System.Drawing.Size(144, 70);
            // 
            // tsmiAddExample
            // 
            this.tsmiAddExample.Name = "tsmiAddExample";
            this.tsmiAddExample.Size = new System.Drawing.Size(143, 22);
            this.tsmiAddExample.Text = "Add Example";
            this.tsmiAddExample.Click += new System.EventHandler(this.tsmiAddExample_Click);
            // 
            // tsmiEditExample
            // 
            this.tsmiEditExample.Name = "tsmiEditExample";
            this.tsmiEditExample.Size = new System.Drawing.Size(143, 22);
            this.tsmiEditExample.Text = "Edit Example";
            this.tsmiEditExample.Click += new System.EventHandler(this.tsmiEditExample_Click);
            // 
            // tsmiDeleteExample
            // 
            this.tsmiDeleteExample.Name = "tsmiDeleteExample";
            this.tsmiDeleteExample.Size = new System.Drawing.Size(143, 22);
            this.tsmiDeleteExample.Text = "Delete";
            this.tsmiDeleteExample.Click += new System.EventHandler(this.tsmiDeleteExample_Click);
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(3, 369);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(638, 86);
            this.rtbOutput.TabIndex = 4;
            this.rtbOutput.Text = "";
            // 
            // bLookUp
            // 
            this.bLookUp.Location = new System.Drawing.Point(194, 26);
            this.bLookUp.Name = "bLookUp";
            this.bLookUp.Size = new System.Drawing.Size(75, 23);
            this.bLookUp.TabIndex = 7;
            this.bLookUp.Text = "Look Up";
            this.bLookUp.UseVisualStyleBackColor = true;
            this.bLookUp.Click += new System.EventHandler(this.bLookUp_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lvExamples, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lvDefinitions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbOutput, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 53);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 458);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // bCopy
            // 
            this.bCopy.Location = new System.Drawing.Point(276, 26);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(75, 23);
            this.bCopy.TabIndex = 9;
            this.bCopy.Text = "Copy";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // bPaste
            // 
            this.bPaste.Location = new System.Drawing.Point(358, 26);
            this.bPaste.Name = "bPaste";
            this.bPaste.Size = new System.Drawing.Size(75, 23);
            this.bPaste.TabIndex = 10;
            this.bPaste.Text = "Paste";
            this.bPaste.UseVisualStyleBackColor = true;
            this.bPaste.Click += new System.EventHandler(this.bPaste_Click);
            // 
            // bCancelOrDelete
            // 
            this.bCancelOrDelete.Location = new System.Drawing.Point(439, 26);
            this.bCancelOrDelete.Name = "bCancelOrDelete";
            this.bCancelOrDelete.Size = new System.Drawing.Size(75, 23);
            this.bCancelOrDelete.TabIndex = 11;
            this.bCancelOrDelete.Text = "Delete";
            this.bCancelOrDelete.UseVisualStyleBackColor = true;
            this.bCancelOrDelete.Click += new System.EventHandler(this.bCancelOrDelete_Click);
            // 
            // EditWordInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 523);
            this.Controls.Add(this.bCancelOrDelete);
            this.Controls.Add(this.bPaste);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.bLookUp);
            this.Controls.Add(this.tbInputWord);
            this.Controls.Add(this.lbInputWord);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditWordInfoForm";
            this.Text = "{0} Word - \"{1}\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditWordInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.EditWordInfoForm_Load);
            this.cmsEntries.ResumeLayout(false);
            this.cmsExamples.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInputWord;
        private System.Windows.Forms.TextBox tbInputWord;
        private System.Windows.Forms.ListView lvDefinitions;
        private System.Windows.Forms.ColumnHeader chLabel;
        private System.Windows.Forms.ColumnHeader chDefinition;
        private System.Windows.Forms.ListView lvExamples;
        private System.Windows.Forms.ColumnHeader chExampleNumber;
        private System.Windows.Forms.ColumnHeader chExample;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.ColumnHeader chActualWord;
        private System.Windows.Forms.ContextMenuStrip cmsEntries;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDefinition;
        private System.Windows.Forms.ColumnHeader chExampleCount;
        private System.Windows.Forms.ContextMenuStrip cmsExamples;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteExample;
        private System.Windows.Forms.Button bLookUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.Button bPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditDefinition;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddExample;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditExample;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDefinition;
        private System.Windows.Forms.Button bCancelOrDelete;
    }
}