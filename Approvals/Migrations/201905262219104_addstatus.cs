namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequests", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentRequests", "Status");
        }
    }
}
