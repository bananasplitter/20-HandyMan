using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Core.Domain
{
    public class User : IUser<string>
    {
        // Primary Key
        public string Id { get; set; }

        // Foreign Keys (only if this class is on the many side of a relationship)
        public int RoleId { get; set; }

        // Properties
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public int? Rate { get; set; }
        
        // Relationships
        public virtual Role Role { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<HandyManTicket> HandyManTickets { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }

    }
}
