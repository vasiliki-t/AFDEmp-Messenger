namespace Project_Mesenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MsgUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "isDeletedFromInbox", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "isDeletedFromOutbox", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "isDeletedFromOutbox");
            DropColumn("dbo.Messages", "isDeletedFromInbox");
        }
    }
}
