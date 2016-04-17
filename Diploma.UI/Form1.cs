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
using Model.Options;
using Model.VHDLWords;
using Model.VHDLWords.Signals;
using VHDLParser;
using VHDLParser.Entities;
using VHDLParser.Services;


namespace Diploma.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            infoGroupBox.Enabled = true;
            _watermarkOptions = new WatermarkOptions();
        }

        private string curentVHDLFile = @"D:\diploma\mil.vhd";
        private string vhdlCode = String.Empty;
        private string vhdlLib = String.Empty;
        private string projectFolder = @"D:\Diploma\";

        private WatermarkOptions _watermarkOptions;
        private VHDLDocument _document;

        private void button1_Click(object sender, EventArgs e)
        {
            if (curentVHDLFile != null)
            {
                inputListBox.Items.Clear();
                outputListBox.Items.Clear();

                var fileService = new FileService();

                vhdlLib = fileService.GetVHDL(@"D:\111\simprim_Vcomponents2.sty");
                vhdlCode = fileService.GetVHDL(curentVHDLFile);

                _document = new VHDLDocument(vhdlCode);
                var parser = new Parser();
                _document.Entity = parser.ParEntities(vhdlCode).FirstOrDefault();
                _document.Components = parser.ParseCompenets(vhdlLib);
                _document.Signals = parser.ParseSignals(vhdlCode);
                _document.Maps = parser.ParseMaps(vhdlCode);
                SearchService searchService = new SearchService(_document);
                _document.ConstValueGenerators = searchService.ConstValuesGenerators();
                _document.FreeLuts = searchService.FreeLuts();
                //TODO Null Checks
                if (_document.Entity == null)
                {
                    string message = "You did not enter a server name. Cancel this operation?";
                    string caption = "There is no entity parsed";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                }
                else
                {
                    infoGroupBox.Enabled = true;
                    _document.Entity.Ports.Where(p => p.PortType == PortTypes.In).ToList().ForEach(x => inputListBox.Items.Add(x));
                    _document.Entity.Ports.Where(p => p.PortType == PortTypes.Out).ToList().ForEach(x => outputListBox.Items.Add(x));
                    entityNameLabel.Text = "Entity Name: " + _document.Entity.Name;
                    freeBitsLabel.Text = "Free Bits for Watermarking: " + _document.FreeLuts.Count() + " bits";

                }
                //File.WriteAllText(@"D:\111.vhdl", document.Document);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (inputListBox.SelectedItem != null)
            {
                var port = inputListBox.SelectedItem as Port;
                var settings = _watermarkOptions.WatermarkSettings.FirstOrDefault(x => x.Port == port);
                if (settings == null)
                {
                    settings = new InWatermarkSettings(port);
                    _watermarkOptions.WatermarkSettings.Add(settings);
                }
                
                PortForm form = new PortForm(settings);
                form.ShowDialog();
            }
        }

        private void openVHDLFileButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"D:\111";
            openFileDialog1.Filter = "VHDL files (*.vhd)|*.vhd|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curentVHDLFile = openFileDialog1.FileName;
                    File.Copy(curentVHDLFile, projectFolder + Path.GetFileName(curentVHDLFile));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void watermarkButton_Click(object sender, EventArgs e)
        {
            if (_document != null)
            {
                WatermarkService service = new WatermarkService(_document);
                _document = service.Watermark(_watermarkOptions);
                WatermarkedTextForm form = new WatermarkedTextForm(_document);
                form.ShowDialog();
            }
            else
            {
                string message = "Entity required";
                string caption = "Entity required";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }


        }

        private void signaturePortsButton_Click(object sender, EventArgs e)
        {
            if (outputListBox.SelectedItem != null)
            {
                var port = outputListBox.SelectedItem as Port;
                var settings = _watermarkOptions.SignatureOutputSettings.FirstOrDefault(x => x.Port == port);
                if (settings == null)
                {
                    settings = new OutWatermarkSettings(port);
                    _watermarkOptions.SignatureOutputSettings.Add(settings);
                }

                OutForm form = new OutForm(settings);
                form.ShowDialog();
            }
        }

        private void addFolderButton_Click(object sender, EventArgs e)
        {
            if (projectBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var directory = projectBrowserDialog.SelectedPath;
                FileService.DirectoryCopy(directory, projectFolder + Path.GetFileName(directory) , true);
                
                //MessageBox.Show(directory);
            }
        }
    }
}
