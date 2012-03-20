using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argus
{
    public class CharacterView
    {        
        public string name { get; set; }
        public string corporationName { get; set; }
        public string forumName { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime logoffDateTime { get; set; }        
        public List<CharacterSheet.CharacterSkills> skills { get; set; }
        public List<CharacterSheet.CharacterTitles> titlesList { get; set; }
        public string titles { get; set; }

        public CharacterView()
        { }

        public CharacterView(string _name, string _cname, string _fname, DateTime _start, DateTime _logoff, List<CharacterSheet.CharacterSkills> _skills, string _titles, List<CharacterSheet.CharacterTitles> _titlelist)
        {
            this.name = _name;
            this.corporationName = _cname;
            this.forumName = _fname;
            this.startDateTime = _start;
            this.logoffDateTime = _logoff;
            this.skills = _skills;
            this.titles = _titles;
            this.titlesList = _titlelist;
        }        
    }
}
