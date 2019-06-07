namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE MembershipTypes ADD Name nvarchar null");

        }
        
        public override void Down()
        {
        }
    }
}
