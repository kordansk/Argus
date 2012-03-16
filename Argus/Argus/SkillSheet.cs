using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Argus
{
    public class SkillSheet
    {
        public string groupName { get; set; }
        public int groupID { get; set; }
        public string typeName { get; set; }
        public int typeID { get; set; }

        public SkillSheet(string _gname, int _gid, string _tname, int _tid)
        {
            this.groupName = _gname;
            this.groupID = _gid;
            this.typeName = _tname;
            this.typeID = _tid;
        }

        public SkillSheet()
        { }

        public List<SkillSheet> GetSkills(string filename)
        {
            XElement xSkillSheet;
            xSkillSheet = XElement.Load(filename);
            List<SkillSheet> temp_list = new List<SkillSheet>();
            foreach (XElement skills in xSkillSheet.Elements("skills"))
            {
                foreach (XElement skill in skills.Elements("skill"))
                {
                    temp_list.Add(new SkillSheet(
                    skill.Attribute("groupName").Value,
                    int.Parse(skill.Attribute("groupID").Value),
                    skill.Attribute("typeName").Value,
                    int.Parse(skill.Attribute("typeID").Value)));
                }
            }
            return temp_list;
        }            
    }    
}
