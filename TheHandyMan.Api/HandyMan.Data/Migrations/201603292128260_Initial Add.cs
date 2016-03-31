namespace HandyMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HandyManTickets",
                c => new
                    {
                        HandyManTicketId = c.Int(nullable: false, identity: true),
                        HandyManUserId = c.String(nullable: false, maxLength: 128),
                        TicketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HandyManTicketId)
                .ForeignKey("dbo.Users", t => t.HandyManUserId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.HandyManUserId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RoleId = c.Int(nullable: false),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        State = c.String(),
                        Rate = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        HandyManName = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        AppointmentEndDate = c.DateTime(nullable: false),
                        Confirmation = c.Int(),
                        Transaction = c.Boolean(nullable: false),
                        Purchased = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TicketServices",
                c => new
                    {
                        TicketServiceId = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketServiceId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.TimeEntries",
                c => new
                    {
                        TimeEntryId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        TicketId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.TimeEntryId)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TicketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeEntries", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.TimeEntries", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketServices", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.HandyManTickets", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.HandyManTickets", "HandyManUserId", "dbo.Users");
            DropIndex("dbo.TimeEntries", new[] { "TicketId" });
            DropIndex("dbo.TimeEntries", new[] { "UserId" });
            DropIndex("dbo.TicketServices", new[] { "ServiceId" });
            DropIndex("dbo.TicketServices", new[] { "TicketId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.HandyManTickets", new[] { "TicketId" });
            DropIndex("dbo.HandyManTickets", new[] { "HandyManUserId" });
            DropTable("dbo.TimeEntries");
            DropTable("dbo.Services");
            DropTable("dbo.TicketServices");
            DropTable("dbo.Tickets");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.HandyManTickets");
        }
    }
}
