namespace AnkiLookup.UI.Models
{
    partial class EditWordDefinitionForm
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
            this.tbWord = new System.Windows.Forms.TextBox();
            this.lbWord = new System.Windows.Forms.Label();
            this.rtbDefinition = new System.Windows.Forms.RichTextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.lbDefinition = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.lbLabel = new System.Windows.Forms.Label();
            this.cbLabel = new System.Windows.Forms.ComboBox();
            this.lbEntryIndex = new System.Windows.Forms.Label();
            this.nudEntryIndex = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudEntryIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // tbWord
            // 
            this.tbWord.Location = new System.Drawing.Point(92, 25);
            this.tbWord.Name = "tbWord";
            this.tbWord.Size = new System.Drawing.Size(203, 21);
            this.tbWord.TabIndex = 9;
            // 
            // lbWord
            // 
            this.lbWord.AutoSize = true;
            this.lbWord.Location = new System.Drawing.Point(89, 8);
            this.lbWord.Name = "lbWord";
            this.lbWord.Size = new System.Drawing.Size(36, 13);
            this.lbWord.TabIndex = 8;
            this.lbWord.Text = "Word";
            // 
            // rtbDefinition
            // 
            this.rtbDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDefinition.Location = new System.Drawing.Point(12, 74);
            this.rtbDefinition.Name = "rtbDefinition";
            this.rtbDefinition.Size = new System.Drawing.Size(568, 182);
            this.rtbDefinition.TabIndex = 7;
            this.rtbDefinition.Text = "";
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(479, 262);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(101, 23);
            this.bOK.TabIndex = 6;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // lbDefinition
            // 
            this.lbDefinition.AutoSize = true;
            this.lbDefinition.Location = new System.Drawing.Point(9, 58);
            this.lbDefinition.Name = "lbDefinition";
            this.lbDefinition.Size = new System.Drawing.Size(61, 13);
            this.lbDefinition.TabIndex = 5;
            this.lbDefinition.Text = "Definition";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bCancel.Location = new System.Drawing.Point(372, 262);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(101, 23);
            this.bCancel.TabIndex = 10;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // lbLabel
            // 
            this.lbLabel.AutoSize = true;
            this.lbLabel.Location = new System.Drawing.Point(298, 8);
            this.lbLabel.Name = "lbLabel";
            this.lbLabel.Size = new System.Drawing.Size(37, 13);
            this.lbLabel.TabIndex = 11;
            this.lbLabel.Text = "Label";
            // 
            // cbLabel
            // 
            this.cbLabel.FormattingEnabled = true;
            this.cbLabel.Items.AddRange(new object[] {
            "adjective",
            "adverb",
            "noun",
            "suffix",
            "verb"});
            this.cbLabel.Location = new System.Drawing.Point(301, 25);
            this.cbLabel.Name = "cbLabel";
            this.cbLabel.Size = new System.Drawing.Size(203, 21);
            this.cbLabel.TabIndex = 13;
            // 
            // lbEntryIndex
            // 
            this.lbEntryIndex.AutoSize = true;
            this.lbEntryIndex.Location = new System.Drawing.Point(9, 8);
            this.lbEntryIndex.Name = "lbEntryIndex";
            this.lbEntryIndex.Size = new System.Drawing.Size(74, 13);
            this.lbEntryIndex.TabIndex = 14;
            this.lbEntryIndex.Text = "Entry Index";
            // 
            // nudEntryIndex
            // 
            this.nudEntryIndex.Location = new System.Drawing.Point(12, 25);
            this.nudEntryIndex.Name = "nudEntryIndex";
            this.nudEntryIndex.Size = new System.Drawing.Size(71, 21);
            this.nudEntryIndex.TabIndex = 15;
            // 
            // EditWordDefinitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 291);
            this.Controls.Add(this.nudEntryIndex);
            this.Controls.Add(this.lbEntryIndex);
            this.Controls.Add(this.cbLabel);
            this.Controls.Add(this.lbLabel);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.tbWord);
            this.Controls.Add(this.lbWord);
            this.Controls.Add(this.rtbDefinition);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lbDefinition);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Name = "EditWordDefinitionForm";
            this.Text = "EditWordDefinitionForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudEntryIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbWord;
        private System.Windows.Forms.Label lbWord;
        private System.Windows.Forms.RichTextBox rtbDefinition;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lbDefinition;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lbLabel;
        private System.Windows.Forms.ComboBox cbLabel;
        private System.Windows.Forms.Label lbEntryIndex;
        private System.Windows.Forms.NumericUpDown nudEntryIndex;
    }
}