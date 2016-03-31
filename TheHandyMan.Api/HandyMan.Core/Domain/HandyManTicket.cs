using HandyMan.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Domain
{
    public class HandyManTicket
    {
        //Primary Key
        public int HandyManTicketId { get; set; }

        //Foreign Key 
        public string HandyManUserId { get; set; }
        public int TicketId { get; set; }

        //Properties

        //Relations
        public virtual Ticket Tickets { get; set; }
        public virtual User Handyman { get; set; }

        public HandyManTicket()
        {

        }

        public HandyManTicket(HandyManTicketModel model)
        {
            this.Update(model);
        }

        public void Update(HandyManTicketModel model)
        {
            TicketId = model.TicketId;
            HandyManUserId = model.HandyManUserId;
        }
    }
}
