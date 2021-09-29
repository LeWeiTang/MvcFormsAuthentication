namespace MvcFormsAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class account : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
