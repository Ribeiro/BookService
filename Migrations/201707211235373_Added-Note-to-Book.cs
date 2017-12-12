namespace BookService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotetoBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Note");
        }
    }
}
