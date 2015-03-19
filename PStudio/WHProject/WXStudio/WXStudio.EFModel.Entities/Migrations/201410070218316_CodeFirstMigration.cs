namespace WXStudio.EFModel.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CodeFirstMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.tb_core_company",
            //    c => new
            //        {
            //            CoreCompanyId = c.Int(nullable: false, identity: true),
            //            ParentCoreCompanyId = c.Int(nullable: false),
            //            Name = c.String(),
            //            Comments = c.String(),
            //        })
            //    .PrimaryKey(t => t.CoreCompanyId);

            //CreateTable(
            //    "dbo.tb_core_config",
            //    c => new
            //        {
            //            CoreConfigId = c.Int(nullable: false, identity: true),
            //            Key = c.String(nullable: false),
            //            Value = c.String(nullable: false),
            //            Comments = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CoreConfigId);

            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);

            //CreateTable(
            //    "dbo.tb_sale_book",
            //    c => new
            //        {
            //            SaleBookId = c.Int(nullable: false, identity: true),
            //            SalesmanId = c.Int(nullable: false),
            //            WXCode = c.String(),
            //            WXNickname = c.String(),
            //            Name = c.String(nullable: false),
            //            Phone = c.String(nullable: false),
            //            Comments = c.String(),
            //            ViewConfirm = c.Int(nullable: false),
            //            BuyConfirm = c.Int(nullable: false),
            //            BuyUnit = c.String(),
            //            CreateDateTimeStamp = c.Long(),
            //            Column1 = c.String(nullable: false),
            //            Column2 = c.String(nullable: false),
            //            Column3 = c.String(nullable: false),
            //            BookDateTimeStamp = c.Long(),
            //            Column4 = c.String(),
            //            Column5 = c.String(),
            //            CompanyId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.SaleBookId);

            //CreateTable(
            //    "dbo.tb_sale_salesman",
            //    c => new
            //        {
            //            SalesmanId = c.Int(nullable: false, identity: true),
            //            WXCode = c.String(),
            //            WXNickname = c.String(),
            //            Name = c.String(),
            //            Phone = c.String(nullable: false),
            //            Company = c.String(),
            //            Address = c.String(),
            //            Comments = c.String(),
            //            CreateDateTimeStamp = c.Long(),
            //            Column1 = c.String(),
            //            Column2 = c.String(),
            //            Column3 = c.String(),
            //            CompanyID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.SalesmanId);

            //CreateTable(
            //    "dbo.tb_sale_salesmanscore",
            //    c => new
            //        {
            //            SalesmanScoreId = c.Int(nullable: false, identity: true),
            //            SaleBookId = c.Int(nullable: false),
            //            SalesmanId = c.Int(nullable: false),
            //            ScoreValue = c.Single(nullable: false),
            //            Comments = c.String(),
            //            Column1 = c.String(),
            //            Column2 = c.String(),
            //            Column3 = c.String(),
            //            CreateDateTimeStamp = c.Long(),
            //            CompanyID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SalesmanScoreId);

            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            CoreAccountId = c.Int(nullable: false),
            //            CompanyId = c.Int(nullable: false),
            //            Nickname = c.String(),
            //            Account = c.String(),
            //            Password = c.String(),
            //            Comments = c.String(),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);

            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);

            this.AddColumn("tb_sale_book", "Column4", c => c.String());
            this.AddColumn("tb_sale_book", "Column5", c => c.String());
            this.AddColumn("tb_sale_book", "BookDateTimeStamp", c => c.Long());

            this.AddColumn("tb_sale_salesman", "Column4", c => c.String());
            this.AddColumn("tb_sale_salesman", "Column5", c => c.String());

            this.AddColumn("tb_sale_salesmanscore", "Column4", c => c.String());
            this.AddColumn("tb_sale_salesmanscore", "Column5", c => c.String());
        }

        public override void Down()
        {
            this.DropColumn("tb_sale_book", "Column4");
            this.DropColumn("tb_sale_book", "Column5");
            this.DropColumn("tb_sale_book", "BookDateTimeStamp");

            this.DropColumn("tb_sale_salesman", "Column4");
            this.DropColumn("tb_sale_salesman", "Column5");

            this.DropColumn("tb_sale_salesmanscore", "Column4");
            this.DropColumn("tb_sale_salesmanscore", "Column5");

            //DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            //DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            //DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            //DropIndex("dbo.AspNetUsers", "UserNameIndex");
            //DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            //DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            //DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            //DropTable("dbo.AspNetUserLogins");
            //DropTable("dbo.AspNetUserClaims");
            //DropTable("dbo.AspNetUsers");
            //DropTable("dbo.tb_sale_salesmanscore");
            //DropTable("dbo.tb_sale_salesman");
            //DropTable("dbo.tb_sale_book");
            //DropTable("dbo.AspNetUserRoles");
            //DropTable("dbo.AspNetRoles");
            //DropTable("dbo.tb_core_config");
            //DropTable("dbo.tb_core_company");
        }
    }
}
