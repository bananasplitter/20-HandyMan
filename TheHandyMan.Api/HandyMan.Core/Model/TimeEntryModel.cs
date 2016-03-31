using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Model
{
    public class TimeEntryModel
    {
        public int TimeEntryId { get; set; }
        public string UserId { get; set; }
        public int TicketId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }

        public virtual User User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
