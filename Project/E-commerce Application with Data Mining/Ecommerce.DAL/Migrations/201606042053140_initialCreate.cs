namespace Ecommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boutique",
                c => new
                    {
                        Boutique_Id = c.Int(nullable: false, identity: true),
                        Boutique_Description = c.String(nullable: false, maxLength: 100),
                        Boutique_Stock = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Boutique_Id);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.Int(nullable: false),
                        Size_Id = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Size", t => t.Size_Id, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Product_Id = c.Int(nullable: false, identity: true),
                        Product_Description = c.String(nullable: false, maxLength: 100),
                        Product_Cost = c.Decimal(nullable: false, storeType: "money"),
                        Product_Warning = c.String(nullable: false, maxLength: 100),
                        Product_SubCategory_Id = c.Int(nullable: false),
                        Product_Boutique_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_Id)
                .ForeignKey("dbo.Boutique", t => t.Product_Boutique_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubCategory", t => t.Product_SubCategory_Id, cascadeDelete: true)
                .Index(t => t.Product_SubCategory_Id)
                .Index(t => t.Product_Boutique_Id);
            
            CreateTable(
                "dbo.Color",
                c => new
                    {
                        Color_Id = c.Int(nullable: false, identity: true),
                        Color_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Color_Id);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Image_Id = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        FileName = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Image_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Size",
                c => new
                    {
                        Size_Id = c.Int(nullable: false, identity: true),
                        Size_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Size_Id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        NumberOfStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Size", t => t.Size_Id, cascadeDelete: true)
                .Index(t => t.Size_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        SubCategory_Id = c.Int(nullable: false, identity: true),
                        SubCategory_Name = c.String(nullable: false, maxLength: 50),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategory_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Category_Id = c.Int(nullable: false, identity: true),
                        Category_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Category_Id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.County",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.City_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Customer_Id = c.Int(nullable: false, identity: true),
                        Customer_Name = c.String(nullable: false, maxLength: 50),
                        Customer_Surname = c.String(nullable: false, maxLength: 50),
                        Customer_Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Customer_Password = c.String(nullable: false, maxLength: 250),
                        Customer_Gender = c.Boolean(nullable: false),
                        Customer_BirthDate = c.DateTime(nullable: false),
                        Customer_Contract = c.Boolean(nullable: false),
                        Customer_County_Id = c.Int(nullable: false),
                        ActivationCode = c.String(maxLength: 50),
                        Customer_Address = c.String(),
                        CustomerTypeOfCustomerId = c.Int(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Customer_Id)
                .ForeignKey("dbo.County", t => t.Customer_County_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfCustomer", t => t.CustomerTypeOfCustomerId, cascadeDelete: true)
                .Index(t => t.Customer_County_Id)
                .Index(t => t.CustomerTypeOfCustomerId);
            
            CreateTable(
                "dbo.TypeOfCustomer",
                c => new
                    {
                        TypeOfCustomer_Id = c.Int(nullable: false, identity: true),
                        TypeOfCustomer_Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TypeOfCustomer_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        CreditCard = c.String(),
                        Email = c.String(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Size_Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Size", t => t.Size_Id, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Customer_Name = c.String(),
                        Customer_Surname = c.String(),
                        Customer_Gender = c.Boolean(nullable: false),
                        Customer_BirthDate = c.DateTime(nullable: false),
                        Customer_Address = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ColorProducts",
                c => new
                    {
                        Product_Product_Id = c.Int(nullable: false),
                        Color_Color_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Product_Id, t.Color_Color_Id })
                .ForeignKey("dbo.Color", t => t.Product_Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Color_Color_Id, cascadeDelete: true)
                .Index(t => t.Product_Product_Id)
                .Index(t => t.Color_Color_Id);
            
            CreateTable(
                "dbo.SizeProducts",
                c => new
                    {
                        Product_Product_Id = c.Int(nullable: false),
                        Size_Size_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Product_Id, t.Size_Size_Id })
                .ForeignKey("dbo.Size", t => t.Product_Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Size_Size_Id, cascadeDelete: true)
                .Index(t => t.Product_Product_Id)
                .Index(t => t.Size_Size_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetail", "Size_Id", "dbo.Size");
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Customer", "CustomerTypeOfCustomerId", "dbo.TypeOfCustomer");
            DropForeignKey("dbo.Customer", "Customer_County_Id", "dbo.County");
            DropForeignKey("dbo.County", "City_Id", "dbo.City");
            DropForeignKey("dbo.Cart", "Size_Id", "dbo.Size");
            DropForeignKey("dbo.Cart", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "Product_SubCategory_Id", "dbo.SubCategory");
            DropForeignKey("dbo.SubCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Stock", "Size_Id", "dbo.Size");
            DropForeignKey("dbo.Stock", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.SizeProducts", "Size_Size_Id", "dbo.Product");
            DropForeignKey("dbo.SizeProducts", "Product_Product_Id", "dbo.Size");
            DropForeignKey("dbo.Product", "Product_Boutique_Id", "dbo.Boutique");
            DropForeignKey("dbo.Image", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ColorProducts", "Color_Color_Id", "dbo.Product");
            DropForeignKey("dbo.ColorProducts", "Product_Product_Id", "dbo.Color");
            DropIndex("dbo.SizeProducts", new[] { "Size_Size_Id" });
            DropIndex("dbo.SizeProducts", new[] { "Product_Product_Id" });
            DropIndex("dbo.ColorProducts", new[] { "Color_Color_Id" });
            DropIndex("dbo.ColorProducts", new[] { "Product_Product_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderDetail", new[] { "Size_Id" });
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.Customer", new[] { "CustomerTypeOfCustomerId" });
            DropIndex("dbo.Customer", new[] { "Customer_County_Id" });
            DropIndex("dbo.County", new[] { "City_Id" });
            DropIndex("dbo.SubCategory", new[] { "Category_Id" });
            DropIndex("dbo.Stock", new[] { "Product_Id" });
            DropIndex("dbo.Stock", new[] { "Size_Id" });
            DropIndex("dbo.Image", new[] { "Product_Id" });
            DropIndex("dbo.Product", new[] { "Product_Boutique_Id" });
            DropIndex("dbo.Product", new[] { "Product_SubCategory_Id" });
            DropIndex("dbo.Cart", new[] { "Size_Id" });
            DropIndex("dbo.Cart", new[] { "ProductId" });
            DropTable("dbo.SizeProducts");
            DropTable("dbo.ColorProducts");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.TypeOfCustomer");
            DropTable("dbo.Customer");
            DropTable("dbo.County");
            DropTable("dbo.City");
            DropTable("dbo.Category");
            DropTable("dbo.SubCategory");
            DropTable("dbo.Stock");
            DropTable("dbo.Size");
            DropTable("dbo.Image");
            DropTable("dbo.Color");
            DropTable("dbo.Product");
            DropTable("dbo.Cart");
            DropTable("dbo.Boutique");
        }
    }
}
