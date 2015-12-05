namespace ReviewStartup.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        Sender = c.String(nullable: false, maxLength: 128),
                        Reciever = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Sender, t.Reciever })
                .ForeignKey("dbo.AspNetUsers", t => t.Sender)
                .ForeignKey("dbo.AspNetUsers", t => t.Reciever)
                .Index(t => t.Sender)
                .Index(t => t.Reciever);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Initiater = c.String(nullable: false, maxLength: 128),
                        Acceptor = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Initiater, t.Acceptor })
                .ForeignKey("dbo.AspNetUsers", t => t.Initiater)
                .ForeignKey("dbo.AspNetUsers", t => t.Acceptor)
                .Index(t => t.Initiater)
                .Index(t => t.Acceptor);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friends", "Acceptor", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Initiater", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "Reciever", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "Sender", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "Acceptor" });
            DropIndex("dbo.Friends", new[] { "Initiater" });
            DropIndex("dbo.FriendRequests", new[] { "Reciever" });
            DropIndex("dbo.FriendRequests", new[] { "Sender" });
            DropTable("dbo.Friends");
            DropTable("dbo.FriendRequests");
        }
    }
}
