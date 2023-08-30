namespace Movie_Mania.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seriesmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Country = c.String(nullable: false),
                        NumberOfSeasons = c.Int(nullable: false),
                        ImagePath = c.String(),
                        VideoPath = c.String(),
                        Slug = c.String(),
                        NewComment_Id = c.Int(),
                        NewSeriesRating_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeriesComments", t => t.NewComment_Id)
                .ForeignKey("dbo.SeriesRatings", t => t.NewSeriesRating_Id)
                .Index(t => t.NewComment_Id)
                .Index(t => t.NewSeriesRating_Id);
            
            CreateTable(
                "dbo.SeriesComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Post = c.String(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        SeriesId = c.Int(nullable: false),
                        Slug = c.String(),
                        Series_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.SeriesId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .Index(t => t.SeriesId)
                .Index(t => t.Series_Id);
            
            CreateTable(
                "dbo.SeriesRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        SeriesID = c.Int(nullable: false),
                        UserName = c.String(),
                        Slug = c.String(),
                        Series_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.SeriesID, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .Index(t => t.SeriesID)
                .Index(t => t.Series_Id);
            
            CreateTable(
                "dbo.SeriesPickedGenres",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        genreid = c.Int(nullable: false),
                        Selected = c.Boolean(nullable: false),
                        Series_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Genres", t => t.genreid, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .Index(t => t.genreid)
                .Index(t => t.Series_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeriesPickedGenres", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.SeriesPickedGenres", "genreid", "dbo.Genres");
            DropForeignKey("dbo.SeriesRatings", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.SeriesComments", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.Series", "NewSeriesRating_Id", "dbo.SeriesRatings");
            DropForeignKey("dbo.SeriesRatings", "SeriesID", "dbo.Series");
            DropForeignKey("dbo.Series", "NewComment_Id", "dbo.SeriesComments");
            DropForeignKey("dbo.SeriesComments", "SeriesId", "dbo.Series");
            DropIndex("dbo.SeriesPickedGenres", new[] { "Series_Id" });
            DropIndex("dbo.SeriesPickedGenres", new[] { "genreid" });
            DropIndex("dbo.SeriesRatings", new[] { "Series_Id" });
            DropIndex("dbo.SeriesRatings", new[] { "SeriesID" });
            DropIndex("dbo.SeriesComments", new[] { "Series_Id" });
            DropIndex("dbo.SeriesComments", new[] { "SeriesId" });
            DropIndex("dbo.Series", new[] { "NewSeriesRating_Id" });
            DropIndex("dbo.Series", new[] { "NewComment_Id" });
            DropTable("dbo.SeriesPickedGenres");
            DropTable("dbo.SeriesRatings");
            DropTable("dbo.SeriesComments");
            DropTable("dbo.Series");
        }
    }
}
