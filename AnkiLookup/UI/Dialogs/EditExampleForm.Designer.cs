namespace AnkiLookup.UI.Dialogs
{
    partial class EditExampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditExampleForm));
            this.rtbDefinition = new System.Windows.Forms.RichTextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.lbDefinition = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbDefinition
            // 
            this.rtbDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDefinition.Location = new System.Drawing.Point(17, 25);
            this.rtbDefinition.Name = "rtbDefinition";
            this.rtbDefinition.Size = new System.Drawing.Size(563, 197);
            this.rtbDefinition.TabIndex = 10;
            this.rtbDefinition.Text = "";
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(462, 228);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(118, 23);
            this.bOK.TabIndex = 9;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // lbDefinition
            // 
            this.lbDefinition.AutoSize = true;
            this.lbDefinition.Location = new System.Drawing.Point(14, 9);
            this.lbDefinition.Name = "lbDefinition";
            this.lbDefinition.Size = new System.Drawing.Size(56, 13);
            this.lbDefinition.TabIndex = 8;
            this.lbDefinition.Text = "Example";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(338, 228);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(118, 23);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // EditExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 263);
            this.Controls.Add(this.rtbDefinition);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lbDefinition);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditExampleForm";
            this.Text = "EditExampleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDefinition;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lbDefinition;
        private System.Windows.Forms.Button bCancel;
    }
}