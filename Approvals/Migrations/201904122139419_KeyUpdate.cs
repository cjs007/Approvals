namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardCharges", "CardHolderId", "dbo.CardHolders");
            DropIndex("dbo.CardCharges", new[] { "CardHolderId" });
            DropColumn("dbo.CardCharges", "CardHolderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardCharges", "CardHolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.CardCharges", "CardHolderId");
            AddForeignKey("dbo.CardCharges", "CardHolderId", "dbo.CardHolders", "Id", cascadeDelete: true);
        }
    }
}
