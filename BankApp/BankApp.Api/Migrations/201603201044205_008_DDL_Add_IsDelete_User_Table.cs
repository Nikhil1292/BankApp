namespace BankApp.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _008_DDL_Add_IsDelete_User_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsDeleted");
        }
    }
}
