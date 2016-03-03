namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001_DDL_Add_AccountType_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountTypes");
        }
    }
}
