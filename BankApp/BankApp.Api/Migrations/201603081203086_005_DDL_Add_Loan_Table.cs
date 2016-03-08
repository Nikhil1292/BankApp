namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _005_DDL_Add_Loan_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        EMIs = c.Int(nullable: false),
                        InterestTypeId = c.Int(nullable: false),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsBilled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        MetaData = c.String(storeType: "xml"),
                        InterestTypes_Code = c.Int(),
                        Users_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.LoanId)
                .ForeignKey("dbo.InterestTypes", t => t.InterestTypes_Code)
                .ForeignKey("dbo.Users", t => t.Users_UserId)
                .Index(t => t.InterestTypes_Code)
                .Index(t => t.Users_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "Users_UserId", "dbo.Users");
            DropForeignKey("dbo.Loans", "InterestTypes_Code", "dbo.InterestTypes");
            DropIndex("dbo.Loans", new[] { "Users_UserId" });
            DropIndex("dbo.Loans", new[] { "InterestTypes_Code" });
            DropTable("dbo.Loans");
        }
    }
}
