namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientUsersUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserClients",
                c => new
                {
                    ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    Client_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Client_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Client_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ApplicationUserClients", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserClients", new[] { "Client_Id" });
            DropIndex("dbo.ApplicationUserClients", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserClients");
        }
    }
}
