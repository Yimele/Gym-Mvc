namespace MVCWithWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        TrackID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Track", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        FacilityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackID)
                .ForeignKey("dbo.Facility", t => t.FacilityID, cascadeDelete: true)
                .Index(t => t.FacilityID);
            
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        FacilityID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Fees = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Administrator_ID = c.Int(),
                    })
                .PrimaryKey(t => t.FacilityID)
                .ForeignKey("dbo.Trainer", t => t.Administrator_ID)
                .Index(t => t.Administrator_ID);
            
            CreateTable(
                "dbo.Trainer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrackTrainer",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        TrainerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackID, t.TrainerID })
                .ForeignKey("dbo.Track", t => t.TrackID, cascadeDelete: true)
                .ForeignKey("dbo.Trainer", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.TrainerID);
            
            CreateStoredProcedure(
                "dbo.Facility_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 50),
                        Fees = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        InstructorID = p.Int(),
                        Administrator_ID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Facility]([Name], [Fees], [StartDate], [InstructorID], [Administrator_ID])
                      VALUES (@Name, @Fees, @StartDate, @InstructorID, @Administrator_ID)
                      
                      DECLARE @FacilityID int
                      SELECT @FacilityID = [FacilityID]
                      FROM [dbo].[Facility]
                      WHERE @@ROWCOUNT > 0 AND [FacilityID] = scope_identity()
                      
                      SELECT t0.[FacilityID], t0.[RowVersion]
                      FROM [dbo].[Facility] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[FacilityID] = @FacilityID"
            );
            
            CreateStoredProcedure(
                "dbo.Facility_Update",
                p => new
                    {
                        FacilityID = p.Int(),
                        Name = p.String(maxLength: 50),
                        Fees = p.Decimal(precision: 19, scale: 4, storeType: "money"),
                        StartDate = p.DateTime(),
                        InstructorID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                        Administrator_ID = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Facility]
                      SET [Name] = @Name, [Fees] = @Fees, [StartDate] = @StartDate, [InstructorID] = @InstructorID, [Administrator_ID] = @Administrator_ID
                      WHERE (([FacilityID] = @FacilityID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))
                      
                      SELECT t0.[RowVersion]
                      FROM [dbo].[Facility] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[FacilityID] = @FacilityID"
            );
            
            CreateStoredProcedure(
                "dbo.Facility_Delete",
                p => new
                    {
                        FacilityID = p.Int(),
                        RowVersion_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                        Administrator_ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Facility]
                      WHERE ((([FacilityID] = @FacilityID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL))) AND (([Administrator_ID] = @Administrator_ID) OR ([Administrator_ID] IS NULL AND @Administrator_ID IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Facility_Delete");
            DropStoredProcedure("dbo.Facility_Update");
            DropStoredProcedure("dbo.Facility_Insert");
            DropForeignKey("dbo.TrackTrainer", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.TrackTrainer", "TrackID", "dbo.Track");
            DropForeignKey("dbo.Track", "FacilityID", "dbo.Facility");
            DropForeignKey("dbo.Facility", "Administrator_ID", "dbo.Trainer");
            DropForeignKey("dbo.Enrollment", "TrackID", "dbo.Track");
            DropForeignKey("dbo.Enrollment", "CustomerID", "dbo.Customer");
            DropIndex("dbo.TrackTrainer", new[] { "TrainerID" });
            DropIndex("dbo.TrackTrainer", new[] { "TrackID" });
            DropIndex("dbo.Facility", new[] { "Administrator_ID" });
            DropIndex("dbo.Track", new[] { "FacilityID" });
            DropIndex("dbo.Enrollment", new[] { "CustomerID" });
            DropIndex("dbo.Enrollment", new[] { "TrackID" });
            DropTable("dbo.TrackTrainer");
            DropTable("dbo.Trainer");
            DropTable("dbo.Facility");
            DropTable("dbo.Track");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Customer");
        }
    }
}
