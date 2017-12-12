using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Model
{
    [Table("Calamity")]
    public class Calamity
    {
        [Key]
        public int CalamityId { get; set; }
        public string About { get; set; }
        public DateTime Date { get; set; }

        public int DeviceId { get; set; }
    }
}
