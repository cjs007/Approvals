namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update20190409 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Submitted = c.Boolean(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        GLCode = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Invoices");
        }
    }
}
