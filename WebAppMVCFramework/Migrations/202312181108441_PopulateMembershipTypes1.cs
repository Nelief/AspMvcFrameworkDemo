namespace WebAppMVCFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Daily' WHERE id=1");
            Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE id=2");
            Sql("UPDATE MembershipTypes SET Name = 'Tri-Monthly' WHERE id=3");
            Sql("UPDATE MembershipTypes SET Name = 'Yearly' WHERE id=4");
        }

        public override void Down()
        {
        }
    }
}
