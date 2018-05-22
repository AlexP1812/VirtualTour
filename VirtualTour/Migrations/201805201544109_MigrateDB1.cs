namespace VirtualTour.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Description = c.String(),
                        ImageId = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.ImageId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        ImagePath = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.ImageGalleries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageGalleries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Comments", "ImageId", "dbo.Images");
            DropIndex("dbo.Comments", new[] { "ImageId" });
            DropTable("dbo.Images");
            DropTable("dbo.Comments");
        }
    }
}
