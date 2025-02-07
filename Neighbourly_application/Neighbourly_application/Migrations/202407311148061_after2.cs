namespace Neighbourly_application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class after2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Canvas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        TeamLeaderId = c.Int(nullable: false),
                        Participant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.Participant_Id)
                .ForeignKey("dbo.Participants", t => t.TeamLeaderId, cascadeDelete: true)
                .Index(t => t.TeamLeaderId)
                .Index(t => t.Participant_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CanvasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Canvas", t => t.CanvasId, cascadeDelete: true)
                .Index(t => t.CanvasId);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CanvasId = c.Int(nullable: false),
                        pId = c.Int(nullable: false),
                        CreatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Canvas", t => t.CanvasId, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.CreatedBy_Id)
                .Index(t => t.CanvasId)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.ForumComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        tId = c.Int(nullable: false),
                        ParentCommentId = c.Int(),
                        commentCreatedById = c.Int(nullable: false),
                        Thread_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.commentCreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Fora", t => t.Thread_Id)
                .Index(t => t.commentCreatedById)
                .Index(t => t.Thread_Id);
            
            CreateTable(
                "dbo.KanbanTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        DueDate = c.DateTime(),
                        Position = c.Int(nullable: false),
                        KanbanColumnId = c.Int(nullable: false),
                        AssignedUserId = c.Int(),
                        AssignedParticipant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.AssignedParticipant_Id)
                .ForeignKey("dbo.KanbanColumns", t => t.KanbanColumnId, cascadeDelete: true)
                .Index(t => t.KanbanColumnId)
                .Index(t => t.AssignedParticipant_Id);
            
            CreateTable(
                "dbo.KanbanColumns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.Int(nullable: false),
                        KanbanBoardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KanbanBoards", t => t.KanbanBoardId, cascadeDelete: true)
                .Index(t => t.KanbanBoardId);
            
            CreateTable(
                "dbo.KanbanBoards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CanvasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Canvas", t => t.CanvasId, cascadeDelete: true)
                .Index(t => t.CanvasId);
            
            AddColumn("dbo.Participants", "UserName", c => c.String());
            AddColumn("dbo.Participants", "Email", c => c.String());
            AddColumn("dbo.Participants", "Canvas_Id", c => c.Int());
            AlterColumn("dbo.Participants", "Name", c => c.String());
            CreateIndex("dbo.Participants", "Canvas_Id");
            AddForeignKey("dbo.Participants", "Canvas_Id", "dbo.Canvas", "Id");
            DropColumn("dbo.Participants", "Surname");
            DropColumn("dbo.Participants", "Age");
            DropColumn("dbo.Participants", "userEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "userEmail", c => c.String());
            AddColumn("dbo.Participants", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Participants", "Surname", c => c.String(nullable: false));
            DropForeignKey("dbo.Canvas", "TeamLeaderId", "dbo.Participants");
            DropForeignKey("dbo.Participants", "Canvas_Id", "dbo.Canvas");
            DropForeignKey("dbo.ForumComments", "Thread_Id", "dbo.Fora");
            DropForeignKey("dbo.Fora", "CreatedBy_Id", "dbo.Participants");
            DropForeignKey("dbo.ForumComments", "commentCreatedById", "dbo.Participants");
            DropForeignKey("dbo.Canvas", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.KanbanTasks", "KanbanColumnId", "dbo.KanbanColumns");
            DropForeignKey("dbo.KanbanColumns", "KanbanBoardId", "dbo.KanbanBoards");
            DropForeignKey("dbo.KanbanBoards", "CanvasId", "dbo.Canvas");
            DropForeignKey("dbo.KanbanTasks", "AssignedParticipant_Id", "dbo.Participants");
            DropForeignKey("dbo.Fora", "CanvasId", "dbo.Canvas");
            DropForeignKey("dbo.Events", "CanvasId", "dbo.Canvas");
            DropIndex("dbo.KanbanBoards", new[] { "CanvasId" });
            DropIndex("dbo.KanbanColumns", new[] { "KanbanBoardId" });
            DropIndex("dbo.KanbanTasks", new[] { "AssignedParticipant_Id" });
            DropIndex("dbo.KanbanTasks", new[] { "KanbanColumnId" });
            DropIndex("dbo.Participants", new[] { "Canvas_Id" });
            DropIndex("dbo.ForumComments", new[] { "Thread_Id" });
            DropIndex("dbo.ForumComments", new[] { "commentCreatedById" });
            DropIndex("dbo.Fora", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Fora", new[] { "CanvasId" });
            DropIndex("dbo.Events", new[] { "CanvasId" });
            DropIndex("dbo.Canvas", new[] { "Participant_Id" });
            DropIndex("dbo.Canvas", new[] { "TeamLeaderId" });
            AlterColumn("dbo.Participants", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Participants", "Canvas_Id");
            DropColumn("dbo.Participants", "Email");
            DropColumn("dbo.Participants", "UserName");
            DropTable("dbo.KanbanBoards");
            DropTable("dbo.KanbanColumns");
            DropTable("dbo.KanbanTasks");
            DropTable("dbo.ForumComments");
            DropTable("dbo.Fora");
            DropTable("dbo.Events");
            DropTable("dbo.Canvas");
        }
    }
}
