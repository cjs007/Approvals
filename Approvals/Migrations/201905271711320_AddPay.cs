namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequests", "CheckNumber", c => c.String());
            AddColumn("dbo.PaymentRequests", "PaidDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentRequests", "PaidDate");
            DropColumn("dbo.PaymentRequests", "CheckNumber");
        }
    }
}
