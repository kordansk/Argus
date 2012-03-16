using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Argus
{
    public class UserData
    {
        public string corp_KeyID { get; set; }
        public string corp_vCode { get; set; }
        public string corp_corpdata_xml { get; set; }

        public UserData(string _key, string _vc, string _cpx)
        {
            this.corp_KeyID = _key;
            this.corp_vCode = _vc;
            this.corp_corpdata_xml = _cpx;
        
        }

        public UserData()
        { }

        public UserData GetUserSettings()
        {
            UserData user = new UserData();
            XElement xUserData;
            xUserData = XElement.Load("userdata.xml");
            foreach (XElement usersettings in xUserData.Elements("settings"))
            {
                foreach (XElement xe in usersettings.Elements("user"))
                {
                    user.corp_KeyID = xe.Attribute("corp_keyID").Value;
                    user.corp_vCode = xe.Attribute("corp_vCode").Value;
                    user.corp_corpdata_xml = xe.Attribute("corpdataxml").Value;
                }
            }
            return user; 
        }
    }
}
