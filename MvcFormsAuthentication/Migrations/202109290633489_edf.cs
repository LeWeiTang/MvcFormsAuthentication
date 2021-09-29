namespace MvcFormsAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Email", c => c.String());
        }
    }
}
