namespace Diploma.UI
{
    partial class PortForm
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
            this.activationCodeTextBox = new System.Windows.Forms.TextBox();
            this.useForWatermarkingCheckBox = new System.Windows.Forms.CheckBox();
            this.watermarkingGroupBox = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.watermarkingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // portNameLabel
            // 
            this.portNameLabel.AutoSize = true;
            this.portNameLabel.Location = new System.Drawing.Point(12, 9);
            this.portNameLabel.Name = "portNameLabel";
            this.portNameLabel.Size = new System.Drawing.Size(57, 13);
            this.portNameLabel.TabIndex = 0;
            this.portNameLabel.Text = "Port Name";
            // 
            // activationCodeTextBox
            // 
            this.activationCodeTextBox.Location = new System.Drawing.Point(14, 37);
            this.activationCodeTextBox.Name = "activationCodeTextBox";
            this.activationCodeTextBox.Size = new System.Drawing.Size(138, 20);
            this.activationCodeTextBox.TabIndex = 1;
            this.activationCodeTextBox.TextChanged += new System.EventHandler(this.activationCodeTextBox_TextChanged);
            // 
            // useForWatermarkingCheckBox
            // 
            this.useForWatermarkingCheckBox.AutoSize = true;
            this.useForWatermarkingCheckBox.Location = new System.Drawing.Point(12, 48);
            this.useForWatermarkingCheckBox.Name = "useForWatermarkingCheckBox";
            this.useForWatermarkingCheckBox.Size = new System.Drawing.Size(126, 17);
            this.useForWatermarkingCheckBox.TabIndex = 6;
            this.useForWatermarkingCheckBox.Text = "Use for watermarking";
            this.useForWatermarkingCheckBox.UseVisualStyleBackColor = true;
            this.useForWatermarkingCheckBox.CheckedChanged += new System.EventHandler(this.useForWatermarkingCheckBox_CheckedChanged);
            // 
            // watermarkingGroupBox
            // 
            this.watermarkingGroupBox.Controls.Add(this.activationCodeTextBox);
            this.watermarkingGroupBox.Location = new System.Drawing.Point(1, 71);
            this.watermarkingGroupBox.Name = "watermarkingGroupBox";
            this.watermarkingGroupBox.Size = new System.Drawing.Size(303, 99);
            this.watermarkingGroupBox.TabIndex = 7;
            this.watermarkingGroupBox.TabStop = false;
            this.watermarkingGroupBox.Text = "Watermarking";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(219, 42);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // PortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 182);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.watermarkingGroupBox);
            this.Controls.Add(this.useForWatermarkingCheckBox);
            this.Controls.Add(this.portNameLabel);
            this.Name = "PortForm";
            this.Text = "PortForm";
            this.Load += new System.EventHandler(this.PortForm_Load);
            this.watermarkingGroupBox.ResumeLayout(false);
            this.watermarkingGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label portNameLabel;
        private System.Windows.Forms.TextBox activationCodeTextBox;
        private System.Windows.Forms.CheckBox useForWatermarkingCheckBox;
        private System.Windows.Forms.GroupBox watermarkingGroupBox;
        private System.Windows.Forms.Button okButton;
    }
}