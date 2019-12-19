namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentRequests", "AddressOne", c => c.String(nullable: false));
            AlterColumn("dbo.PaymentRequests", "AddressTwo", c => c.String(nullable: false));
            AlterColumn("dbo.PaymentRequests", "Amount", c => c.String(nullable: false));
            AlterColumn("dbo.PaymentRequests", "GLLine", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentRequests", "GLLine", c => c.String());
            AlterColumn("dbo.PaymentRequests", "Amount", c => c.String());
            AlterColumn("dbo.PaymentRequests", "AddressTwo", c => c.String());
            AlterColumn("dbo.PaymentRequests", "AddressOne", c => c.String());
        }
    }
}
