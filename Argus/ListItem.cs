using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argus
{
    public class ListItem
    {
        public string name { get; set; }
        public string value { get; set; }
        public ListItem(string _name, string _value)
        {
            this.name = _name;
            this.value = _value;
        }
        public override string ToString()
        {
            return name.ToString();
        }
    }
}
