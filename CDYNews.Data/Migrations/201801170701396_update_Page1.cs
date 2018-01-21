namespace CDYNews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Page1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pages", "ViewCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "ViewCount", c => c.Int(nullable: false));
        }
    }
}
