using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Model
{
    public class TicketModel
    {

        public int TicketId { get; set; }
        public string UserId { get; set; }
        public int? Confirmation { get; set; }
        public bool Transaction { get; set; }
        public bool Purchased { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentEndDate { get; set; }

        public virtual User Users { get; set; }
        public virtual ICollection<TicketService> TicketServices { get; set; }
        public virtual ICollection<HandyManTicket> HandyManTickets { get; set; }

    }
}
