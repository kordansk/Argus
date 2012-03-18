namespace Argus
{
    partial class frmMain
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
            this.btn_ImportData = new System.Windows.Forms.Button();
            this.listMainView = new System.Windows.Forms.ListBox();
            this.btn_AddCorp = new System.Windows.Forms.Button();
            this.tabcontrol_Main = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.btn_RefreshList = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.lbl_CorpNameRemove = new System.Windows.Forms.Label();
            this.btnRemoveCorp = new System.Windows.Forms.Button();
            this.cbRemoveCorp = new System.Windows.Forms.ComboBox();
            this.btn_UpdateCorpDataXML = new System.Windows.Forms.Button();
            this.btn_UpdateSkillTree = new System.Windows.Forms.Button();
            this.lbl_Corp_vCode = new System.Windows.Forms.Label();
            this.lbl_CorpKeyID = new System.Windows.Forms.Label();
            this.cb_LockAddCorporation = new System.Windows.Forms.CheckBox();
            this.tb_Corp_vCode = new System.Windows.Forms.TextBox();
            this.tb_CorpKeyID = new System.Windows.Forms.TextBox();
            this.tabcontrol_Main.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ImportData
            // 
            this.btn_ImportData.Location = new System.Drawing.Point(656, 6);
            this.btn_ImportData.Name = "btn_ImportData";
            this.btn_ImportData.Size = new System.Drawing.Size(140, 30);
            this.btn_ImportData.TabIndex = 0;
            this.btn_ImportData.Text = "Import Data";
            this.btn_ImportData.UseVisualStyleBackColor = true;
            this.btn_ImportData.Click += new System.EventHandler(this.btn_ImportData_Click);
            // 
            // listMainView
            // 
            this.listMainView.FormattingEnabled = true;
            this.listMainView.Location = new System.Drawing.Point(6, 112);
            this.listMainView.Name = "listMainView";
            this.listMainView.Size = new System.Drawing.Size(787, 394);
            this.listMainView.TabIndex = 1;
            // 
            // btn_AddCorp
            // 
            this.btn_AddCorp.Location = new System.Drawing.Point(83, 186);
            this.btn_AddCorp.Name = "btn_AddCorp";
            this.btn_AddCorp.Size = new System.Drawing.Size(120, 30);
            this.btn_AddCorp.TabIndex = 2;
            this.btn_AddCorp.Text = "Add Corporation";
            this.btn_AddCorp.UseVisualStyleBackColor = true;
            this.btn_AddCorp.Click += new System.EventHandler(this.btn_AddCorp_Click);
            // 
            // tabcontrol_Main
            // 
            this.tabcontrol_Main.Controls.Add(this.tabMain);
            this.tabcontrol_Main.Controls.Add(this.tabSettings);
            this.tabcontrol_Main.Location = new System.Drawing.Point(12, 12);
            this.tabcontrol_Main.Name = "tabcontrol_Main";
            this.tabcontrol_Main.SelectedIndex = 0;
            this.tabcontrol_Main.Size = new System.Drawing.Size(810, 538);
            this.tabcontrol_Main.TabIndex = 3;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.btn_RefreshList);
            this.tabMain.Controls.Add(this.listMainView);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(802, 512);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main View";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // btn_RefreshList
            // 
            this.btn_RefreshList.Location = new System.Drawing.Point(656, 6);
            this.btn_RefreshList.Name = "btn_RefreshList";
            this.btn_RefreshList.Size = new System.Drawing.Size(140, 30);
            this.btn_RefreshList.TabIndex = 2;
            this.btn_RefreshList.Text = "Refresh";
            this.btn_RefreshList.UseVisualStyleBackColor = true;
            this.btn_RefreshList.Click += new System.EventHandler(this.btn_RefreshList_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.lbl_CorpNameRemove);
            this.tabSettings.Controls.Add(this.btnRemoveCorp);
            this.tabSettings.Controls.Add(this.cbRemoveCorp);
            this.tabSettings.Controls.Add(this.btn_UpdateCorpDataXML);
            this.tabSettings.Controls.Add(this.btn_UpdateSkillTree);
            this.tabSettings.Controls.Add(this.lbl_Corp_vCode);
            this.tabSettings.Controls.Add(this.lbl_CorpKeyID);
            this.tabSettings.Controls.Add(this.cb_LockAddCorporation);
            this.tabSettings.Controls.Add(this.tb_Corp_vCode);
            this.tabSettings.Controls.Add(this.tb_CorpKeyID);
            this.tabSettings.Controls.Add(this.btn_AddCorp);
            this.tabSettings.Controls.Add(this.btn_ImportData);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(802, 512);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // lbl_CorpNameRemove
            // 
            this.lbl_CorpNameRemove.AutoSize = true;
            this.lbl_CorpNameRemove.Location = new System.Drawing.Point(454, 458);
            this.lbl_CorpNameRemove.Name = "lbl_CorpNameRemove";
            this.lbl_CorpNameRemove.Size = new System.Drawing.Size(150, 13);
            this.lbl_CorpNameRemove.TabIndex = 12;
            this.lbl_CorpNameRemove.Text = "Corporation Name to Remove:";
            // 
            // btnRemoveCorp
            // 
            this.btnRemoveCorp.Location = new System.Drawing.Point(653, 419);
            this.btnRemoveCorp.Name = "btnRemoveCorp";
            this.btnRemoveCorp.Size = new System.Drawing.Size(140, 30);
            this.btnRemoveCorp.TabIndex = 11;
            this.btnRemoveCorp.Text = "Remove Corporation";
            this.btnRemoveCorp.UseVisualStyleBackColor = true;
            this.btnRemoveCorp.Click += new System.EventHandler(this.btnRemoveCorp_Click);
            // 
            // cbRemoveCorp
            // 
            this.cbRemoveCorp.FormattingEnabled = true;
            this.cbRemoveCorp.Location = new System.Drawing.Point(610, 455);
            this.cbRemoveCorp.Name = "cbRemoveCorp";
            this.cbRemoveCorp.Size = new System.Drawing.Size(183, 21);
            this.cbRemoveCorp.TabIndex = 10;
            // 
            // btn_UpdateCorpDataXML
            // 
            this.btn_UpdateCorpDataXML.Location = new System.Drawing.Point(656, 78);
            this.btn_UpdateCorpDataXML.Name = "btn_UpdateCorpDataXML";
            this.btn_UpdateCorpDataXML.Size = new System.Drawing.Size(140, 30);
            this.btn_UpdateCorpDataXML.TabIndex = 9;
            this.btn_UpdateCorpDataXML.Text = "Update Corporation API";
            this.btn_UpdateCorpDataXML.UseVisualStyleBackColor = true;
            this.btn_UpdateCorpDataXML.Click += new System.EventHandler(this.btn_UpdateCorpDataXML_Click);
            // 
            // btn_UpdateSkillTree
            // 
            this.btn_UpdateSkillTree.Location = new System.Drawing.Point(656, 42);
            this.btn_UpdateSkillTree.Name = "btn_UpdateSkillTree";
            this.btn_UpdateSkillTree.Size = new System.Drawing.Size(140, 30);
            this.btn_UpdateSkillTree.TabIndex = 8;
            this.btn_UpdateSkillTree.Text = "Update Skill Tree";
            this.btn_UpdateSkillTree.UseVisualStyleBackColor = true;
            this.btn_UpdateSkillTree.Click += new System.EventHandler(this.btn_UpdateSkillTree_Click);
            // 
            // lbl_Corp_vCode
            // 
            this.lbl_Corp_vCode.AutoSize = true;
            this.lbl_Corp_vCode.Location = new System.Drawing.Point(11, 250);
            this.lbl_Corp_vCode.Name = "lbl_Corp_vCode";
            this.lbl_Corp_vCode.Size = new System.Drawing.Size(66, 13);
            this.lbl_Corp_vCode.TabIndex = 7;
            this.lbl_Corp_vCode.Text = "Corp vCode:";
            // 
            // lbl_CorpKeyID
            // 
            this.lbl_CorpKeyID.AutoSize = true;
            this.lbl_CorpKeyID.Location = new System.Drawing.Point(13, 224);
            this.lbl_CorpKeyID.Name = "lbl_CorpKeyID";
            this.lbl_CorpKeyID.Size = new System.Drawing.Size(64, 13);
            this.lbl_CorpKeyID.TabIndex = 6;
            this.lbl_CorpKeyID.Text = "Corp KeyID:";
            // 
            // cb_LockAddCorporation
            // 
            this.cb_LockAddCorporation.AutoSize = true;
            this.cb_LockAddCorporation.Location = new System.Drawing.Point(221, 194);
            this.cb_LockAddCorporation.Name = "cb_LockAddCorporation";
            this.cb_LockAddCorporation.Size = new System.Drawing.Size(62, 17);
            this.cb_LockAddCorporation.TabIndex = 5;
            this.cb_LockAddCorporation.Text = "Locked";
            this.cb_LockAddCorporation.UseVisualStyleBackColor = true;
            this.cb_LockAddCorporation.CheckedChanged += new System.EventHandler(this.cb_LockCorpUpdate_CheckedChanged);
            // 
            // tb_Corp_vCode
            // 
            this.tb_Corp_vCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Corp_vCode.Location = new System.Drawing.Point(83, 248);
            this.tb_Corp_vCode.Name = "tb_Corp_vCode";
            this.tb_Corp_vCode.Size = new System.Drawing.Size(500, 20);
            this.tb_Corp_vCode.TabIndex = 4;
            // 
            // tb_CorpKeyID
            // 
            this.tb_CorpKeyID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_CorpKeyID.Location = new System.Drawing.Point(83, 222);
            this.tb_CorpKeyID.Name = "tb_CorpKeyID";
            this.tb_CorpKeyID.Size = new System.Drawing.Size(200, 20);
            this.tb_CorpKeyID.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 562);
            this.Controls.Add(this.tabcontrol_Main);
            this.Name = "frmMain";
            this.Text = "Argus";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabcontrol_Main.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ImportData;
        private System.Windows.Forms.ListBox listMainView;
        private System.Windows.Forms.Button btn_AddCorp;
        private System.Windows.Forms.TabControl tabcontrol_Main;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TextBox tb_Corp_vCode;
        private System.Windows.Forms.TextBox tb_CorpKeyID;
        private System.Windows.Forms.CheckBox cb_LockAddCorporation;
        private System.Windows.Forms.Label lbl_Corp_vCode;
        private System.Windows.Forms.Label lbl_CorpKeyID;
        private System.Windows.Forms.Button btn_UpdateSkillTree;
        private System.Windows.Forms.Button btn_UpdateCorpDataXML;
        private System.Windows.Forms.Button btn_RefreshList;
        private System.Windows.Forms.ComboBox cbRemoveCorp;
        private System.Windows.Forms.Button btnRemoveCorp;
        private System.Windows.Forms.Label lbl_CorpNameRemove;
    }
}

