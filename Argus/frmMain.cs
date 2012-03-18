using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Argus
{   
    public partial class frmMain : Form
    {
        public List<ImportedData> CSVList = new List<ImportedData>();
        public List<CorpData> CorporationDataList = new List<CorpData>();
        public List<SkillSheet> EveSkillList = new List<SkillSheet>();
        public SkillSheet EveSkillObject = new SkillSheet();
        public List<CharacterSheet> EveCharactersList = new List<CharacterSheet>();
        public List<UserData> UserInfoList = new List<UserData>();
        public List<ListItem> CorpDropDownList = new List<ListItem>();
        public List<CharacterView> CharacterDataGrid = new List<CharacterView>();
        public string CorpFileXML;    
        public string CharacterDataXML = "datasheet.xml";  
        public string EveSkillsLocalXML = "skilldata.xml";
        public string UserDataLocalXML = "userdata.xml";
        public string EVE_API_RemoteXML = "https://api.eveonline.com";
        public string ImportFileNameXML;
        
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            cb_LockAddCorporation.Checked = true;
            btn_AddCorp.Enabled = false;
            tb_CorpKeyID.Enabled = false;
            tb_Corp_vCode.Enabled = false;
            LoadFormData();
        }
        private void LoadFormData()
        {
            List<CharacterSheet> emptyEveCharactersList = new List<CharacterSheet>();
            List<CorpData> emptyCorporationDataList = new List<CorpData>();
            List<SkillSheet> emptyEveSkillList = new List<SkillSheet>();
            List<ImportedData> emptyCSVList = new List<ImportedData>();
            List<ListItem> emptyCorpDropDownList = new List<ListItem>();
            List<UserData> emptyUserInfoList = new List<UserData>();
            List<CharacterView> emptyCharacterDataGrid = new List<CharacterView>();
            EveCharactersList = emptyEveCharactersList;
            CorporationDataList = emptyCorporationDataList;
            EveSkillList = emptyEveSkillList;
            CSVList = emptyCSVList;
            CorpDropDownList = emptyCorpDropDownList;
            UserInfoList = emptyUserInfoList;
            CharacterDataGrid = emptyCharacterDataGrid;
            cbRemoveCorp.Items.Clear();            
            if (File.Exists(UserDataLocalXML))
            {
                XElement xUserData;
                xUserData = XElement.Load(UserDataLocalXML);
                foreach (XElement usersettings in xUserData.Elements("settings"))
                {
                    foreach (XElement xe in usersettings.Elements("user"))
                    {
                        UserInfoList.Add(new UserData(
                            xe.Attribute("corp_keyID").Value,
                            xe.Attribute("corp_vCode").Value,
                            xe.Attribute("corpdataxml").Value));
                    }
                }
                if (UserInfoList.Count > 0)
                {
                    foreach (UserData user in UserInfoList)
                    {
                        LoadCorpData(user.corp_corpdata_xml);
                    } 
                    int i = 0;
                    foreach (CorpData corp in CorporationDataList)
                    {
                        CorpDropDownList.Add(new ListItem(corp.corporationName.ToString(), UserInfoList[i].corp_KeyID.ToString()));
                        i++;
                    }
                }
                foreach (ListItem item in CorpDropDownList)
                {
                    cbRemoveCorp.Items.Add(item);
                    cbRemoveCorp.SelectedIndex = 0;
                }
                if (cbRemoveCorp.Items.Count == 0)
                {
                    cbRemoveCorp.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No userdata loaded, please add your corp API information.");
            }
            if (File.Exists(EveSkillsLocalXML))
            {
                EveSkillList = EveSkillObject.GetSkills(EveSkillsLocalXML);                
            }
            else
            {
                MessageBox.Show("Updating Skill List.");
                UpdateLocalEveSkillsXML();
            }
            if (File.Exists(CharacterDataXML))
            {
                LoadLocalDataSheet();
                foreach (CharacterSheet item in EveCharactersList)
                {
                    listMainView.Items.Add(item.name + "\t" + item.forumName);
                }
            
            }
            else
            {
                XDocument CreateDatasheetXML = new XDocument();
                XElement DatasheetRoot = new XElement("characters");
                CreateDatasheetXML.Add(DatasheetRoot);
                CreateDatasheetXML.Save(CharacterDataXML); 
            }

            /*
            _dgMainView.DataSource = CharacterDataGrid;
            _dgMainView.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn ownerColumn = new DataGridViewTextBoxColumn();
            ownerColumn.DataPropertyName = "forumName";
            ownerColumn.HeaderText = "Forum Owner";
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "name";
            nameColumn.HeaderText = "Character Name";
            DataGridViewTextBoxColumn corpnameColumn = new DataGridViewTextBoxColumn();
            corpnameColumn.DataPropertyName = "corporationName";
            corpnameColumn.HeaderText = "Corporation Name";
            DataGridViewTextBoxColumn startdateColumn = new DataGridViewTextBoxColumn();
            startdateColumn.DataPropertyName = "startDateTime";
            startdateColumn.HeaderText = "Start Date";
            DataGridViewTextBoxColumn logoffdateColumn = new DataGridViewTextBoxColumn();
            logoffdateColumn.DataPropertyName = "logoffDateTime";
            logoffdateColumn.HeaderText = "Last Time Logged In";
            */

            foreach (CharacterSheet pilot in EveCharactersList)
            {
                CorpData this_pilots_corp = CorporationDataList.Find(delegate(CorpData s) { return s.corporationID == pilot.corporationID; });
                CorpData.Member this_pilots_corp_name = this_pilots_corp.memberList.Find(delegate (CorpData.Member s) {return s.characterID == pilot.characterID; });
                //CharacterDataGrid.Add(new CharacterView(pilot.name, pilot.corporationName, pilot.forumName, this_pilots_corp_name.startDateTime, this_pilots_corp_name.logoffDateTime, pilot.skills, pilot.titles));
            }
            //_dgMainView.DataSource = CharacterDataGrid;
            foreach (CharacterSheet item in EveCharactersList)
            {
                listMainView.Items.Add(item.name + "\t" + item.forumName);
            }
            
        }
        private void LoadCorpData(string file)
        {
            XElement xCorpData;
            CorpData corp_object = new CorpData();
            xCorpData = XElement.Load(file);
            List<CorpData.Member> temp_list = new List<CorpData.Member>();
            foreach (XElement csettings in xCorpData.Elements("corpinfo"))
            {
                corp_object.corporationID = Int64.Parse(csettings.Element("corporationID").Value);
                corp_object.corporationName = csettings.Element("corporationName").Value;
                corp_object.corporationTicker = csettings.Element("corporationTicker").Value;
                corp_object.memberCount = int.Parse(csettings.Element("memberCount").Value);
                corp_object.memberLimit = int.Parse(csettings.Element("memberLimit").Value);
                corp_object.ceoName = csettings.Element("ceoName").Value;
                corp_object.cachedUntil = Convert.ToDateTime(csettings.Element("cachedUntil").Value);
            }
            foreach (XElement members in xCorpData.Elements("members"))
            {
                foreach (XElement member in members.Elements("member"))
                {
                    temp_list.Add(new CorpData.Member(int.Parse(member.Attribute("characterID").Value),
                        member.Attribute("name").Value,
                        Convert.ToDateTime(member.Attribute("startDateTime").Value),
                        Convert.ToDateTime(member.Attribute("logoffDateTime").Value),
                        member.Attribute("location").Value,
                        member.Attribute("shipType").Value));
                }
            }
            corp_object.memberList = temp_list;
            CorporationDataList.Add(corp_object);
        }
        private void LoadLocalDataSheet() //loads local datasheet into memory (this is the file with the actual information to manipulate
        {
            XElement xDataSheet;
            xDataSheet = XElement.Load(CharacterDataXML);
            foreach (XElement pilot in xDataSheet.Elements("pilot"))
            {
                List<CharacterSheet.CharacterSkills> temp_skills = new List<CharacterSheet.CharacterSkills>();                
                List<CharacterSheet.CharacterTitles> temp_titles = new List<CharacterSheet.CharacterTitles>();                
                foreach (XElement skills in pilot.Elements("skills"))
                {
                    foreach (XElement skill in skills.Elements("skill"))
                    {
                        temp_skills.Add(new CharacterSheet.CharacterSkills(
                            int.Parse(skill.Attribute("typeID").Value), 
                            int.Parse(skill.Attribute("skillpoints").Value), 
                            int.Parse(skill.Attribute("level").Value),
                            skill.Attribute("name").Value));
                    }
                }
                foreach (XElement titles in pilot.Elements("titles"))
                {
                    foreach (XElement title in titles.Elements("title"))
                    {
                        temp_titles.Add(new CharacterSheet.CharacterTitles(
                            int.Parse(title.Attribute("titleID").Value), 
                            title.Attribute("titleName").Value));
                    }
                }
                EveCharactersList.Add(new CharacterSheet(
                    Convert.ToInt64(pilot.Attribute("characterID").Value),
                    pilot.Attribute("name").Value,
                    Convert.ToInt64(pilot.Attribute("corporationID").Value),
                    pilot.Attribute("corporationName").Value,
                    pilot.Attribute("forumName").Value,
                    temp_skills,
                    temp_titles));   
            }
        }
        private void UpdateLocalEveSkillsXML() //grabs remote skill data and saves it locally
        {
            Application.DoEvents();
            XElement xRemoteSkillTree;
            string remote_skills_api = EVE_API_RemoteXML + "/eve/SkillTree.xml.aspx";
            xRemoteSkillTree = XElement.Load(remote_skills_api);
            foreach (XElement result in xRemoteSkillTree.Elements("result"))
            {
                foreach (XElement rowset_upper in result.Elements("rowset"))
                {
                    foreach (XElement rowgroup in rowset_upper.Elements("row"))
                    {
                        foreach (XElement rowset_lower in rowgroup.Elements("rowset"))
                        {
                            foreach (XElement row in rowset_lower.Elements("row"))
                            {
                                EveSkillList.Add(new SkillSheet(
                                    rowgroup.Attribute("groupName").Value,
                                    int.Parse(row.Attribute("groupID").Value),
                                    row.Attribute("typeName").Value,
                                    int.Parse(row.Attribute("typeID").Value)));
                            }
                        }
                    }
                }
            }
            if (File.Exists(EveSkillsLocalXML))
            {
                File.Delete(EveSkillsLocalXML);
            }
            XDocument WriteLocalSkills = new XDocument();
            XElement xroot = new XElement("skill_list");
            xroot.Add(new XElement("skills"));
            WriteLocalSkills.Add(xroot);
            WriteLocalSkills.Save(EveSkillsLocalXML);
            foreach (SkillSheet skill in EveSkillList)
            {
                WriteLocalSkills.Root.Element("skills").Add(
                    new XElement("skill",
                        new XAttribute("groupName", skill.groupName),
                        new XAttribute("groupID", skill.groupID),
                        new XAttribute("typeName", skill.typeName),
                        new XAttribute("typeID", skill.typeID)));
            }
            WriteLocalSkills.Save(EveSkillsLocalXML);
        }
        private void UpdateLocalCorpXML() //grabs remote api data and saves it locally; updates all CorpXML Files
        {
            foreach (UserData users in UserInfoList)
            {
                if (File.Exists(users.corp_corpdata_xml))
                {
                    File.Delete(users.corp_corpdata_xml);
                }
                string remote_corp_API = EVE_API_RemoteXML + "/corp/CorporationSheet.xml.aspx?keyID=" + users.corp_KeyID + "&vCode=" + users.corp_vCode;
                string remote_corp_members = EVE_API_RemoteXML + "/corp/MemberTracking.xml.aspx?keyID=" + users.corp_KeyID + "&vCode=" + users.corp_vCode + "&extended=1";
                XElement xRemoteCorpAPI;
                XElement xRemoteCorpMembers;
                CorpData temp_corp_vals = new CorpData();
                xRemoteCorpAPI = XElement.Load(remote_corp_API);
                xRemoteCorpMembers = XElement.Load(remote_corp_members);
                XDocument WriteCorpData = new XDocument();
                XElement corproot = new XElement("corpdata");
                corproot.Add(new XElement("corpinfo"));
                corproot.Add(new XElement("members"));
                WriteCorpData.Add(corproot);
                WriteCorpData.Save(users.corp_corpdata_xml);
                List<CorpData.Member> temp_members = new List<CorpData.Member>();
                foreach (XElement result in xRemoteCorpAPI.Elements("result"))
                {
                    temp_corp_vals.corporationID = Int64.Parse(result.Element("corporationID").Value);
                    temp_corp_vals.corporationName = result.Element("corporationName").Value;
                    temp_corp_vals.corporationTicker = result.Element("ticker").Value;
                    temp_corp_vals.ceoName = result.Element("ceoName").Value;
                    temp_corp_vals.allianceName = result.Element("allianceName").Value;
                    temp_corp_vals.memberCount = int.Parse(result.Element("memberCount").Value);
                    temp_corp_vals.memberLimit = int.Parse(result.Element("memberLimit").Value);
                }
                temp_corp_vals.cachedUntil = Convert.ToDateTime(xRemoteCorpAPI.Element("cachedUntil").Value);
                foreach (XElement result in xRemoteCorpMembers.Elements("result"))
                {
                    foreach (XElement rowset in result.Elements("rowset"))
                    {
                        foreach (XElement row in rowset.Elements("row"))
                        {
                            temp_members.Add(new CorpData.Member(
                                 Int64.Parse(row.Attribute("characterID").Value),
                                 row.Attribute("name").Value,
                                 Convert.ToDateTime(row.Attribute("startDateTime").Value),
                                 Convert.ToDateTime(row.Attribute("logoffDateTime").Value),
                                 row.Attribute("location").Value,
                                 row.Attribute("shipType").Value));
                        }
                    }
                }
                temp_corp_vals.memberList = temp_members;
                WriteCorpData.Root.Element("corpinfo").Add(
                    new XElement("corporationID", temp_corp_vals.corporationID),
                    new XElement("corporationName", temp_corp_vals.corporationName),
                    new XElement("corporationTicker", temp_corp_vals.corporationTicker),
                    new XElement("memberCount", temp_corp_vals.memberCount),
                    new XElement("memberLimit", temp_corp_vals.memberLimit),
                    new XElement("ceoName", temp_corp_vals.ceoName),
                    new XElement("cachedUntil", temp_corp_vals.cachedUntil)
                );
                foreach (CorpData.Member members in temp_corp_vals.memberList)
                {
                    WriteCorpData.Root.Element("members").Add(
                        new XElement("member",
                            new XAttribute("characterID", members.characterID),
                            new XAttribute("name", members.name),
                            new XAttribute("startDateTime", members.startDateTime),
                            new XAttribute("logoffDateTime", members.logoffDateTime),
                            new XAttribute("location", members.location),
                            new XAttribute("shipType", members.shipType)
                         ));
                }
                WriteCorpData.Save(users.corp_corpdata_xml);
            }
        }
        private void AddToDatasetXML()
        {
            List<CharacterSheet.CharacterImport> list_getcharids = new List<CharacterSheet.CharacterImport>();
            List<CharacterSheet> list_updatechars = new List<CharacterSheet>();
            foreach (ImportedData line in CSVList)
            {
                string remote_corp_API = EVE_API_RemoteXML + "/account/Characters.xml.aspx?keyID=" + line.KeyID + "&vCode=" + line.vCode;
                XElement xRemoteCharacters;
                xRemoteCharacters = XElement.Load(remote_corp_API);
                foreach (XElement result in xRemoteCharacters.Elements("result"))
                {
                    foreach (XElement rowset in result.Elements("rowset"))
                    {
                        foreach (XElement row in rowset.Elements("row"))
                        {
                            list_getcharids.Add(new CharacterSheet.CharacterImport(
                                row.Attribute("name").Value,
                                Int64.Parse(row.Attribute("characterID").Value),
                                row.Attribute("corporationName").Value,
                                Int64.Parse(row.Attribute("corporationID").Value),
                                line.KeyID,
                                line.vCode,
                                line.ForumName));
                        }
                    }
                }
            }
            // Retrieving the XML data from EVE API
            foreach (CharacterSheet.CharacterImport chars in list_getcharids)
            {
                List<CharacterSheet.CharacterSkills> temp_skills = new List<CharacterSheet.CharacterSkills>();
                List<CharacterSheet.CharacterTitles> temp_titles = new List<CharacterSheet.CharacterTitles>();                
                string remote_character_sheet_API = EVE_API_RemoteXML + "/char/CharacterSheet.xml.aspx?keyID=" + chars.keyID + "&vCode=" + chars.vCode + "&characterID=" + chars.characterID;
                XElement xRemoteCharacterSheet;
                xRemoteCharacterSheet = XElement.Load(remote_character_sheet_API);
                XDocument xdocCharSheet = XDocument.Load(remote_character_sheet_API);
                IEnumerable<XElement> select_titles =
                    from el in xdocCharSheet.Descendants("rowset")
                    where el.Attribute("name").Value == "corporationTitles"
                    select el;
                foreach (XElement titles in select_titles.Elements("row"))
                {
                    temp_titles.Add(new CharacterSheet.CharacterTitles(
                        int.Parse(titles.Attribute("titleID").Value),
                        titles.Attribute("titleName").Value));
                }

                IEnumerable<XElement> select_skills =
                    from el in xdocCharSheet.Descendants("rowset")
                    where el.Attribute("name").Value == "skills"
                    select el;
                foreach (XElement skills in select_skills.Elements("row"))
                {
                    SkillSheet skill_name = EveSkillList.Find(delegate(SkillSheet s) { return s.typeID == int.Parse(skills.Attribute("typeID").Value); });
                    temp_skills.Add(new CharacterSheet.CharacterSkills(
                        int.Parse(skills.Attribute("typeID").Value),
                        int.Parse(skills.Attribute("skillpoints").Value),
                        int.Parse(skills.Attribute("level").Value),
                        skill_name.typeName));
                }
                foreach (XElement result in xRemoteCharacterSheet.Descendants("result"))
                {
                    CharacterSheet.CharacterImport this_character = list_getcharids.Find(delegate(CharacterSheet.CharacterImport s) { return s.characterID.ToString() == result.Element("characterID").Value.ToString(); });
                    list_updatechars.Add(new CharacterSheet(
                        Int64.Parse(result.Element("characterID").Value),
                        result.Element("name").Value,
                        Int64.Parse(result.Element("corporationID").Value),
                        result.Element("corporationName").Value,
                        this_character.forumName,
                        temp_skills,
                        temp_titles
                        ));
                }
            }
            //Adding to dataset here.   
            // if updatedatasheet doesnt contain a reference to a characterid in the list_updatechars, 
            // add it to the sheet; else delete that node and add new data to update list_updatechars and EveCharactersList
            XDocument UpdateDataSheet = XDocument.Load(CharacterDataXML);
            List<long> characterID_duplicates = new List<long>();
            foreach (CharacterSheet import in list_updatechars)
            {
                foreach (CharacterSheet exists in EveCharactersList)
                {
                    if (import.characterID == exists.characterID)
                    {
                        characterID_duplicates.Add(exists.characterID);
                    } 
                }
            }
            foreach (long id in characterID_duplicates)
            {
                UpdateDataSheet.Descendants("pilot").Where(p => p.Attribute("characterID") != null && (string)p.Attribute("characterID") == id.ToString()).Remove();
            }
            characterID_duplicates.Clear();
            UpdateDataSheet.Save(CharacterDataXML);
            foreach (CharacterSheet pilot in list_updatechars)
            {               
                UpdateDataSheet.Element("characters").Add(
                    new XElement("pilot",
                        new XAttribute("characterID", pilot.characterID),
                        new XAttribute("name", pilot.name),
                        new XAttribute("corporationID", pilot.corporationID),
                        new XAttribute("corporationName", pilot.corporationName),
                        new XAttribute("forumName", pilot.forumName)
                        ));
                IEnumerable<XElement> children = from el in UpdateDataSheet.Root.Elements("pilot")
                                                 where (string)el.Attribute("characterID") == pilot.characterID.ToString()
                                                 select el;
                foreach (XElement el in children)
                {
                    el.Add(new XElement("skills"));
                    el.Add(new XElement("titles"));
                    foreach (CharacterSheet.CharacterSkills skill in pilot.skills)
                    {
                        el.Element("skills").Add(
                            new XElement("skill",
                                new XAttribute("typeID", skill.typeID),
                                new XAttribute("skillpoints", skill.skillpoints),
                                new XAttribute("level", skill.level),
                                new XAttribute("name", skill.name)));
                    }
                    foreach (CharacterSheet.CharacterTitles title in pilot.titles)
                    {
                        el.Element("titles").Add(
                            new XElement("title",
                                new XAttribute("titleID", title.titleID),
                                new XAttribute("titleName", title.titleName)));
                    }
                }
            }
            UpdateDataSheet.Save(CharacterDataXML);
            LoadFormData();
        }
        private void AddDataFromCSV()
        {
            char[] seps = { ',' };
            if (ImportFileNameXML != null)
            {
                string[] csvDataFile = File.ReadAllLines(ImportFileNameXML);
                foreach (string line in csvDataFile)
                {
                    string[] split_values = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                    CSVList.Add(new ImportedData(split_values[0], split_values[1], split_values[2]));
                }
            }
            AddToDatasetXML();
        } //grabs all new user data files and loads them into memory from CSV file
        private void AddCorp()
        {
            string corpdataFileName = "corpdata_" + tb_CorpKeyID.Text + ".xml";
            if (!File.Exists(UserDataLocalXML))
            {
                XDocument WriteUserData = new XDocument();
                XElement xroot = new XElement("UserData");
                xroot.Add(new XElement("settings"));
                WriteUserData.Add(xroot);
                WriteUserData.Save(UserDataLocalXML);
                WriteUserData.Root.Element("settings").Add(
                    new XElement("user",
                        new XAttribute("corp_keyID", tb_CorpKeyID.Text),
                        new XAttribute("corp_vCode", tb_Corp_vCode.Text),
                        new XAttribute("corpdataxml", corpdataFileName)));
                WriteUserData.Save(UserDataLocalXML);
            }
            else
            {
                XDocument UpdateUserData = XDocument.Load(UserDataLocalXML);
                UpdateUserData.Root.Element("settings").Add(new XElement("user",
                    new XAttribute("corp_keyID", tb_CorpKeyID.Text),
                    new XAttribute("corp_vCode", tb_Corp_vCode.Text),
                    new XAttribute("corpdataxml", corpdataFileName)));
                UpdateUserData.Save(UserDataLocalXML);
            }
            tb_Corp_vCode.Clear();
            tb_CorpKeyID.Clear();
            cb_LockAddCorporation.Checked = true;
            btn_AddCorp.Enabled = false;
            tb_CorpKeyID.Enabled = false;
            tb_Corp_vCode.Enabled = false;
            XElement xUserData;
            xUserData = XElement.Load(UserDataLocalXML);
            foreach (XElement usersettings in xUserData.Elements("settings"))
            {
                foreach (XElement xe in usersettings.Elements("user"))
                {
                    UserInfoList.Add(new UserData(
                        xe.Attribute("corp_keyID").Value,
                        xe.Attribute("corp_vCode").Value,
                        xe.Attribute("corpdataxml").Value));
                }
            }
        }
        private void RemoveCorp()
        {
            if (CorpDropDownList.Count > 0)
            {
                ListItem corpKeyID = (ListItem)cbRemoveCorp.SelectedItem;
                XDocument RemoveCorp = XDocument.Load(UserDataLocalXML);
                RemoveCorp.Descendants("settings").Elements("user").Where(p => p.Attribute("corp_keyID") != null && (string)p.Attribute("corp_keyID") == corpKeyID.value.ToString()).Remove();
                RemoveCorp.Save(UserDataLocalXML);
                File.Delete("corpdata_" + corpKeyID.value + ".xml");
                cbRemoveCorp.Items.RemoveAt(cbRemoveCorp.SelectedIndex);
                LoadFormData();
            }
        }
        private void cb_LockCorpUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_LockAddCorporation.Checked == false)
            {
                btn_AddCorp.Enabled = true;
                tb_CorpKeyID.Enabled = true;
                tb_Corp_vCode.Enabled = true;
            }
            else
            {
                btn_AddCorp.Enabled = false;
                tb_CorpKeyID.Enabled = false;
                tb_Corp_vCode.Enabled = false;
            }
        }
        private void btn_AddCorp_Click(object sender, EventArgs e) //saves corp data in the boxes into an xml
        {
            if (tb_CorpKeyID.Text != "" & tb_Corp_vCode.Text != "")
            {
                AddCorp();
                UpdateLocalCorpXML();
                LoadFormData();
            }
            else
            {
                MessageBox.Show("Please enter a Key ID and a vCode.");
            }
        }
        private void btn_UpdateCorpDataXML_Click(object sender, EventArgs e)
        {
            UpdateLocalCorpXML();
        }
        private void btn_UpdateSkillTree_Click(object sender, EventArgs e) //this begins updating the skill data from remotely
        {
            UpdateLocalEveSkillsXML();
        }
        private void btn_ImportData_Click(object sender, EventArgs e) //gets the csv file path to import into memory
        {
            ImportFileNameXML = getFileName();
            AddDataFromCSV();
        }
        private void btn_RefreshList_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        public string getFileName()
        {
            OpenFileDialog fileBrowserDialog = new OpenFileDialog();
            fileBrowserDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fileBrowserDialog.Filter = "csv files (*.csv)|*.csv";
            fileBrowserDialog.FilterIndex = 1;
            if (fileBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string _filename = fileBrowserDialog.FileName;
                return _filename;
            }
            else
            {
                return null;
            }
        }
        private void RefreshData()
        {
            LoadFormData();
        }
        private void btnRemoveCorp_Click(object sender, EventArgs e)
        {
            RemoveCorp();
        }

    }
}
