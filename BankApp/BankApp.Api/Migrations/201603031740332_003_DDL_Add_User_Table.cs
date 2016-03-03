namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003_DDL_Add_User_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        IsLoanUser = c.Boolean(nullable: false),
                        IsBankUser = c.Boolean(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                        Address = c.String(),
                        PinCode = c.String(),
                        City = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        MetaData = c.String(storeType: "xml"),
                        AccountType_Code = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId)
                .Index(t => t.AccountTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.Users", new[] { "AccountTypeId" });
            DropTable("dbo.Users");
        }
    }
}
