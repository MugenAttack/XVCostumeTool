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
            BinaryWriter bw = new BinaryWriter(File.Open(CharFolder + "/" + bcs + "/" + bcs + ".bcs", FileMode.Open,FileAccess.Write));

            //find the address used by the template 
            br.BaseStream.Seek(72 + (tID * 4), SeekOrigin.Begin);
            int TemplateAddress = br.ReadInt32();

            //write new data location
            bw.BaseStream.Seek(0, SeekOrigin.End);
            int NewAddress = (int)bw.BaseStream.Position;
            bw.BaseStream.Seek(72 + (id * 4), SeekOrigin.Begin);
            bw.Write(NewAddress);

            //determine size of template using the next costume sets data.
            int Address;
            while (true)
            {
                Address = br.ReadInt32();
                if (Address != 0)
                    break;
            }

            int TemplateSize = Address - TemplateAddress;

            //copy the data onto end of bcs from templates data set.
            br.BaseStream.Seek(TemplateAddress, SeekOrigin.Begin);
            byte[] data = br.ReadBytes(TemplateSize);
            bw.BaseStream.Seek(0, SeekOrigin.End);
            bw.Write(data);


            //change id and adjust for text
            for (int i = 0; i < 10; i++)
            {
                br.BaseStream.Seek(TemplateAddress + 32 + (i * 4), SeekOrigin.Begin);
                int SubAddress = br.ReadInt32();
                if (SubAddress != 0)
                {
                    //adjust id
                    bw.BaseStream.Seek(NewAddress + SubAddress, SeekOrigin.Begin);
                    bw.Write((Int16)id);
                    bw.Write((Int16)id);

                    //handle text - head to position of addresses of the text
                    short check = 0;

                    br.BaseStream.Seek(TemplateAddress + SubAddress + 74,SeekOrigin.Begin);
                    check = br.ReadInt16();
                    
                    if (check == 1) 
                    {
                        int[] RTAddresses = new int[6]; //read text addresses
                        br.BaseStream.Seek(TemplateAddress + SubAddress + 120, SeekOrigin.Begin);
                        for (int j = 0; j < 6; j++)
                        {
                            RTAddresses[j] = br.ReadInt32();
                            
                        }

                        int[] WTAddresses = new int[6]; //write text addresses

                        for (int j = 0; j < 6; j++) 
                        {
                            if (RTAddresses[j] != 0)
                            {
                                br.BaseStream.Seek(TemplateAddress + SubAddress + 80 + RTAddresses[j], SeekOrigin.Begin);
                                List<byte> byteData = new List<byte>();
                                while (true)
                                {
                                    byteData.Add(br.ReadByte());
                                    if (byteData[byteData.Count - 1] == 0x00)
                                        break;

                                }

                                string text = Encoding.ASCII.GetString(byteData.ToArray());
                                text.Replace(tID.ToString("000"), id.ToString("000"));
                                
                                bw.BaseStream.Seek(0, SeekOrigin.End);
                                WTAddresses[j] = (int)bw.BaseStream.Position;
                                bw.Write(Encoding.ASCII.GetBytes(text));
                            }
                        }

                        //write text positions
                        int textAddresses = NewAddress + SubAddress + 80;
                        bw.BaseStream.Seek(NewAddress + SubAddress + 120,SeekOrigin.Begin);
                        for (int j = 0; j < 6; j++)
                        {
                            if (RTAddresses[j] != 0)
                                bw.Write(WTAddresses[j] - textAddresses);
                            else
                                bw.Write((int)0);
                        }
                    }
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
                if (lstchkType.GetItemCheckState(0) == CheckState.Checked || lstchkType.GetItemCheckState(1) == CheckState.Checked || lstchkType.GetItemCheckState(2) == CheckState.Checked || lstchkType.GetItemCheckState(3) == CheckState.Checked)
                {
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
                            BinaryReader br = new BinaryReader(File.Open(sysFolder + "/item/costume_" + items[i] + "_item.idb", FileMode.Open));
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
                }

                // accesssories
                if (lstchkType.GetItemCheckState(4) == CheckState.Checked)
                {
                    msg Names = msgStream.Load2(msgFolder + "/proper_noun_accessory_name_" + lang + ".msg");
                    msgData[] Expand = new msgData[Names.data.Length + 1];
                    Array.Copy(Names.data, Expand, Names.data.Length);
                    Expand[Expand.Length - 1].NameID = "accessory_" + Names.data.Length.ToString("000");
                    nID = Names.data.Length;
                    Expand[Expand.Length - 1].ID = Names.data.Length;
                    Expand[Expand.Length - 1].Lines = new string[] { txtName.Text };
                    Names.data = Expand;
                    msgStream.Save(Names, msgFolder + "/proper_noun_costume_name_" + lang + ".msg");

                    msg Descs = msgStream.Load2(msgFolder + "/proper_noun_accessory_info_" + lang + ".msg");
                    Expand = new msgData[Descs.data.Length + 1];
                    Array.Copy(Descs.data, Expand, Descs.data.Length);
                    Expand[Expand.Length - 1].NameID = "accessory_eff_" + Descs.data.Length.ToString("000");
                    dID = Descs.data.Length;
                    Expand[Expand.Length - 1].ID = Descs.data.Length;
                    Expand[Expand.Length - 1].Lines = new string[] { txtDesc.Text };
                    Descs.data = Expand;
                    msgStream.Save(Descs, msgFolder + "/proper_noun_costume_info_" + lang + ".msg");

                    BinaryReader br = new BinaryReader(File.Open(sysFolder + "/item/accessory_item.idb", FileMode.Open));
                    br.BaseStream.Seek(-640, SeekOrigin.End);
                    byte[] idbData = br.ReadBytes(640);
                    br.BaseStream.Seek(-640, SeekOrigin.End);
                    int id = br.ReadInt16();
                    br.Close();

                    FileInfo info = new FileInfo(sysFolder + "/item/accessory_item.idb");
                    int count = ((int)info.Length - 16) / 640;

                    

                    BinaryWriter bw = new BinaryWriter(File.Open(sysFolder + "/item/accessory_item.idb", FileMode.Open));
                    bw.BaseStream.Seek(0, SeekOrigin.End);
                    bw.Write(idbData);
                    bw.BaseStream.Seek(-640, SeekOrigin.End);
                    bw.Write((Int16)(id + 1));
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
