using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Argus
{
    public class CorpData
    {
        public long corporationID { get; set; }
        public string corporationName { get; set; }
        public string corporationTicker { get; set; }
        public int memberCount { get; set; }
        public int memberLimit { get; set; }
        public string ceoName { get; set; }
        public string allianceName { get; set; }
        public DateTime cachedUntil { get; set; }
        public List<Member> memberList { get; set; }
        
        public CorpData()
        {  }
        
        public CorpData(long _corpID, string _corpName, string _corpTicker, int _memCount, int _memLimit, string _ceoName, string _allianceName, DateTime _cache, List<Member> _members)
        {
            this.corporationID = _corpID;
            this.corporationName = _corpName;
            this.corporationTicker = _corpTicker;
            this.memberCount = _memCount;
            this.memberLimit = _memLimit;
            this.ceoName = _ceoName;
            this.allianceName = _allianceName;
            this.cachedUntil = _cache;
            this.memberList = _members;
        }
        public class Member
        {
            public long characterID { get; set; }
            public string name { get; set; }
            public DateTime startDateTime { get; set; }
            public DateTime logoffDateTime { get; set; }
            public string location { get; set; }
            public string shipType { get; set; }
            public Member(long _charID, string _name, DateTime _start, DateTime _logged, string _location, string _ship)
            {
                this.characterID = _charID;
                this.name = _name;
                this.startDateTime = _start;
                this.logoffDateTime = _logged;
                this.location = _location;
                this.shipType = _ship;
            }
        }
        
    }
}
