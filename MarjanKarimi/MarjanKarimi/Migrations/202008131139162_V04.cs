namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogComments", "Response", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Blogs", "Response");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "Response", c => c.String(storeType: "ntext"));
            DropColumn("dbo.BlogComments", "Response");
        }
    }
}
