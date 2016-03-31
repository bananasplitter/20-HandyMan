using HandyMan.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Domain
{

    public class Ticket
    {
        //Primary Key
        public int TicketId { get; set; }

        //Foreign Key 
        public string UserId { get; set; }

        //Properties
        public string HandyManName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentEndDate { get; set; }
        public int? Confirmation { get; set; }
        public bool Transaction { get; set; }
        public bool Purchased { get; set; }

        //Relations
        public virtual User User { get; set; }
        public virtual ICollection<TicketService> TicketServices { get; set; }
        public virtual ICollection<HandyManTicket> HandyManTickets { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }

            public Ticket()
        {
            TicketServices = new Collection<TicketService>();
        }

            public Ticket(TicketModel model)
        {
            this.Update(model);
            this.AppointmentDate = DateTime.Now;
        }


        public void Update(TicketModel model)
        {
            TicketId = model.TicketId;
            UserId = model.UserId;
            AppointmentDate = model.AppointmentDate;
            Confirmation = model.Confirmation;
        }
    }
}
