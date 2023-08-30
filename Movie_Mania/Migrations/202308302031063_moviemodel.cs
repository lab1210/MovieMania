namespace Movie_Mania.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moviemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Country = c.String(),
                        ImagePath = c.String(),
                        VideoPath = c.String(),
                        Slug = c.String(),
                        NewComment_Id = c.Int(),
                        NewRating_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieComments", t => t.NewComment_Id)
                .ForeignKey("dbo.Ratings", t => t.NewRating_Id)
                .Index(t => t.NewComment_Id)
                .Index(t => t.NewRating_Id);
            
            CreateTable(
                "dbo.MovieComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Post = c.String(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Slug = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.MovieId)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                        UserName = c.String(),
                        Slug = c.String(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.MovieID)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.PickedGenres",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        genreid = c.Int(nullable: false),
                        Selected = c.Boolean(nullable: false),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Genres", t => t.genreid, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.genreid)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PickedGenres", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.PickedGenres", "genreid", "dbo.Genres");
            DropForeignKey("dbo.Ratings", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Movies", "NewRating_Id", "dbo.Ratings");
            DropForeignKey("dbo.Ratings", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Movies", "NewComment_Id", "dbo.MovieComments");
            DropForeignKey("dbo.MovieComments", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieComments", "MovieId", "dbo.Movies");
            DropIndex("dbo.PickedGenres", new[] { "Movie_Id" });
            DropIndex("dbo.PickedGenres", new[] { "genreid" });
            DropIndex("dbo.Ratings", new[] { "Movie_Id" });
            DropIndex("dbo.Ratings", new[] { "MovieID" });
            DropIndex("dbo.MovieComments", new[] { "Movie_Id" });
            DropIndex("dbo.MovieComments", new[] { "MovieId" });
            DropIndex("dbo.Movies", new[] { "NewRating_Id" });
            DropIndex("dbo.Movies", new[] { "NewComment_Id" });
            DropTable("dbo.PickedGenres");
            DropTable("dbo.Ratings");
            DropTable("dbo.MovieComments");
            DropTable("dbo.Movies");
        }
    }
}
