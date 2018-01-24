namespace CDYNews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "UserInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "UserInfo");
        }
    }
}
