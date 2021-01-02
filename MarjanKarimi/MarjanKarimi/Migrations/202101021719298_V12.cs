namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogComments", "ResponseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogComments", "ResponseDate");
        }
    }
}
