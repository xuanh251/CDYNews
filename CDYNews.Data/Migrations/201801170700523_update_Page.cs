namespace CDYNews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Page : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "ViewCount");
        }
    }
}
