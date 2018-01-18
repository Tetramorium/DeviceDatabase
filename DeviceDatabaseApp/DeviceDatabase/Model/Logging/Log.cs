using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceDatabase.Model.Logging
{
    public class Log
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string User { get; set; }

        public Log()
        {

        }

        public Log(string _Action)
        {
            this.Action = _Action;
            this.Date = DateTime.Now;
            this.User = Thread.CurrentPrincipal.Identity.Name;

            if (this.User == "")
                this.User = "Not authenticated";
        }
    }
}
