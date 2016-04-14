namespace Diploma.UI
{
    partial class WatermarkedTextForm
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
            this.watermarkedVHDLRichTextBox = new System.Windows.Forms.RichTextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // watermarkedVHDLRichTextBox
            // 
            this.watermarkedVHDLRichTextBox.Location = new System.Drawing.Point(5, 3);
            this.watermarkedVHDLRichTextBox.Name = "watermarkedVHDLRichTextBox";
            this.watermarkedVHDLRichTextBox.Size = new System.Drawing.Size(698, 475);
            this.watermarkedVHDLRichTextBox.TabIndex = 1;
            this.watermarkedVHDLRichTextBox.Text = "";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(628, 488);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // WatermarkedTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 517);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.watermarkedVHDLRichTextBox);
            this.Name = "WatermarkedTextForm";
            this.Text = "WatermarkedTextForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox watermarkedVHDLRichTextBox;
        private System.Windows.Forms.Button okButton;
    }
}