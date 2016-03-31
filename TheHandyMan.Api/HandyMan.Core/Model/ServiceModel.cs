using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Model
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public virtual ICollection<TicketService> TicketServices { get; set; }
    }
}
