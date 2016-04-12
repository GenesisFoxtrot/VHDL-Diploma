using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Model;
using VHDLParser;

namespace Diploma.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Entity _entity;

        private void button1_Click(object sender, EventArgs e)
        {
            var fileService = new FileService();

            var vhdlLib = fileService.GetVHDL(@"D:\111\simprim_Vcomponents2.sty");
            var vhdl = fileService.GetVHDL(@"D:\111\222.vhd");
            var parser = new Parser();
            _entity = parser.ParEntities(vhdl).FirstOrDefault();
            WatermarkService service = new WatermarkService();
            var document = service.Watermark(vhdl, vhdlLib, _entity);
            _entity.Ports.Where(p=>p.PortType == PortTypes.In).ToList().ForEach(x=> InputListBox.Items.Add(x) );
            _entity.Ports.Where(p => p.PortType == PortTypes.Out).ToList().ForEach(x => OutputListBox.Items.Add(x));
            //File.WriteAllText(@"D:\111.vhdl", document.Document);
        }


        private void InputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (InputListBox.SelectedItem != null)
            {
                PortForm form = new PortForm(InputListBox.SelectedItem as Port);
                form.Port = InputListBox.SelectedItem as Port;
                form.ShowDialog();
            }
        }
    }
}
