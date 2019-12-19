namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentRequests", "ApproveDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentRequests", "ApproveDateTime", c => c.DateTime(nullable: false));
        }
    }
}
