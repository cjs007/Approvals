namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentRequestModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequests", "ClientName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentRequests", "ClientName");
        }
    }
}
