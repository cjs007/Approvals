namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCardModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardCharges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GLCode = c.String(),
                        CardHolder_Id = c.Int(),
                        CardStatement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardHolders", t => t.CardHolder_Id)
                .ForeignKey("dbo.CardStatements", t => t.CardStatement_Id)
                .Index(t => t.CardHolder_Id)
                .Index(t => t.CardStatement_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardCharges", "CardStatement_Id", "dbo.CardStatements");
            DropForeignKey("dbo.CardCharges", "CardHolder_Id", "dbo.CardHolders");
            DropIndex("dbo.CardCharges", new[] { "CardStatement_Id" });
            DropIndex("dbo.CardCharges", new[] { "CardHolder_Id" });
            DropTable("dbo.CardStatements");
            DropTable("dbo.CardHolders");
            DropTable("dbo.CardCharges");
        }
    }
}
