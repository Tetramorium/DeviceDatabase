using DeviceDatabase.Controller;
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

        // For a 0-to-many relationship in Entity Framework, have the foreign key be nullable. => ?
        public virtual int? DeviceTypeId { get; set; }

        public DeviceType DeviceType { get; set; }

        public virtual List<Calamity> CalamityCollection { get; set; }

        public Device(string _Name, int _DeviceTypeId)
        {
            this.Name = _Name;
            this.SerialCode = SerialCodeController.GenerateRandomSerialCode();
            this.DeviceTypeId = _DeviceTypeId;
            this.CalamityCollection = new List<Calamity>();
        }

        public Device()
        {
        }
    }
}
