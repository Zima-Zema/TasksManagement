namespace TasksManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishIt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        UploadTime = c.DateTime(),
                        ViewFlag = c.Boolean(nullable: false),
                        User_key = c.String(maxLength: 128),
                        Type_key = c.Int(),
                        Task_Key = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedTasks", t => t.Task_Key, cascadeDelete: true)
                .ForeignKey("dbo.AttachmentTypes", t => t.Type_key)
                .ForeignKey("dbo.AspNetUsers", t => t.User_key)
                .Index(t => t.User_key)
                .Index(t => t.Type_key)
                .Index(t => t.Task_Key);
            
            CreateTable(
                "dbo.AssignedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Purpose = c.String(),
                        CreatedTime = c.DateTime(),
                        LastUpdate = c.DateTime(),
                        DeadTime = c.DateTime(),
                        ViewFlag = c.Boolean(nullable: false),
                        Owner_Key = c.String(maxLength: 128),
                        Creator_Key = c.String(maxLength: 128),
                        Status_Key = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Key)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Key)
                .ForeignKey("dbo.Status", t => t.Status_Key, cascadeDelete: true)
                .Index(t => t.Owner_Key)
                .Index(t => t.Creator_Key)
                .Index(t => t.Status_Key)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ViewFlag = c.Boolean(nullable: false),
                        Leader_Key = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Leader_Key)
                .Index(t => t.Leader_Key);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Level = c.Int(nullable: false),
                        Description = c.String(),
                        ViewFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AttachmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Description = c.String(),
                        ViewFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Team_Key", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "SecondName", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.AspNetUsers", "Team_Key");
            AddForeignKey("dbo.AspNetUsers", "Team_Key", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "User_key", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attachments", "Type_key", "dbo.AttachmentTypes");
            DropForeignKey("dbo.Attachments", "Task_Key", "dbo.AssignedTasks");
            DropForeignKey("dbo.AssignedTasks", "Status_Key", "dbo.Status");
            DropForeignKey("dbo.AssignedTasks", "Owner_Key", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignedTasks", "Creator_Key", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teams", "Leader_Key", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Team_Key", "dbo.Teams");
            DropForeignKey("dbo.AssignedTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Teams", new[] { "Leader_Key" });
            DropIndex("dbo.AspNetUsers", new[] { "Team_Key" });
            DropIndex("dbo.AssignedTasks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AssignedTasks", new[] { "Status_Key" });
            DropIndex("dbo.AssignedTasks", new[] { "Creator_Key" });
            DropIndex("dbo.AssignedTasks", new[] { "Owner_Key" });
            DropIndex("dbo.Attachments", new[] { "Task_Key" });
            DropIndex("dbo.Attachments", new[] { "Type_key" });
            DropIndex("dbo.Attachments", new[] { "User_key" });
            AlterColumn("dbo.AspNetUsers", "SecondName", c => c.String(maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 255));
            DropColumn("dbo.AspNetUsers", "Team_Key");
            DropTable("dbo.AttachmentTypes");
            DropTable("dbo.Status");
            DropTable("dbo.Teams");
            DropTable("dbo.AssignedTasks");
            DropTable("dbo.Attachments");
        }
    }
}
