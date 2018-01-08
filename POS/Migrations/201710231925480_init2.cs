namespace POS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            //Sql("INSERT INTO Items(Id, Name, Price) VALUES(1, 'Mobile', 10000)");
        }
        
        public override void Down()
        {
        }
    }
}
