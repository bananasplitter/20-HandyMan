using HandyMan.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Domain
{

    public class TicketService
    {
        //Primary Key
        public int TicketServiceId { get; set; }

        //Foreign Key 
        public int TicketId { get; set; }
        public int ServiceId { get; set; }

        //Properties


        //Relations
        public virtual Ticket Tickets { get; set; }
        public virtual Service Services { get; set; }

        public TicketService()
        {

        }

        public TicketService(TicketServiceModel model)
        {
            this.Update(model);
        }

        public void Update(TicketServiceModel model)
        {
            TicketId = model.TicketId;
            ServiceId = model.ServiceId;
        }
    }

}
