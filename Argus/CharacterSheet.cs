using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argus
{
    public class CharacterSheet
    {
        public long characterID { get; set; }
        public string name { get; set; }
        public long corporationID { get; set; }
        public string corporationName { get; set; }
        public List<CharacterSkills> skills { get; set; }
        public List<CharacterTitles> titles { get; set; }
        public string keyID { get; set; }
        public string vCode { get; set; }
        public string forumName { get; set; }

        public CharacterSheet()
        { }

        public CharacterSheet(long _charid, string _name, long _corpid, string _corpname, List<CharacterSkills> _skills, List<CharacterTitles> _titles)
        {
            this.characterID = _charid;
            this.name = _name;
            this.corporationID = _corpid;
            this.corporationName = _corpname;
            this.skills = _skills;
            this.titles = _titles;
        }
        public CharacterSheet(long _charid, string _name, long _corpid, string _corpname, string _fname, List<CharacterSkills> _skills, List<CharacterTitles> _titles)
        {
            this.characterID = _charid;
            this.name = _name;
            this.corporationID = _corpid;
            this.corporationName = _corpname;
            this.forumName = _fname;
            this.skills = _skills;
            this.titles = _titles;
        }
        public class CharacterImport
        {
            public long characterID { get; set; }
            public string name { get; set; }
            public long corporationID { get; set; }
            public string corporationName { get; set; }
            public string keyID { get; set; }
            public string vCode { get; set; }
            public string forumName { get; set; }
            public CharacterImport(string _name, long _charid, string _corpname, long _corpid, string _key, string _vCode, string _fname)
            {
                this.characterID = _charid;
                this.name = _name;
                this.corporationID = _corpid;
                this.corporationName = _corpname;
                this.keyID = _key;
                this.vCode = _vCode;
                this.forumName = _fname;
            }
        }
        public class CharacterTitles
        {
            public int titleID { get; set; }
            public string titleName { get; set; }
            public CharacterTitles(int _tid, string _tname)
            {
                this.titleID = _tid;
                this.titleName = _tname;
            }
        }
        public class CharacterSkills
        {
            public int typeID { get; set; }
            public int skillpoints { get; set; }
            public int level { get; set; }
            public string name { get; set; }
            public CharacterSkills(int _tid, int _sp, int _level, string _name)
            {
                this.typeID = _tid;
                this.skillpoints = _sp;
                this.level = _level;
                this.name = _name;
            }
            public CharacterSkills(int _tid, int _sp, int _level)
            {
                this.typeID = _tid;
                this.skillpoints = _sp;
                this.level = _level;                
            }
        }

    }
}
