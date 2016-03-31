using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Model
{
    public class HandyManTicketModel
    {
        public int HandyManTicketId { get; set; }

        //Foreign Key 
        public string HandyManUserId { get; set; }
        public int TicketId { get; set; }

        //Properties
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }

        //Relations
        public virtual Ticket Tickets { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
