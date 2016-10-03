using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Msgfile;
namespace XVCostumeTool
{
    public partial class Form1 : Form
    {

        string msgFolder;
        string sysFolder;
        string CharFolder;
        string lang;
        string[] Folders = { "HUM", "HUF", "NMC", "FRI", "MAM", "MAF" };

        
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            

            if (!File.Exists("CACTool_Settings.csv"))
            {
                SettingForm SF = new SettingForm();
                SF.Show();
            }
            else
            {
                //load settings file
                using (StreamReader sr = new StreamReader(File.Open("CACTool_Settings.csv", FileMode.Open)))
                {

                    string line = sr.ReadLine();
                    string[] s = line.Split(",".ToCharArray());
                    msgFolder = s[1];
                    line = sr.ReadLine();
                    s = line.Split(",".ToCharArray());
                    sysFolder = s[1];
                    line = sr.ReadLine();
                    s = line.Split(",".ToCharArray());
                    CharFolder = s[1];
                    line = sr.ReadLine();
                    s = line.Split(",".ToCharArray());
                    lang = s[1];
                }

                //perform check in bcs files for available ids
                bool[] AvailableIDS = new bool[900];

                for (int i = 0; i < 900; i++)
                    AvailableIDS[i] = true;


                for (int i = 0; i < 6; i++)
                {
                    using (BinaryReader br = new BinaryReader(File.Open(CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs", FileMode.Open)))
                    {
                        br.BaseStream.Seek(72, SeekOrigin.Begin);
                        for (int j = 0; j < 900; j++)
                        {
                            int value = br.ReadInt32();
                            if (value != 0)
                                AvailableIDS[j] = false;
                        }
                    }
               }

                for (int i = 0; i < 900; i++)
                    if (AvailableIDS[i])
                        lstID.Items.Add(i);
                    

                    if (!Directory.Exists("CAC_Backups"))
                    {
                        Directory.CreateDirectory("CAC_Backups");
                        for (int i = 0; i < 6; i++)
                        {
                            File.Copy(CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs", "CAC_Backups/" + Folders[i] + ".bcs");
                            
                        }

                    }


                
            }

            

        }






        private int getRaceLock()
        {
            int rLock = 0;
            int[] val = { 1, 2, 4, 8, 16, 32, 64, 128 };
            for (int i = 0; i < 8; i++)
            {
                if (chlstRG.GetItemCheckState(i) == CheckState.Checked)
                    rLock += val[i];
            }

            return rLock;
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            
        }

        void injectBCS(string bcs)
        {
            int id = (int)lstID.SelectedItem;
            int tID = int.Parse(txtTID.Text);
            BinaryReader br = new BinaryReader(File.Open("CAC_Backups/" + bcs + ".bcs", FileMode.Open));
            BinaryWriter bw = new BinaryWriter(File.Open(CharFolder + "/" + bcs + "/" + bcs + ".bcs", FileMode.Open));

            //find address and length of section
            br.BaseStream.Seek(72 + (int.Parse(txtTID.Text) * 4), SeekOrigin.Begin);

            // get address where data starts
            int address = br.ReadInt32(); 

            //determine length by finding the difference of id using as template and next id in with a proper address.
            int address2;
            while(true) 
            {
                address2 = br.ReadInt32();
                if (address2 != 0)
                    break;
            }

            int size = address2 - address;

            //write data to end of file.
            bw.BaseStream.Seek(72 + (id * 4), SeekOrigin.Begin);
            bw.Write((int)bw.BaseStream.Length);

            br.BaseStream.Seek(address, SeekOrigin.Begin);
            bw.BaseStream.Seek(0, SeekOrigin.End);
            bw.Write(br.ReadBytes(size));
           

            br.BaseStream.Seek(address + 32, SeekOrigin.Begin);

            for (int i = 0; i < 10; i++)
            {
                
                address2 = br.ReadInt32();

                //apply id
                if (address2 != 0)
                {
                    bw.BaseStream.Seek(-size, SeekOrigin.End);
                    bw.BaseStream.Seek(address2, SeekOrigin.Current);
                    bw.Write((Int16)id);
                    bw.Write((Int16)id);


                    //adjust addresses
                    int rAddress, sSize;
                    if (i != 9)
                    {
                        rAddress = (int)br.BaseStream.Position;
                        int address3 = br.ReadInt32();
                        sSize = address3 - address2;
                    }
                    else
                    {
                        rAddress = (int)br.BaseStream.Position;
                        sSize = size - address2;
                    }



                    bw.BaseStream.Seek(-size, SeekOrigin.End);
                    int wStart = (int)bw.BaseStream.Position;

                    for (int j = 0; j < 6; j++)
                    {
                        bw.BaseStream.Seek(wStart, SeekOrigin.Begin);
                        bw.BaseStream.Seek(address2 + sSize - 24, SeekOrigin.Current);
                        br.BaseStream.Seek(address + address2 + sSize - 24, SeekOrigin.Begin);

                        br.BaseStream.Seek(j * 4, SeekOrigin.Current);
                        bw.BaseStream.Seek(j * 4, SeekOrigin.Current);
                        int wordPos = br.ReadInt32();

                        if (wordPos != 0)
                        {
                            
                            br.BaseStream.Seek(address + address2 + wordPos + 80, SeekOrigin.Begin);
                            List<byte> wordbytes = new List<byte>();
                            while (true)
                            {
                                wordbytes.Add(br.ReadByte());
                                if (wordbytes[wordbytes.Count - 1] == 0x00)
                                    break;
                            }

                            br.BaseStream.Seek(address + address2 + wordPos + 80, SeekOrigin.Begin);
                            string text = Encoding.ASCII.GetString(br.ReadBytes(wordbytes.Count));
                            text.Replace(tID.ToString("000"), id.ToString("000"));


                            int rAddress4 = (int)bw.BaseStream.Position; //write position
                            bw.BaseStream.Seek(0, SeekOrigin.End);
                            int writeAddress = (int)bw.BaseStream.Position;
                            bw.Write(Encoding.ASCII.GetBytes(text));
                            bw.BaseStream.Seek(rAddress4, SeekOrigin.Begin);
                            bw.Write(writeAddress - (wStart + address2 + 80));

                            //bw.Write((address + address2 + wordPos) - (wStart + address2) - 4);
                        }
                        else
                            bw.Write((int)0);


                    }

                    br.BaseStream.Seek(rAddress, SeekOrigin.Begin);
                }
            }

            br.Close();
            bw.Close();
        }

        private void btnWork_Click_1(object sender, EventArgs e)
        {
            
            //inject bcs
            if (chlstRG.GetItemCheckState(0) == CheckState.Checked || chlstRG.GetItemCheckState(2) == CheckState.Checked)
                injectBCS("HUM");

            if (chlstRG.GetItemCheckState(1) == CheckState.Checked || chlstRG.GetItemCheckState(3) == CheckState.Checked)
                injectBCS("HUF");

            if (chlstRG.GetItemCheckState(4) == CheckState.Checked)
                injectBCS("NMC");

            if (chlstRG.GetItemCheckState(5) == CheckState.Checked)
                injectBCS("FRI");

            if (chlstRG.GetItemCheckState(6) == CheckState.Checked)
                injectBCS("MAM");

            if (chlstRG.GetItemCheckState(7) == CheckState.Checked)
                injectBCS("MAF");

            //msg load for adding the info on the 
            if (txtName.Text != "")
            {
                int nID, dID;
                msg Names = msgStream.Load2(msgFolder + "/proper_noun_costume_name_" + lang + ".msg");
                msgData[] Expand = new msgData[Names.data.Length + 1];
                Array.Copy(Names.data, Expand, Names.data.Length);
                Expand[Expand.Length - 1].NameID = "wear_cmn_" + Names.data.Length.ToString("000");
                nID = Names.data.Length;
                Expand[Expand.Length - 1].ID = Names.data.Length;
                Expand[Expand.Length - 1].Lines = new string[] { txtName.Text };
                Names.data = Expand;
                msgStream.Save(Names, msgFolder + "/proper_noun_costume_name_" + lang + ".msg");

                msg Descs = msgStream.Load2(msgFolder + "/proper_noun_costume_info_" + lang + ".msg");
                Expand = new msgData[Descs.data.Length + 1];
                Array.Copy(Descs.data, Expand, Descs.data.Length);
                Expand[Expand.Length - 1].NameID = "wear_cmn_" + Descs.data.Length.ToString("000");
                dID = Descs.data.Length;
                Expand[Expand.Length - 1].ID = Descs.data.Length;
                Expand[Expand.Length - 1].Lines = new string[] { txtDesc.Text };
                Descs.data = Expand;
                msgStream.Save(Descs, msgFolder + "/proper_noun_costume_info_" + lang + ".msg");

                //scan idb files for next highest idb id
                string[] items = { "top", "bottom", "gloves", "shoes" };
                int idbID = 0;
                for (int i = 0; i < 4; i++)
                {
                    BinaryReader br = new BinaryReader(File.Open(sysFolder + "/item/costume_" + items[i] + "_item.idb", FileMode.Open));
                    br.BaseStream.Seek(-640, SeekOrigin.End);
                    int id = br.ReadInt16();
                    if (idbID < id)
                        idbID = id;
                    br.Close();
                }

                for (int i = 0; i < 4; i++)
                {
                    if (lstchkType.GetItemCheckState(0) == CheckState.Checked)
                    {
                        BinaryReader br = new BinaryReader(File.Open(sysFolder + "/item/costume_" + items[i] +"_item.idb", FileMode.Open));
                        br.BaseStream.Seek(-640, SeekOrigin.End);
                        byte[] idbData = br.ReadBytes(640);
                        br.Close();

                        FileInfo info = new FileInfo(sysFolder + "/item/costume_" + items[i] + "_item.idb");
                        int count = ((int)info.Length - 16) / 640;


                        BinaryWriter bw = new BinaryWriter(File.Open(sysFolder + "/item/costume_" + items[i] + "_item.idb", FileMode.Open));
                        bw.BaseStream.Seek(0, SeekOrigin.End);
                        bw.Write(idbData);
                        bw.BaseStream.Seek(-640, SeekOrigin.End);
                        bw.Write((Int16)(idbID + 1));
                        bw.BaseStream.Seek(2, SeekOrigin.Current);
                        
                        bw.Write((Int16)nID);
                        bw.Write((Int16)dID);
                        
                        bw.BaseStream.Seek(8, SeekOrigin.Current);
                        bw.Write(int.Parse(txtBuy.Text));
                        bw.Write(int.Parse(txtSell.Text));
                        bw.Write(getRaceLock());
                        bw.BaseStream.Seek(8, SeekOrigin.Current);
                        for (int j = 0; j < 25; j++)
                            bw.Write((float)1);

                        bw.BaseStream.Seek(476, SeekOrigin.Current);
                        bw.Write((int)lstID.SelectedItem);
                        bw.BaseStream.Seek(8, SeekOrigin.Begin);
                        bw.Write(count + 1);
                        bw.Close();
                    }

                }

                MessageBox.Show(txtName.Text + " has been added to the Game");
            }
            else
                MessageBox.Show("New BCS Data has been added");

        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
            }
        }

        private void humanMaleHUMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
        }

        private void humanFemaleHUFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 1;
            File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
        }

        private void namekianNMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 2;
            File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
        }

        private void friezaRaceFRIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 3;
            File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
        }

        private void majinMaleMAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 4;
            File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
        }

        private void majinFemaleMAFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 5;
            File.Copy("CAC_Backups/" + Folders[i] + ".bcs", CharFolder + "/" + Folders[i] + "/" + Folders[i] + ".bcs");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
