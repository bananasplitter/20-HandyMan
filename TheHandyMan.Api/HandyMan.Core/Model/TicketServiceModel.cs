using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Model
{
    public class TicketServiceModel
    {
        public int TicketServiceId { get; set; }
        public int TicketId { get; set; }
        public int ServiceId { get; set; }

        public string UserId { get; set; }

        public virtual Ticket Tickets { get; set; }
        public virtual Service Services { get; set; }
    }
}
