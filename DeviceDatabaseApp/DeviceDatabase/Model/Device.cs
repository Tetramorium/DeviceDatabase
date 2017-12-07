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
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string SerialCode { get; set; }
        public List<Calamity> CalamitiesList { get; set; }
        public int Status { get; set; }
    }
}
