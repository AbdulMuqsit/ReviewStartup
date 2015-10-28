namespace ReviewStartup.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Title", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Title");
        }
    }
}
