namespace MvcFormsAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepasswordLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false, maxLength: 1280));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
