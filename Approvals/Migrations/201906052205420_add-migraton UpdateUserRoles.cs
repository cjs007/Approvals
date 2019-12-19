namespace Approvals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigratonUpdateUserRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsApprover", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsOwner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsOwner");
            DropColumn("dbo.AspNetUsers", "IsApprover");
            DropColumn("dbo.AspNetUsers", "IsAdmin");
        }
    }
}
