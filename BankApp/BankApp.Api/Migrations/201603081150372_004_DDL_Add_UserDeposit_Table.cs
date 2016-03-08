namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _004_DDL_Add_UserDeposit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDeposits",
                c => new
                    {
                        UseDepositId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        MetaData = c.String(storeType: "xml"),
                        Users_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UseDepositId)
                .ForeignKey("dbo.Users", t => t.Users_UserId)
                .Index(t => t.Users_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDeposits", "Users_UserId", "dbo.Users");
            DropIndex("dbo.UserDeposits", new[] { "Users_UserId" });
            DropTable("dbo.UserDeposits");
        }
    }
}
