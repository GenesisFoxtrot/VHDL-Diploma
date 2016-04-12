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
            this.InputListBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.EnableIputLabel = new System.Windows.Forms.Label();
            this.OutputListBox = new System.Windows.Forms.ListBox();
            this.EnableOutputLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InputListBox
            // 
            this.InputListBox.FormattingEnabled = true;
            this.InputListBox.Location = new System.Drawing.Point(20, 48);
            this.InputListBox.Name = "InputListBox";
            this.InputListBox.Size = new System.Drawing.Size(120, 95);
            this.InputListBox.TabIndex = 0;
            this.InputListBox.SelectedIndexChanged += new System.EventHandler(this.InputListBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(742, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EnableIputLabel
            // 
            this.EnableIputLabel.AutoSize = true;
            this.EnableIputLabel.Location = new System.Drawing.Point(17, 23);
            this.EnableIputLabel.Name = "EnableIputLabel";
            this.EnableIputLabel.Size = new System.Drawing.Size(86, 13);
            this.EnableIputLabel.TabIndex = 2;
            this.EnableIputLabel.Text = "Enable iput ports";
            // 
            // OutputListBox
            // 
            this.OutputListBox.FormattingEnabled = true;
            this.OutputListBox.Location = new System.Drawing.Point(163, 48);
            this.OutputListBox.Name = "OutputListBox";
            this.OutputListBox.Size = new System.Drawing.Size(120, 95);
            this.OutputListBox.TabIndex = 3;
            // 
            // EnableOutputLabel
            // 
            this.EnableOutputLabel.AutoSize = true;
            this.EnableOutputLabel.Location = new System.Drawing.Point(160, 23);
            this.EnableOutputLabel.Name = "EnableOutputLabel";
            this.EnableOutputLabel.Size = new System.Drawing.Size(99, 13);
            this.EnableOutputLabel.TabIndex = 4;
            this.EnableOutputLabel.Text = "Enable output ports";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(661, 432);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 467);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.EnableOutputLabel);
            this.Controls.Add(this.OutputListBox);
            this.Controls.Add(this.EnableIputLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InputListBox);
            this.Name = "Form1";
            this.Text = "FPGA Watermarking";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox InputListBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label EnableIputLabel;
        private System.Windows.Forms.ListBox OutputListBox;
        private System.Windows.Forms.Label EnableOutputLabel;
        private System.Windows.Forms.Button button2;
    }
}

