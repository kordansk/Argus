using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argus
{
    public class ImportedData
    {
        public string ForumName { get; set; }
        public string KeyID { get; set; }
        public string vCode { get; set; }

        public ImportedData(string _fname, string _keyid, string _vcode)
        {
            this.ForumName = _fname;
            this.KeyID = _keyid;
            this.vCode = _vcode;
        }
    }
}
