namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _00 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "ProductTest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductTest", c => c.String());
        }
    }
}
