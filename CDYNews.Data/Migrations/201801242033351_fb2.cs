namespace CDYNews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fb2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "ClientIp", c => c.String());
            AddColumn("dbo.Feedbacks", "ClientLat", c => c.String());
            AddColumn("dbo.Feedbacks", "ClientLng", c => c.String());
            DropColumn("dbo.Feedbacks", "UserInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "UserInfo", c => c.String());
            DropColumn("dbo.Feedbacks", "ClientLng");
            DropColumn("dbo.Feedbacks", "ClientLat");
            DropColumn("dbo.Feedbacks", "ClientIp");
        }
    }
}
