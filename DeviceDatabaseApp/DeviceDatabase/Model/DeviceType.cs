using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Model
{
    [Table("DeviceType")]
    public class DeviceType
    {
        [Key]
        public int DeviceTypeId { get; set; }
        public string Name { get; set; }

        public DeviceType(string _Name)
        {
            this.Name = _Name;
        }

        public DeviceType()
        {

        }

        //https://rachel53461.wordpress.com/2011/08/20/comboboxs-selecteditem-not-displaying/

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DeviceType))
                return false;

            return ((DeviceType)obj).DeviceTypeId == this.DeviceTypeId;
        }
    }
}
