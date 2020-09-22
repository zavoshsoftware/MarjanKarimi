namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceGroups", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceGroups", "ImageUrl");
        }
    }
}
