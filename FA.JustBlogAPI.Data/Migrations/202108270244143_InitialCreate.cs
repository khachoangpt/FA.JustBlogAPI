namespace FA.JustBlogAPI.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "common.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false),
                        Description = c.String(maxLength: 1024),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 255),
                        ShortDescription = c.String(maxLength: 1024),
                        ImageUrl = c.String(maxLength: 255),
                        PostContent = c.String(nullable: false),
                        UrlSlug = c.String(nullable: false),
                        Published = c.Boolean(nullable: false),
                        PostedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        Modified = c.DateTimeOffset(nullable: false, precision: 7),
                        ViewCount = c.Int(nullable: false),
                        RateCount = c.Int(nullable: false),
                        TotalRate = c.Int(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "common.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false),
                        CommentHeader = c.String(maxLength: 255),
                        CommentText = c.String(maxLength: 1024),
                        CommentTime = c.DateTimeOffset(nullable: false, precision: 7),
                        PostId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "common.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        UrlSlug = c.String(nullable: false),
                        Description = c.String(maxLength: 1024),
                        Count = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.PostTagMap",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        TagId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("common.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("common.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.PostTagMap", "TagId", "common.Tags");
            DropForeignKey("common.PostTagMap", "PostId", "common.Posts");
            DropForeignKey("common.Comments", "PostId", "common.Posts");
            DropForeignKey("common.Posts", "CategoryId", "common.Categories");
            DropIndex("common.PostTagMap", new[] { "TagId" });
            DropIndex("common.PostTagMap", new[] { "PostId" });
            DropIndex("common.Comments", new[] { "PostId" });
            DropIndex("common.Posts", new[] { "CategoryId" });
            DropTable("common.PostTagMap");
            DropTable("common.Tags");
            DropTable("common.Comments");
            DropTable("common.Posts");
            DropTable("common.Categories");
        }
    }
}
