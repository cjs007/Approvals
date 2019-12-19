namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardCharges", "CardStatementId", "dbo.CardStatements");
            DropIndex("dbo.CardCharges", new[] { "CardStatementId" });
            DropTable("dbo.CardCharges");
            DropTable("dbo.CardStatements");
            DropTable("dbo.CardHolders");
            DropTable("dbo.Invoices");
        }
        
        public override void Down()
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
            
            CreateTable(
                "dbo.CardHolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CardNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardStatements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardCharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GLCode = c.String(),
                        CardStatementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CardCharges", "CardStatementId");
            AddForeignKey("dbo.CardCharges", "CardStatementId", "dbo.CardStatements", "Id", cascadeDelete: true);
        }
    }
}
