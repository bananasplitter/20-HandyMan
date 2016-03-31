using HandyMan.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan.Data.Infrastructure
{
    public class HandyManDataContext : DbContext
    {
        public HandyManDataContext() : base("HandyMan")
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        }

        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Service> Services { get; set; }
        public IDbSet<Ticket> Tickets { get; set; }
        public IDbSet<TicketService> TicketServices { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<HandyManTicket> HandyManTickets { get; set; }
        public IDbSet<TimeEntry> TimeEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
            .HasMany(t => t.TicketServices)
            .WithRequired(ts => ts.Tickets)
            .HasForeignKey(ts => ts.TicketId)
            .WillCascadeOnDelete(false);


            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.HandyManTickets)
                .WithRequired(hmt => hmt.Tickets)
                .HasForeignKey(hmt => hmt.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.TimeEntries)
                .WithRequired(te => te.Ticket)
                .HasForeignKey(te => te.TicketId);

            modelBuilder.Entity<Service>()
            .HasMany(ts => ts.TicketServices)
            .WithRequired(ts => ts.Services)
            .HasForeignKey(ts => ts.ServiceId);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Tickets)
            .WithRequired(t => t.User)
            .HasForeignKey(t => t.UserId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.HandyManTickets)
                .WithRequired(hmt => hmt.Handyman)
                .HasForeignKey(hmt => hmt.HandyManUserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.TimeEntries)
                .WithRequired(te => te.User)
                .HasForeignKey(te => te.UserId);

            modelBuilder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithRequired(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);

        }
    }
}
