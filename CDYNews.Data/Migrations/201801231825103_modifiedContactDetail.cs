namespace CDYNews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedContactDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactDetails", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactDetails", "Website");
        }
    }
}
