namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductTest", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductTest");
        }
    }
}
