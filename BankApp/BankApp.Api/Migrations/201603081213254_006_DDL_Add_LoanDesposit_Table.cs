namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _006_DDL_Add_LoanDesposit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanDeposits",
                c => new
                    {
                        LoanDepositId = c.Long(nullable: false, identity: true),
                        LoanId = c.Long(nullable: false),
                        EMI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        MetaData = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.LoanDepositId)
                .ForeignKey("dbo.Loans", t => t.LoanId, cascadeDelete: false)
                .Index(t => t.LoanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanDeposits", "LoanId", "dbo.Loans");
            DropIndex("dbo.LoanDeposits", new[] { "LoanId" });
            DropTable("dbo.LoanDeposits");
        }
    }
}
