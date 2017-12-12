namespace BookService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAuthorIdTypeToLong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropColumn("dbo.Books", "AuthorId");
            RenameColumn(table: "dbo.Books", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Books", "AuthorId", c => c.Long(nullable: false));
            AlterColumn("dbo.Books", "AuthorId", c => c.Long(nullable: false));
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            AlterColumn("dbo.Books", "AuthorId", c => c.Long());
            AlterColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Books", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "Author_Id");
            AddForeignKey("dbo.Books", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
