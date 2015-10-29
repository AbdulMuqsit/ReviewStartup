namespace ReviewStartup.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaPostChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MediaPosts", "Ratings");
            DropColumn("dbo.MediaPosts", "MarkText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MediaPosts", "MarkText", c => c.String(maxLength: 150));
            AddColumn("dbo.MediaPosts", "Ratings", c => c.Double());
        }
    }
}
