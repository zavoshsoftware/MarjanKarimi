namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(),
                        Name = c.String(),
                        Email = c.String(),
                        Question = c.String(),
                        Answer = c.String(),
                        AnswerDate = c.DateTime(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.ServiceFaqs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(),
                        Question = c.String(),
                        Answer = c.String(),
                        ServiceId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.ServiceTopics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(),
                        Title = c.String(),
                        ServiceId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            AddColumn("dbo.Blogs", "ServiceId", c => c.Guid());
            AddColumn("dbo.Services", "GroupAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Services", "SingleAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Blogs", "ServiceId");
            AddForeignKey("dbo.Blogs", "ServiceId", "dbo.Services", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceTopics", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceFaqs", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceComments", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Blogs", "ServiceId", "dbo.Services");
            DropIndex("dbo.ServiceTopics", new[] { "ServiceId" });
            DropIndex("dbo.ServiceFaqs", new[] { "ServiceId" });
            DropIndex("dbo.ServiceComments", new[] { "ServiceId" });
            DropIndex("dbo.Blogs", new[] { "ServiceId" });
            DropColumn("dbo.Services", "SingleAmount");
            DropColumn("dbo.Services", "GroupAmount");
            DropColumn("dbo.Blogs", "ServiceId");
            DropTable("dbo.ServiceTopics");
            DropTable("dbo.ServiceFaqs");
            DropTable("dbo.ServiceComments");
        }
    }
}
