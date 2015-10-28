namespace ReviewStartup.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewAverage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaPosts", "Thumbnail", c => c.Binary());
            AddColumn("dbo.MediaPosts", "AverageScore", c => c.Double(nullable: false));
            AlterColumn("dbo.Reviews", "Ratings", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Ratings", c => c.Int(nullable: false));
            DropColumn("dbo.MediaPosts", "AverageScore");
            DropColumn("dbo.MediaPosts", "Thumbnail");
        }
    }
}
