namespace WebAppMVCFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedMovies : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
        }
    }
}
