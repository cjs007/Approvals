namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AddressOne = c.String(),
                        AddressTwo = c.String(),
                        AddressThree = c.String(),
                        Amount = c.String(),
                        GLLine = c.String(),
                        Approved = c.Boolean(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        SubmitDateTime = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        Client = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentRequests");
        }
    }
}
