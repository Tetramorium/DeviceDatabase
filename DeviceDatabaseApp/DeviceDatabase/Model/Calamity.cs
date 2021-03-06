﻿using System;
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

        public bool IsSolved { get; set; }
        public string IsSolvedSolution { get; set; }
        public DateTime? IsSolvedDate { get; set; }

        public virtual Device Device { get; set; }
        public virtual int DeviceId { get; set; }

        public Calamity(string _About, DateTime _Date)
        {
            this.About = _About;
            this.Date = _Date;
        }

        public Calamity(string _About, DateTime _Date, bool _IsSolved)
        {
            this.About = _About;
            this.Date = _Date;
            this.IsSolved = _IsSolved;
        }

        public Calamity()
        {
        }
    }
}
