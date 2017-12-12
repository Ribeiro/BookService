namespace BookService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBookAndAuthorIdTypeToLong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropPrimaryKey("dbo.Authors");
            DropPrimaryKey("dbo.Books");
            AddColumn("dbo.Books", "Author_Id", c => c.Long());
            AlterColumn("dbo.Authors", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Books", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Authors", "Id");
            AddPrimaryKey("dbo.Books", "Id");
            CreateIndex("dbo.Books", "Author_Id");
            AddForeignKey("dbo.Books", "Author_Id", "dbo.Authors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropPrimaryKey("dbo.Books");
            DropPrimaryKey("dbo.Authors");
            AlterColumn("dbo.Books", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Authors", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Books", "Author_Id");
            AddPrimaryKey("dbo.Books", "Id");
            AddPrimaryKey("dbo.Authors", "Id");
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
        }
    }
}
