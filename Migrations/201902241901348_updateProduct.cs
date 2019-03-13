namespace TilleWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "AmountBoughtCurrentMonth", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "CurrentMonthCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "CurrentMonthCost");
            DropColumn("dbo.Product", "AmountBoughtCurrentMonth");
        }
    }
}
