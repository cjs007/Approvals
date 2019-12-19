namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentRequestUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequests", "Submitter", c => c.String());
            AddColumn("dbo.PaymentRequests", "ApproveDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentRequests", "Approver", c => c.String());
            AddColumn("dbo.PaymentRequests", "Payor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentRequests", "Payor");
            DropColumn("dbo.PaymentRequests", "Approver");
            DropColumn("dbo.PaymentRequests", "ApproveDateTime");
            DropColumn("dbo.PaymentRequests", "Submitter");
        }
    }
}
