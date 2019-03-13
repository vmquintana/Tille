namespace TilleWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        NumberOfClients = c.Int(nullable: false),
                        DateIn = c.DateTime(nullable: false),
                        DateOut = c.DateTime(nullable: false),
                        EstimatedPrice = c.Double(nullable: false),
                        MovementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movement", t => t.MovementId, cascadeDelete: true)
                .Index(t => t.MovementId);
            
            CreateTable(
                "dbo.Movement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMovs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
                        MovementId = c.Int(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movement", t => t.MovementId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.MovementId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Int(nullable: false)
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        MovementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movement", t => t.MovementId, cascadeDelete: true)
                .Index(t => t.MovementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Service", "MovementId", "dbo.Movement");
            DropForeignKey("dbo.ProductMovs", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductMovs", "MovementId", "dbo.Movement");
            DropForeignKey("dbo.Book", "MovementId", "dbo.Movement");
            DropIndex("dbo.Service", new[] { "MovementId" });
            DropIndex("dbo.ProductMovs", new[] { "ProductId" });
            DropIndex("dbo.ProductMovs", new[] { "MovementId" });
            DropIndex("dbo.Book", new[] { "MovementId" });
            DropTable("dbo.Service");
            DropTable("dbo.Product");
            DropTable("dbo.ProductMovs");
            DropTable("dbo.Movement");
            DropTable("dbo.Book");
        }
    }
}
