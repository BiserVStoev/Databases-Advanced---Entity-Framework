namespace MassDefect.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropCascadeThroughMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anomalies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginPlanet_Id = c.Int(),
                        TeleportPlanet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.OriginPlanet_Id)
                .ForeignKey("dbo.Planets", t => t.TeleportPlanet_Id)
                .Index(t => t.OriginPlanet_Id)
                .Index(t => t.TeleportPlanet_Id);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SolarSystem_Id = c.Int(nullable: false),
                        Sun_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SolarSystems", t => t.SolarSystem_Id, cascadeDelete: false)
                .ForeignKey("dbo.Stars", t => t.Sun_Id, cascadeDelete: false)
                .Index(t => t.SolarSystem_Id)
                .Index(t => t.Sun_Id);
            
            CreateTable(
                "dbo.SolarSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SolarSystem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SolarSystems", t => t.SolarSystem_Id, cascadeDelete: false)
                .Index(t => t.SolarSystem_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        HomePlanet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.HomePlanet_Id, cascadeDelete: false)
                .Index(t => t.HomePlanet_Id);
            
            CreateTable(
                "dbo.AnomalyVictims",
                c => new
                    {
                        AnomalyId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnomalyId, t.PersonId })
                .ForeignKey("dbo.Anomalies", t => t.AnomalyId, cascadeDelete: false)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: false)
                .Index(t => t.AnomalyId)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnomalyVictims", "PersonId", "dbo.People");
            DropForeignKey("dbo.AnomalyVictims", "AnomalyId", "dbo.Anomalies");
            DropForeignKey("dbo.People", "HomePlanet_Id", "dbo.Planets");
            DropForeignKey("dbo.Anomalies", "TeleportPlanet_Id", "dbo.Planets");
            DropForeignKey("dbo.Planets", "Sun_Id", "dbo.Stars");
            DropForeignKey("dbo.Stars", "SolarSystem_Id", "dbo.SolarSystems");
            DropForeignKey("dbo.Planets", "SolarSystem_Id", "dbo.SolarSystems");
            DropForeignKey("dbo.Anomalies", "OriginPlanet_Id", "dbo.Planets");
            DropIndex("dbo.AnomalyVictims", new[] { "PersonId" });
            DropIndex("dbo.AnomalyVictims", new[] { "AnomalyId" });
            DropIndex("dbo.People", new[] { "HomePlanet_Id" });
            DropIndex("dbo.Stars", new[] { "SolarSystem_Id" });
            DropIndex("dbo.Planets", new[] { "Sun_Id" });
            DropIndex("dbo.Planets", new[] { "SolarSystem_Id" });
            DropIndex("dbo.Anomalies", new[] { "TeleportPlanet_Id" });
            DropIndex("dbo.Anomalies", new[] { "OriginPlanet_Id" });
            DropTable("dbo.AnomalyVictims");
            DropTable("dbo.People");
            DropTable("dbo.Stars");
            DropTable("dbo.SolarSystems");
            DropTable("dbo.Planets");
            DropTable("dbo.Anomalies");
        }
    }
}
