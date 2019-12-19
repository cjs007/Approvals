namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientIdUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentRequests", "Client_Id", "dbo.Clients");
            DropIndex("dbo.PaymentRequests", new[] { "Client_Id" });
            RenameColumn(table: "dbo.PaymentRequests", name: "Client_Id", newName: "ClientId");
            AlterColumn("dbo.PaymentRequests", "ClientId", c => c.Int(nullable: true));
            CreateIndex("dbo.PaymentRequests", "ClientId");
            AddForeignKey("dbo.PaymentRequests", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            DropColumn("dbo.PaymentRequests", "ClientName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentRequests", "ClientName", c => c.String());
            DropForeignKey("dbo.PaymentRequests", "ClientId", "dbo.Clients");
            DropIndex("dbo.PaymentRequests", new[] { "ClientId" });
            AlterColumn("dbo.PaymentRequests", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.PaymentRequests", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.PaymentRequests", "Client_Id");
            AddForeignKey("dbo.PaymentRequests", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
