namespace AnkiLookup.UI.Dialogs
{
    partial class EditWordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditWordForm));
            this.lbInputWord = new System.Windows.Forms.Label();
            this.tbInputWord = new System.Windows.Forms.TextBox();
            this.cmsEntries = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.lvExamples = new System.Windows.Forms.ListView();
            this.chExampleNumber = new System.Windows.Forms.ColumnHeader();
            this.chExample = new System.Windows.Forms.ColumnHeader();
            this.cmsExamples = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddExample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditExample = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteExample = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.bLookUp = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvDefinitions = new System.Windows.Forms.ListView();
            this.chActualWord = new System.Windows.Forms.ColumnHeader();
            this.chLabel = new System.Windows.Forms.ColumnHeader();
            this.chExampleCount = new System.Windows.Forms.ColumnHeader();
            this.chDefinition = new System.Windows.Forms.ColumnHeader();
            this.bCopy = new System.Windows.Forms.Button();
            this.bPaste = new System.Windows.Forms.Button();
            this.bCancelOrDelete = new System.Windows.Forms.Button();
            this.pFoundState = new System.Windows.Forms.Panel();
            this.cmsEntries.SuspendLayout();
            this.cmsExamples.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbInputWord
            // 
            this.lbInputWord.AutoSize = true;
            this.lbInputWord.Location = new System.Drawing.Point(12, 10);
            this.lbInputWord.Name = "lbInputWord";
            this.lbInputWord.Size = new System.Drawing.Size(175, 34);
            this.lbInputWord.TabIndex = 0;
            this.lbInputWord.Text = "Input Word";
            // 
            // tbInputWord
            // 
            this.tbInputWord.Location = new System.Drawing.Point(15, 34);
            this.tbInputWord.Name = "tbInputWord";
            this.tbInputWord.Size = new System.Drawing.Size(723, 41);
            this.tbInputWord.TabIndex = 1;
            this.tbInputWord.TextChanged += new System.EventHandler(this.tbInputWord_TextChanged);
            this.tbInputWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInputWord_KeyDown);
            // 
            // cmsEntries
            // 
            this.cmsEntries.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.cmsEntries.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddDefinition,
            this.tsmiEditDefinition,
            this.tsmiDeleteDefinition});
            this.cmsEntries.Name = "contextMenuStrip1";
            this.cmsEntries.Size = new System.Drawing.Size(289, 148);
            // 
            // tsmiAddDefinition
            // 
            this.tsmiAddDefinition.Name = "tsmiAddDefinition";
            this.tsmiAddDefinition.Size = new System.Drawing.Size(288, 48);
            this.tsmiAddDefinition.Text = "Add Definition";
            this.tsmiAddDefinition.Click += new System.EventHandler(this.tsmiAddDefinition_Click);
            // 
            // tsmiEditDefinition
            // 
            this.tsmiEditDefinition.Name = "tsmiEditDefinition";
            this.tsmiEditDefinition.Size = new System.Drawing.Size(288, 48);
            this.tsmiEditDefinition.Text = "Edit Definition";
            this.tsmiEditDefinition.Click += new System.EventHandler(this.tsmiEditDefinition_Click);
            // 
            // tsmiDeleteDefinition
            // 
            this.tsmiDeleteDefinition.Name = "tsmiDeleteDefinition";
            this.tsmiDeleteDefinition.Size = new System.Drawing.Size(288, 48);
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
            this.lvExamples.HideSelection = false;
            this.lvExamples.Location = new System.Drawing.Point(3, 143);
            this.lvExamples.Name = "lvExamples";
            this.lvExamples.Size = new System.Drawing.Size(723, 136);
            this.lvExamples.TabIndex = 3;
            this.lvExamples.UseCompatibleStateImageBehavior = false;
            this.lvExamples.View = System.Windows.Forms.View.Details;
            this.lvExamples.SelectedIndexChanged += new System.EventHandler(this.lvExamples_SelectedIndexChanged);
            // 
            // chExampleNumber
            // 
            this.chExampleNumber.Text = "Example #";
            this.chExampleNumber.Width = 100;
            // 
            // chExample
            // 
            this.chExample.Text = "Example";
            this.chExample.Width = 484;
            // 
            // cmsExamples
            // 
            this.cmsExamples.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.cmsExamples.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddExample,
            this.tsmiEditExample,
            this.tsmiDeleteExample});
            this.cmsExamples.Name = "contextMenuStrip1";
            this.cmsExamples.Size = new System.Drawing.Size(271, 148);
            // 
            // tsmiAddExample
            // 
            this.tsmiAddExample.Name = "tsmiAddExample";
            this.tsmiAddExample.Size = new System.Drawing.Size(270, 48);
            this.tsmiAddExample.Text = "Add Example";
            this.tsmiAddExample.Click += new System.EventHandler(this.tsmiAddExample_Click);
            // 
            // tsmiEditExample
            // 
            this.tsmiEditExample.Name = "tsmiEditExample";
            this.tsmiEditExample.Size = new System.Drawing.Size(270, 48);
            this.tsmiEditExample.Text = "Edit Example";
            this.tsmiEditExample.Click += new System.EventHandler(this.tsmiEditExample_Click);
            // 
            // tsmiDeleteExample
            // 
            this.tsmiDeleteExample.Name = "tsmiDeleteExample";
            this.tsmiDeleteExample.Size = new System.Drawing.Size(270, 48);
            this.tsmiDeleteExample.Text = "Delete";
            this.tsmiDeleteExample.Click += new System.EventHandler(this.tsmiDeleteExample_Click);
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(3, 285);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(723, 131);
            this.rtbOutput.TabIndex = 4;
            this.rtbOutput.Text = "";
            // 
            // bLookUp
            // 
            this.bLookUp.Location = new System.Drawing.Point(420, 5);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 61);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.87978F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.51366F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(729, 419);
            this.tableLayoutPanel1.TabIndex = 8;
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
            this.lvDefinitions.HideSelection = false;
            this.lvDefinitions.Location = new System.Drawing.Point(3, 3);
            this.lvDefinitions.Name = "lvDefinitions";
            this.lvDefinitions.Size = new System.Drawing.Size(723, 134);
            this.lvDefinitions.TabIndex = 3;
            this.lvDefinitions.UseCompatibleStateImageBehavior = false;
            this.lvDefinitions.View = System.Windows.Forms.View.Details;
            this.lvDefinitions.SelectedIndexChanged += new System.EventHandler(this.lvEntries_SelectedIndexChanged);
            this.lvDefinitions.DoubleClick += new System.EventHandler(this.lvDefinitions_DoubleClick);
            // 
            // chActualWord
            // 
            this.chActualWord.Text = "Actual Word";
            this.chActualWord.Width = 140;
            // 
            // chLabel
            // 
            this.chLabel.Text = "Label";
            this.chLabel.Width = 75;
            // 
            // chExampleCount
            // 
            this.chExampleCount.Text = "Example Count";
            this.chExampleCount.Width = 140;
            // 
            // chDefinition
            // 
            this.chDefinition.Text = "Definition";
            this.chDefinition.Width = 337;
            // 
            // bCopy
            // 
            this.bCopy.Location = new System.Drawing.Point(501, 5);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(75, 23);
            this.bCopy.TabIndex = 9;
            this.bCopy.Text = "Copy";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // bPaste
            // 
            this.bPaste.Location = new System.Drawing.Point(582, 5);
            this.bPaste.Name = "bPaste";
            this.bPaste.Size = new System.Drawing.Size(75, 23);
            this.bPaste.TabIndex = 10;
            this.bPaste.Text = "Paste";
            this.bPaste.UseVisualStyleBackColor = true;
            this.bPaste.Click += new System.EventHandler(this.bPaste_Click);
            // 
            // bCancelOrDelete
            // 
            this.bCancelOrDelete.Location = new System.Drawing.Point(663, 5);
            this.bCancelOrDelete.Name = "bCancelOrDelete";
            this.bCancelOrDelete.Size = new System.Drawing.Size(75, 23);
            this.bCancelOrDelete.TabIndex = 11;
            this.bCancelOrDelete.Text = "Delete";
            this.bCancelOrDelete.UseVisualStyleBackColor = true;
            this.bCancelOrDelete.Click += new System.EventHandler(this.bCancelOrDelete_Click);
            // 
            // pFoundState
            // 
            this.pFoundState.BackColor = System.Drawing.Color.Blue;
            this.pFoundState.Location = new System.Drawing.Point(106, 14);
            this.pFoundState.Name = "pFoundState";
            this.pFoundState.Size = new System.Drawing.Size(184, 10);
            this.pFoundState.TabIndex = 12;
            this.pFoundState.Visible = false;
            // 
            // EditWordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 492);
            this.Controls.Add(this.pFoundState);
            this.Controls.Add(this.bCancelOrDelete);
            this.Controls.Add(this.bPaste);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.bLookUp);
            this.Controls.Add(this.tbInputWord);
            this.Controls.Add(this.lbInputWord);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditWordForm";
            this.Text = "{0} Word - \"{1}\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditWordForm_FormClosing);
            this.Load += new System.EventHandler(this.EditWordForm_Load);
            this.cmsEntries.ResumeLayout(false);
            this.cmsExamples.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInputWord;
        private System.Windows.Forms.TextBox tbInputWord;
        private System.Windows.Forms.ListView lvExamples;
        private System.Windows.Forms.ColumnHeader chExampleNumber;
        private System.Windows.Forms.ColumnHeader chExample;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.ContextMenuStrip cmsEntries;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDefinition;
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
        private System.Windows.Forms.Panel pFoundState;
        private System.Windows.Forms.ListView lvDefinitions;
        private System.Windows.Forms.ColumnHeader chActualWord;
        private System.Windows.Forms.ColumnHeader chLabel;
        private System.Windows.Forms.ColumnHeader chExampleCount;
        private System.Windows.Forms.ColumnHeader chDefinition;
    }
}