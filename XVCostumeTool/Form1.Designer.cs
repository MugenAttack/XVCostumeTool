namespace XVCostumeTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstID = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBuy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revertBscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humanMaleHUMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humanFemaleHUFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namekianNMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.friezaRaceFRIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.majinMaleMAMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.majinFemaleMAFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.chlstRG = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSell = new System.Windows.Forms.TextBox();
            this.lstchkType = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnWork = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(13, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(227, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(13, 90);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(227, 119);
            this.txtDesc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // lstID
            // 
            this.lstID.FormattingEnabled = true;
            this.lstID.Location = new System.Drawing.Point(246, 49);
            this.lstID.Name = "lstID";
            this.lstID.Size = new System.Drawing.Size(122, 160);
            this.lstID.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Available IDs";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtBuy
            // 
            this.txtBuy.Location = new System.Drawing.Point(13, 240);
            this.txtBuy.Name = "txtBuy";
            this.txtBuy.Size = new System.Drawing.Size(106, 20);
            this.txtBuy.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Buy Value";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(385, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.revertBscToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // revertBscToolStripMenuItem
            // 
            this.revertBscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.humanMaleHUMToolStripMenuItem,
            this.humanFemaleHUFToolStripMenuItem,
            this.namekianNMCToolStripMenuItem,
            this.friezaRaceFRIToolStripMenuItem,
            this.majinMaleMAMToolStripMenuItem,
            this.majinFemaleMAFToolStripMenuItem});
            this.revertBscToolStripMenuItem.Name = "revertBscToolStripMenuItem";
            this.revertBscToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.revertBscToolStripMenuItem.Text = "Revert Bsc";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // humanMaleHUMToolStripMenuItem
            // 
            this.humanMaleHUMToolStripMenuItem.Name = "humanMaleHUMToolStripMenuItem";
            this.humanMaleHUMToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.humanMaleHUMToolStripMenuItem.Text = "Human Male (HUM)";
            this.humanMaleHUMToolStripMenuItem.Click += new System.EventHandler(this.humanMaleHUMToolStripMenuItem_Click);
            // 
            // humanFemaleHUFToolStripMenuItem
            // 
            this.humanFemaleHUFToolStripMenuItem.Name = "humanFemaleHUFToolStripMenuItem";
            this.humanFemaleHUFToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.humanFemaleHUFToolStripMenuItem.Text = "Human Female (HUF)";
            this.humanFemaleHUFToolStripMenuItem.Click += new System.EventHandler(this.humanFemaleHUFToolStripMenuItem_Click);
            // 
            // namekianNMCToolStripMenuItem
            // 
            this.namekianNMCToolStripMenuItem.Name = "namekianNMCToolStripMenuItem";
            this.namekianNMCToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.namekianNMCToolStripMenuItem.Text = "Namekian (NMC)";
            this.namekianNMCToolStripMenuItem.Click += new System.EventHandler(this.namekianNMCToolStripMenuItem_Click);
            // 
            // friezaRaceFRIToolStripMenuItem
            // 
            this.friezaRaceFRIToolStripMenuItem.Name = "friezaRaceFRIToolStripMenuItem";
            this.friezaRaceFRIToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.friezaRaceFRIToolStripMenuItem.Text = "Frieza Race (FRI)";
            this.friezaRaceFRIToolStripMenuItem.Click += new System.EventHandler(this.friezaRaceFRIToolStripMenuItem_Click);
            // 
            // majinMaleMAMToolStripMenuItem
            // 
            this.majinMaleMAMToolStripMenuItem.Name = "majinMaleMAMToolStripMenuItem";
            this.majinMaleMAMToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.majinMaleMAMToolStripMenuItem.Text = "Majin Male (MAM)";
            this.majinMaleMAMToolStripMenuItem.Click += new System.EventHandler(this.majinMaleMAMToolStripMenuItem_Click);
            // 
            // majinFemaleMAFToolStripMenuItem
            // 
            this.majinFemaleMAFToolStripMenuItem.Name = "majinFemaleMAFToolStripMenuItem";
            this.majinFemaleMAFToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.majinFemaleMAFToolStripMenuItem.Text = "Majin Female (MAF)";
            this.majinFemaleMAFToolStripMenuItem.Click += new System.EventHandler(this.majinFemaleMAFToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 314);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Template ID";
            // 
            // chlstRG
            // 
            this.chlstRG.FormattingEnabled = true;
            this.chlstRG.Items.AddRange(new object[] {
            "Human Male",
            "Human Female",
            "Saiyan Male",
            "Saiyan Female",
            "Namekian",
            "Frieza Race",
            "Majin Male",
            "Majin Female"});
            this.chlstRG.Location = new System.Drawing.Point(246, 231);
            this.chlstRG.Name = "chlstRG";
            this.chlstRG.Size = new System.Drawing.Size(122, 124);
            this.chlstRG.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Race/Gender";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // txtTID
            // 
            this.txtTID.Location = new System.Drawing.Point(13, 330);
            this.txtTID.Name = "txtTID";
            this.txtTID.Size = new System.Drawing.Size(106, 20);
            this.txtTID.TabIndex = 20;
            this.txtTID.Text = "707";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sell Value";
            // 
            // txtSell
            // 
            this.txtSell.Location = new System.Drawing.Point(13, 286);
            this.txtSell.Name = "txtSell";
            this.txtSell.Size = new System.Drawing.Size(106, 20);
            this.txtSell.TabIndex = 9;
            // 
            // lstchkType
            // 
            this.lstchkType.FormattingEnabled = true;
            this.lstchkType.Items.AddRange(new object[] {
            "Top",
            "Bottom",
            "Hands",
            "Feet",
            "Accessory"});
            this.lstchkType.Location = new System.Drawing.Point(125, 231);
            this.lstchkType.Name = "lstchkType";
            this.lstchkType.Size = new System.Drawing.Size(115, 124);
            this.lstchkType.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(128, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "IDB Inserts";
            // 
            // btnWork
            // 
            this.btnWork.Location = new System.Drawing.Point(81, 361);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(231, 23);
            this.btnWork.TabIndex = 23;
            this.btnWork.Text = "Add Costume";
            this.btnWork.UseVisualStyleBackColor = true;
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 394);
            this.Controls.Add(this.btnWork);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstchkType);
            this.Controls.Add(this.txtTID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chlstRG);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSell);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBuy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstID);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CAC Costume Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBuy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox chlstRG;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSell;
        private System.Windows.Forms.CheckedListBox lstchkType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnWork;
        private System.Windows.Forms.ToolStripMenuItem revertBscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem humanMaleHUMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem humanFemaleHUFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namekianNMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem friezaRaceFRIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem majinMaleMAMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem majinFemaleMAFToolStripMenuItem;
    }
}

