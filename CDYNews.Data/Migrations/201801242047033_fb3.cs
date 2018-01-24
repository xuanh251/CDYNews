namespace CDYNews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fb3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "UserInfo", c => c.String());
            DropColumn("dbo.Feedbacks", "ClientIp");
            DropColumn("dbo.Feedbacks", "ClientLat");
            DropColumn("dbo.Feedbacks", "ClientLng");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "ClientLng", c => c.String());
            AddColumn("dbo.Feedbacks", "ClientLat", c => c.String());
            AddColumn("dbo.Feedbacks", "ClientIp", c => c.String());
            DropColumn("dbo.Feedbacks", "UserInfo");
        }
    }
}
