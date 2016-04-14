namespace Diploma.UI
{
    partial class OutForm
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
            this.portNameLabel = new System.Windows.Forms.Label();
            this.useForSignatureCheckBox = new System.Windows.Forms.CheckBox();
            this.signatureGroupBox = new System.Windows.Forms.GroupBox();
            this.signatureTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.signatureGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // portNameLabel
            // 
            this.portNameLabel.AutoSize = true;
            this.portNameLabel.Location = new System.Drawing.Point(13, 13);
            this.portNameLabel.Name = "portNameLabel";
            this.portNameLabel.Size = new System.Drawing.Size(63, 13);
            this.portNameLabel.TabIndex = 0;
            this.portNameLabel.Text = "Port Name: ";
            // 
            // useForSignatureCheckBox
            // 
            this.useForSignatureCheckBox.AutoSize = true;
            this.useForSignatureCheckBox.Location = new System.Drawing.Point(16, 76);
            this.useForSignatureCheckBox.Name = "useForSignatureCheckBox";
            this.useForSignatureCheckBox.Size = new System.Drawing.Size(139, 17);
            this.useForSignatureCheckBox.TabIndex = 1;
            this.useForSignatureCheckBox.Text = "Use for signature output";
            this.useForSignatureCheckBox.UseVisualStyleBackColor = true;
            this.useForSignatureCheckBox.CheckedChanged += new System.EventHandler(this.useForSignatureCheckBox_CheckedChanged);
            // 
            // signatureGroupBox
            // 
            this.signatureGroupBox.Controls.Add(this.signatureTextBox);
            this.signatureGroupBox.Location = new System.Drawing.Point(16, 100);
            this.signatureGroupBox.Name = "signatureGroupBox";
            this.signatureGroupBox.Size = new System.Drawing.Size(222, 143);
            this.signatureGroupBox.TabIndex = 2;
            this.signatureGroupBox.TabStop = false;
            this.signatureGroupBox.Text = "Signature";
            // 
            // signatureTextBox
            // 
            this.signatureTextBox.Location = new System.Drawing.Point(15, 46);
            this.signatureTextBox.Name = "signatureTextBox";
            this.signatureTextBox.Size = new System.Drawing.Size(185, 20);
            this.signatureTextBox.TabIndex = 0;
            this.signatureTextBox.TextChanged += new System.EventHandler(this.signatureTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(163, 249);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // OutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 279);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.signatureGroupBox);
            this.Controls.Add(this.useForSignatureCheckBox);
            this.Controls.Add(this.portNameLabel);
            this.Name = "OutForm";
            this.Text = "OutForm";
            this.Load += new System.EventHandler(this.OutForm_Load);
            this.signatureGroupBox.ResumeLayout(false);
            this.signatureGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label portNameLabel;
        private System.Windows.Forms.CheckBox useForSignatureCheckBox;
        private System.Windows.Forms.GroupBox signatureGroupBox;
        private System.Windows.Forms.TextBox signatureTextBox;
        private System.Windows.Forms.Button okButton;
    }
}