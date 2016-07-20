namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncentiveGroup",
                c => new
                    {
                        IncentiveGroupId = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        StartDateTime = c.DateTime(),
                        EndDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.IncentiveGroupId)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: false)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        SiteId = c.String(nullable: false, maxLength: 3),
                        City = c.String(nullable: false, maxLength: 100),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.State", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Abbreviation = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.StateId)
                .Index(t => t.Abbreviation, unique: true)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Vin = c.String(maxLength: 100),
                        Model = c.String(maxLength: 100),
                        Year = c.Int(nullable: false),
                        Miles = c.Int(nullable: false),
                        Color = c.String(maxLength: 100),
                        LocationId = c.Int(nullable: false),
                        RentToOwn = c.Boolean(nullable: false),
                        Make = c.String(nullable: false, maxLength: 100),
                        MediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: false)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: false)
                .Index(t => t.LocationId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        FileName = c.String(nullable: false, maxLength: 100),
                        File = c.Binary(),
                    })
                .PrimaryKey(t => t.MediaId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        HashOfPassword = c.String(maxLength: 32),
                        UserAdministrator = c.Boolean(nullable: false),
                        FleetAdministrator = c.Boolean(nullable: false),
                        BrandingAdministrator = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncentiveGroup", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Vehicle", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Vehicle", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Location", "StateId", "dbo.State");
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.User", new[] { "Username" });
            DropIndex("dbo.Vehicle", new[] { "MediaId" });
            DropIndex("dbo.Vehicle", new[] { "LocationId" });
            DropIndex("dbo.State", new[] { "Name" });
            DropIndex("dbo.State", new[] { "Abbreviation" });
            DropIndex("dbo.Location", new[] { "StateId" });
            DropIndex("dbo.IncentiveGroup", new[] { "LocationId" });
            DropTable("dbo.User");
            DropTable("dbo.Media");
            DropTable("dbo.Vehicle");
            DropTable("dbo.State");
            DropTable("dbo.Location");
            DropTable("dbo.IncentiveGroup");
        }
    }
}
