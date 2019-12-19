namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CardModelUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardCharges", "CardHolder_Id", "dbo.CardHolders");
            DropForeignKey("dbo.CardCharges", "CardStatement_Id", "dbo.CardStatements");
            DropIndex("dbo.CardCharges", new[] { "CardHolder_Id" });
            DropIndex("dbo.CardCharges", new[] { "CardStatement_Id" });
            RenameColumn(table: "dbo.CardCharges", name: "CardHolder_Id", newName: "CardHolderId");
            RenameColumn(table: "dbo.CardCharges", name: "CardStatement_Id", newName: "CardStatementId");
            AlterColumn("dbo.CardCharges", "CardHolderId", c => c.Int(nullable: false));
            AlterColumn("dbo.CardCharges", "CardStatementId", c => c.Int(nullable: false));
            CreateIndex("dbo.CardCharges", "CardHolderId");
            CreateIndex("dbo.CardCharges", "CardStatementId");
            AddForeignKey("dbo.CardCharges", "CardHolderId", "dbo.CardHolders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CardCharges", "CardStatementId", "dbo.CardStatements", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardCharges", "CardStatementId", "dbo.CardStatements");
            DropForeignKey("dbo.CardCharges", "CardHolderId", "dbo.CardHolders");
            DropIndex("dbo.CardCharges", new[] { "CardStatementId" });
            DropIndex("dbo.CardCharges", new[] { "CardHolderId" });
            AlterColumn("dbo.CardCharges", "CardStatementId", c => c.Int());
            AlterColumn("dbo.CardCharges", "CardHolderId", c => c.Int());
            RenameColumn(table: "dbo.CardCharges", name: "CardStatementId", newName: "CardStatement_Id");
            RenameColumn(table: "dbo.CardCharges", name: "CardHolderId", newName: "CardHolder_Id");
            CreateIndex("dbo.CardCharges", "CardStatement_Id");
            CreateIndex("dbo.CardCharges", "CardHolder_Id");
            AddForeignKey("dbo.CardCharges", "CardStatement_Id", "dbo.CardStatements", "Id");
            AddForeignKey("dbo.CardCharges", "CardHolder_Id", "dbo.CardHolders", "Id");
        }
    }
}
