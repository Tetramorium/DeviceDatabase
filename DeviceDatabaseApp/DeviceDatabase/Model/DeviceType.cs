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
        public int Status { get; set; }

        public DeviceType(string _Name)
        {
            this.Name = _Name;
            this.Status = 0;
        }

        public DeviceType()
        {

        }
    }
}
