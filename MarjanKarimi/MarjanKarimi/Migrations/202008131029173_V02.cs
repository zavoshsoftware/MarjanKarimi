namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        UrlParam = c.String(),
                        Order = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Galleries", "GalleryGroupId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Galleries", "GalleryGroupId");
            AddForeignKey("dbo.Galleries", "GalleryGroupId", "dbo.GalleryGroups", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Galleries", "GalleryGroupId", "dbo.GalleryGroups");
            DropIndex("dbo.Galleries", new[] { "GalleryGroupId" });
            DropColumn("dbo.Galleries", "GalleryGroupId");
            DropTable("dbo.GalleryGroups");
        }
    }
}
