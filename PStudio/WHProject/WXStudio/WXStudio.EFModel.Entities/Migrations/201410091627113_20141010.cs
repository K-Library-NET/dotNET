namespace WXStudio.EFModel.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20141010 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.tb_sale_salesman", "Column4", c => c.String());
            //AddColumn("dbo.tb_sale_salesman", "Column5", c => c.String());
            //AddColumn("dbo.tb_sale_salesmanscore", "Column4", c => c.String());
            //AddColumn("dbo.tb_sale_salesmanscore", "Column5", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.tb_sale_salesmanscore", "Column5");
            //DropColumn("dbo.tb_sale_salesmanscore", "Column4");
            //DropColumn("dbo.tb_sale_salesman", "Column5");
            //DropColumn("dbo.tb_sale_salesman", "Column4");
        }
    }
}
