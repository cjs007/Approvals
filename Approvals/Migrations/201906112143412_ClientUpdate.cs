namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequests", "Client_Id", c => c.Int());
            CreateIndex("dbo.PaymentRequests", "Client_Id");
            AddForeignKey("dbo.PaymentRequests", "Client_Id", "dbo.Clients", "Id");
            DropColumn("dbo.PaymentRequests", "Client");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentRequests", "Client", c => c.String());
            DropForeignKey("dbo.PaymentRequests", "Client_Id", "dbo.Clients");
            DropIndex("dbo.PaymentRequests", new[] { "Client_Id" });
            DropColumn("dbo.PaymentRequests", "Client_Id");
        }
    }
}
