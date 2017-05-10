namespace TasksManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "SecondName", c => c.String(maxLength: 255));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "SecondName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
