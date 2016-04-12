using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diploma.UI.Properties;
using Model;

namespace Diploma.UI
{
    public partial class PortForm : Form
    {
        public PortForm(Port port)
        {
            InitializeComponent();
            Port = port;
        }

        public Port Port { get; set; }
        private void PortForm_Load(object sender, EventArgs e)
        {
            portNameLabel.Text = Resources.PortForm_PortForm_Load_Port_Name__ +  Port.Name;
        }
    }
}
