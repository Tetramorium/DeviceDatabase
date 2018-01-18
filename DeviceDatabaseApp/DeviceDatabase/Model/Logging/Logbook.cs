using DeviceDatabase.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeviceDatabase.Model.Logging
{
    public class Logbook
    {
        [XmlElement("Log")]
        public SortableBindingList<Log> LogList { get; set; }

        public Logbook()
        {
            this.LogList = new SortableBindingList<Log>();
        }

        public void Add(Log _NewLog)
        {
            this.LogList.Add(_NewLog);
        }
    }
}
