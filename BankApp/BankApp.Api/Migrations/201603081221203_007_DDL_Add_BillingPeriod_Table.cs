namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _007_DDL_Add_BillingPeriod_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingPeriods",
                c => new
                    {
                        BillingPeriodId = c.Long(nullable: false, identity: true),
                        LoanId = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EMI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        MetaData = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.BillingPeriodId)
                .ForeignKey("dbo.Loans", t => t.LoanId, cascadeDelete: false)
                .Index(t => t.LoanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillingPeriods", "LoanId", "dbo.Loans");
            DropIndex("dbo.BillingPeriods", new[] { "LoanId" });
            DropTable("dbo.BillingPeriods");
        }
    }
}
