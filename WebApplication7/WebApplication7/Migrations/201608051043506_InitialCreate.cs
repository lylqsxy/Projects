namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerAddr = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductPrice = c.Double(nullable: false),
                        ProductDes = c.String(),
                        ProductCat = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Order", new[] { "ProductID" });
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
        }
    }
}
