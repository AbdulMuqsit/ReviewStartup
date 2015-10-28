namespace ReviewStartup.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Text : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaPosts", "MarkText", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MediaPosts", "MarkText");
        }
    }
}
