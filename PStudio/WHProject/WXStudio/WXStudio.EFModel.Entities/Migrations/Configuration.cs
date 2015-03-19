namespace WXStudio.EFModel.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<WXStudio.EFModel.Entities.WXPstudioDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            var migrator = new DbMigrator(this);
            migrator.Update("201410091627113_20141010");
        }

        protected override void Seed(WXStudio.EFModel.Entities.WXPstudioDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    public partial class CodeFirstMigration : DbMigration
    {
        //public override void Down()
        //{
        //    base.Down();
        //}

        //public override void Up()
        //{
        //    this.AddColumn("tb_sale_book", "Column4", c => c.String());
        //}
    }
}
