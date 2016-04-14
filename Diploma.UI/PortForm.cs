using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diploma.UI.Properties;
using Model;
using Model.Options;
using Model.VHDLWords;

namespace Diploma.UI
{
    public partial class PortForm : Form
    {
        public PortForm(InWatermarkSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            
        }
        private InWatermarkSettings _settings { get; set; }
        private void PortForm_Load(object sender, EventArgs e)
        {
            portNameLabel.Text = Resources.PortForm_PortForm_Load_Port_Name__ +  _settings.Port.Name;
            useForWatermarkingCheckBox.Checked = _settings.IsUsed;
            activationCodeTextBox.Text = _settings.ActivaionCode;
            useForWatermarkingCheckBox_CheckedChanged(null, null);

        }

        private string OldText = String.Empty;
        private bool IgnoteTextChange = false;
        private void activationCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoteTextChange) return;

            const string codePattern = "^(|[0-1]+)$";
            if (!Regex.IsMatch(activationCodeTextBox.Text, codePattern) ||
                activationCodeTextBox.Text.Length > _settings.Port.Bits)
            {
                IgnoteTextChange = true;
                activationCodeTextBox.Text = OldText;
                activationCodeTextBox.SelectionStart = activationCodeTextBox.Text.Length;
                IgnoteTextChange = false;
            }
            else
            {
                OldText = activationCodeTextBox.Text;
                _settings.ActivaionCode = activationCodeTextBox.Text;
            }
        }

        private void useForWatermarkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useForWatermarkingCheckBox.Checked)
            {
                watermarkingGroupBox.Enabled = true;
                _settings.IsUsed = true;
            }
            else
            {
                _settings.IsUsed = false;
                watermarkingGroupBox.Enabled = false;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
