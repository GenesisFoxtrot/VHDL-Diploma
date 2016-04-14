namespace Diploma.UI
{
    partial class Form1
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
            this.inputListBox = new System.Windows.Forms.ListBox();
            this.parseVHDLButton = new System.Windows.Forms.Button();
            this.enableIputLabel = new System.Windows.Forms.Label();
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.enableOutputLabel = new System.Windows.Forms.Label();
            this.openPortDialogButton = new System.Windows.Forms.Button();
            this.openVHDLFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openVHDLFileButton = new System.Windows.Forms.Button();
            this.watermarkButton = new System.Windows.Forms.Button();
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.entityNameLabel = new System.Windows.Forms.Label();
            this.freeBitsLabel = new System.Windows.Forms.Label();
            this.signaturePortsButton = new System.Windows.Forms.Button();
            this.infoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputListBox
            // 
            this.inputListBox.FormattingEnabled = true;
            this.inputListBox.Location = new System.Drawing.Point(20, 48);
            this.inputListBox.Name = "inputListBox";
            this.inputListBox.Size = new System.Drawing.Size(120, 95);
            this.inputListBox.TabIndex = 0;
            // 
            // parseVHDLButton
            // 
            this.parseVHDLButton.Location = new System.Drawing.Point(101, 424);
            this.parseVHDLButton.Name = "parseVHDLButton";
            this.parseVHDLButton.Size = new System.Drawing.Size(75, 23);
            this.parseVHDLButton.TabIndex = 1;
            this.parseVHDLButton.Text = "Parse";
            this.parseVHDLButton.UseVisualStyleBackColor = true;
            this.parseVHDLButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // enableIputLabel
            // 
            this.enableIputLabel.AutoSize = true;
            this.enableIputLabel.Location = new System.Drawing.Point(17, 23);
            this.enableIputLabel.Name = "enableIputLabel";
            this.enableIputLabel.Size = new System.Drawing.Size(86, 13);
            this.enableIputLabel.TabIndex = 2;
            this.enableIputLabel.Text = "Enable iput ports";
            // 
            // outputListBox
            // 
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.Location = new System.Drawing.Point(163, 48);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(120, 95);
            this.outputListBox.TabIndex = 3;
            // 
            // enableOutputLabel
            // 
            this.enableOutputLabel.AutoSize = true;
            this.enableOutputLabel.Location = new System.Drawing.Point(160, 23);
            this.enableOutputLabel.Name = "enableOutputLabel";
            this.enableOutputLabel.Size = new System.Drawing.Size(99, 13);
            this.enableOutputLabel.TabIndex = 4;
            this.enableOutputLabel.Text = "Enable output ports";
            // 
            // openPortDialogButton
            // 
            this.openPortDialogButton.Location = new System.Drawing.Point(65, 149);
            this.openPortDialogButton.Name = "openPortDialogButton";
            this.openPortDialogButton.Size = new System.Drawing.Size(75, 23);
            this.openPortDialogButton.TabIndex = 5;
            this.openPortDialogButton.Text = "Set";
            this.openPortDialogButton.UseVisualStyleBackColor = true;
            this.openPortDialogButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // openVHDLFileDialog
            // 
            this.openVHDLFileDialog.FileName = "file";
            // 
            // openVHDLFileButton
            // 
            this.openVHDLFileButton.Location = new System.Drawing.Point(20, 424);
            this.openVHDLFileButton.Name = "openVHDLFileButton";
            this.openVHDLFileButton.Size = new System.Drawing.Size(75, 23);
            this.openVHDLFileButton.TabIndex = 6;
            this.openVHDLFileButton.Text = "OpenFile";
            this.openVHDLFileButton.UseVisualStyleBackColor = true;
            this.openVHDLFileButton.Click += new System.EventHandler(this.openVHDLFileButton_Click);
            // 
            // watermarkButton
            // 
            this.watermarkButton.Location = new System.Drawing.Point(184, 424);
            this.watermarkButton.Name = "watermarkButton";
            this.watermarkButton.Size = new System.Drawing.Size(75, 23);
            this.watermarkButton.TabIndex = 7;
            this.watermarkButton.Text = "Watermark";
            this.watermarkButton.UseVisualStyleBackColor = true;
            this.watermarkButton.Click += new System.EventHandler(this.watermarkButton_Click);
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.freeBitsLabel);
            this.infoGroupBox.Controls.Add(this.entityNameLabel);
            this.infoGroupBox.Location = new System.Drawing.Point(20, 194);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new System.Drawing.Size(263, 207);
            this.infoGroupBox.TabIndex = 8;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Inforamtion";
            // 
            // entityNameLabel
            // 
            this.entityNameLabel.AutoSize = true;
            this.entityNameLabel.Location = new System.Drawing.Point(11, 29);
            this.entityNameLabel.Name = "entityNameLabel";
            this.entityNameLabel.Size = new System.Drawing.Size(64, 13);
            this.entityNameLabel.TabIndex = 0;
            this.entityNameLabel.Text = "Entity Name";
            // 
            // freeBitsLabel
            // 
            this.freeBitsLabel.AutoSize = true;
            this.freeBitsLabel.Location = new System.Drawing.Point(11, 56);
            this.freeBitsLabel.Name = "freeBitsLabel";
            this.freeBitsLabel.Size = new System.Drawing.Size(132, 13);
            this.freeBitsLabel.TabIndex = 1;
            this.freeBitsLabel.Text = "Free Bits for Watermarking";
            // 
            // signaturePortsButton
            // 
            this.signaturePortsButton.Location = new System.Drawing.Point(208, 149);
            this.signaturePortsButton.Name = "signaturePortsButton";
            this.signaturePortsButton.Size = new System.Drawing.Size(75, 23);
            this.signaturePortsButton.TabIndex = 9;
            this.signaturePortsButton.Text = "Set";
            this.signaturePortsButton.UseVisualStyleBackColor = true;
            this.signaturePortsButton.Click += new System.EventHandler(this.signaturePortsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 467);
            this.Controls.Add(this.signaturePortsButton);
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.watermarkButton);
            this.Controls.Add(this.openVHDLFileButton);
            this.Controls.Add(this.openPortDialogButton);
            this.Controls.Add(this.enableOutputLabel);
            this.Controls.Add(this.outputListBox);
            this.Controls.Add(this.enableIputLabel);
            this.Controls.Add(this.parseVHDLButton);
            this.Controls.Add(this.inputListBox);
            this.Name = "Form1";
            this.Text = "FPGA Watermarking";
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox inputListBox;
        private System.Windows.Forms.Button parseVHDLButton;
        private System.Windows.Forms.Label enableIputLabel;
        private System.Windows.Forms.ListBox outputListBox;
        private System.Windows.Forms.Label enableOutputLabel;
        private System.Windows.Forms.Button openPortDialogButton;
        private System.Windows.Forms.OpenFileDialog openVHDLFileDialog;
        private System.Windows.Forms.Button openVHDLFileButton;
        private System.Windows.Forms.Button watermarkButton;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.Label entityNameLabel;
        private System.Windows.Forms.Label freeBitsLabel;
        private System.Windows.Forms.Button signaturePortsButton;
    }
}

