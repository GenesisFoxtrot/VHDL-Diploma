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
using Model.Options;

namespace Diploma.UI
{
    public partial class OutForm : Form
    {
        private readonly OutWatermarkSettings _settings;
        public OutForm(OutWatermarkSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void OutForm_Load(object sender, EventArgs e)
        {
            portNameLabel.Text = Resources.PortForm_PortForm_Load_Port_Name__ + _settings.Port.Name;
            useForSignatureCheckBox.Checked = _settings.IsUsed;
            signatureTextBox.Text = _settings.SignatureCode;
            useForSignatureCheckBox_CheckedChanged(null, null);
        }


        private string OldText = String.Empty;
        private bool IgnoteTextChange = false;
        private void signatureTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoteTextChange) return;

            const string codePattern = "^(|[0-1]+)$";
            if (!Regex.IsMatch(signatureTextBox.Text, codePattern) ||
                signatureTextBox.Text.Length > _settings.Port.Bits)
            {
                IgnoteTextChange = true;
                signatureTextBox.Text = OldText;
                signatureTextBox.SelectionStart = signatureTextBox.Text.Length;
                IgnoteTextChange = false;
            }
            else
            {
                OldText = signatureTextBox.Text;
                _settings.SignatureCode = signatureTextBox.Text;
            }
        }

        private void useForSignatureCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useForSignatureCheckBox.Checked)
            {
                signatureGroupBox.Enabled = true;
                _settings.IsUsed = true;
            }
            else
            {
                _settings.IsUsed = false;
                signatureGroupBox.Enabled = false;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
