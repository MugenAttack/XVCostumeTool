using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XVCostumeTool
{
    public partial class SettingForm : Form
    {
        string lang = "en";
        public SettingForm()
        {
            InitializeComponent();
        }

        private void rPL_CheckedChanged(object sender, EventArgs e)
        {
            lang = "pl";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnFMSG_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFolder = new FolderBrowserDialog();
            browseFolder.Description = "Find Msg Folder";
            if (browseFolder.ShowDialog() == DialogResult.Cancel)
                return;

            txtMSG.Text = browseFolder.SelectedPath;
        }

        private void btnFSystem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFolder = new FolderBrowserDialog();
            browseFolder.Description = "Find Msg Folder";
            if (browseFolder.ShowDialog() == DialogResult.Cancel)
                return;

            txtSystem.Text = browseFolder.SelectedPath;
        }

        private void rCA_CheckedChanged(object sender, EventArgs e)
        {
            lang = "ca";
        }

        private void rDE_CheckedChanged(object sender, EventArgs e)
        {
            lang = "de";
        }

        private void rEN_CheckedChanged(object sender, EventArgs e)
        {
            lang = "en";
        }

        private void rES_CheckedChanged(object sender, EventArgs e)
        {
            lang = "es";
        }

        private void rFR_CheckedChanged(object sender, EventArgs e)
        {
            lang = "fr";
        }

        private void rIT_CheckedChanged(object sender, EventArgs e)
        {
            lang = "it";
        }

        private void rPT_CheckedChanged(object sender, EventArgs e)
        {
            lang = "pt";
        }

        private void rRU_CheckedChanged(object sender, EventArgs e)
        {
            lang = "ru";
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(File.Create("CACTool_Settings.csv"));
            sw.WriteLine("MsgFolder," + txtMSG.Text);
            sw.WriteLine("SysFolder," + txtSystem.Text);
            sw.WriteLine("CharFolder," + txtChar.Text);
            sw.WriteLine("Language," + lang);
            sw.Close();
            this.Close();
        }

        private void btnFChar_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFolder = new FolderBrowserDialog();
            browseFolder.Description = "Find Chara Folder";
            if (browseFolder.ShowDialog() == DialogResult.Cancel)
                return;

            txtChar.Text = browseFolder.SelectedPath;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
