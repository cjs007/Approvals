namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentRequests", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentRequests", "Name", c => c.String());
        }
    }
}
