using MyProg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace refreshMe
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            var config = new IniFile("config.ini");
            config.Write("interval", "50");
            OpenFileDialog selectFile = new OpenFileDialog();
            selectFile.Title = "Open file containing only text";
            selectFile.Filter = "Text file (*.txt)|*.txt|INI (*.ini)|*.ini"
                    + "|All Files (*.*)|*.*";
            if (selectFile.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = selectFile.FileName; ;
                config.Write("fileChosen", selectedFile);
            }       
            readerPnl.Text = File.ReadAllText(config.Read("fileChosen"));
            int interval = Convert.ToInt32(config.Read("interval"));
            var refreshTimer = new Timer { Interval = interval };
            refreshTimer.Tick += (o, args) =>
            {
                readerPnl.Text = File.ReadAllText(config.Read("fileChosen"));
            };
            refreshTimer.Start();
            //Thanks to icons8.com for the icon
        }
    }
}
