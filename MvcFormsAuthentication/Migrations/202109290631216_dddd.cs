namespace MvcFormsAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dddd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String());
            AlterColumn("dbo.Accounts", "Name", c => c.String());
        }
    }
}
