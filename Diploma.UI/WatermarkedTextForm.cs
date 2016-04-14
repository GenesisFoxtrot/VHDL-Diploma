using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VHDLParser;
using VHDLParser.Entities;

namespace Diploma.UI
{
    public partial class WatermarkedTextForm : Form
    {
        public WatermarkedTextForm(VHDLDocument document)
        {
            InitializeComponent();
            watermarkedVHDLRichTextBox.Text = document.Document;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
