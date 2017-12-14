using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Model
{
    [Table("Device")]
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        public int Status { get; set; }

        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public List<Calamity> CalamityCollection { get; set; }

        public Device(string _Name, int _DeviceTypeId, string _SerialCode)
        {
            this.Name = _Name;
            this.SerialCode = _SerialCode;
            this.Status = 0;

            this.DeviceTypeId = _DeviceTypeId;

            this.CalamityCollection = new List<Calamity>();
        }

        public Device()
        {

        }
    }
}
