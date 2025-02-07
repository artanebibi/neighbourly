namespace Neighbourly_application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCanvasParticipant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Canvas", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.Participants", "Canvas_Id", "dbo.Canvas");
            DropIndex("dbo.Canvas", new[] { "Participant_Id" });
            DropIndex("dbo.Participants", new[] { "Canvas_Id" });
            CreateTable(
                "dbo.CanvasParticipants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipantId = c.Int(nullable: false),
                        CanvasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Canvas", t => t.CanvasId)
                .ForeignKey("dbo.Participants", t => t.ParticipantId)
                .Index(t => t.ParticipantId)
                .Index(t => t.CanvasId);
            
            DropColumn("dbo.Canvas", "Participant_Id");
            DropColumn("dbo.Participants", "Canvas_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Canvas_Id", c => c.Int());
            AddColumn("dbo.Canvas", "Participant_Id", c => c.Int());
            DropForeignKey("dbo.CanvasParticipants", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.CanvasParticipants", "CanvasId", "dbo.Canvas");
            DropIndex("dbo.CanvasParticipants", new[] { "CanvasId" });
            DropIndex("dbo.CanvasParticipants", new[] { "ParticipantId" });
            DropTable("dbo.CanvasParticipants");
            CreateIndex("dbo.Participants", "Canvas_Id");
            CreateIndex("dbo.Canvas", "Participant_Id");
            AddForeignKey("dbo.Participants", "Canvas_Id", "dbo.Canvas", "Id");
            AddForeignKey("dbo.Canvas", "Participant_Id", "dbo.Participants", "Id");
        }
    }
}
