namespace WebAppMVCFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotationsCustomer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Cognome", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Cognome", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
        }
    }
}
