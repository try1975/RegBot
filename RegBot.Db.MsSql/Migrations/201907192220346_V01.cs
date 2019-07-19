namespace RegBot.Db.MsSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountsData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangeBy = c.String(maxLength: 50),
                        ChangeAt = c.DateTime(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        AccountName = c.String(),
                        Password = c.String(),
                        Domain = c.String(),
                        PhoneCountryCode = c.String(),
                        Phone = c.String(),
                        Success = c.Boolean(nullable: false),
                        ErrMsg = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuditEntries",
                c => new
                    {
                        AuditEntryID = c.Int(nullable: false, identity: true),
                        EntitySetName = c.String(maxLength: 255),
                        EntityTypeName = c.String(maxLength: 255),
                        State = c.Int(nullable: false),
                        StateName = c.String(maxLength: 255),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditEntryID);
            
            CreateTable(
                "dbo.AuditEntryProperties",
                c => new
                    {
                        AuditEntryPropertyID = c.Int(nullable: false, identity: true),
                        AuditEntryID = c.Int(nullable: false),
                        RelationName = c.String(maxLength: 255),
                        PropertyName = c.String(maxLength: 255),
                        OldValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.AuditEntryPropertyID)
                .ForeignKey("dbo.AuditEntries", t => t.AuditEntryID, cascadeDelete: true)
                .Index(t => t.AuditEntryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuditEntryProperties", "AuditEntryID", "dbo.AuditEntries");
            DropIndex("dbo.AuditEntryProperties", new[] { "AuditEntryID" });
            DropTable("dbo.AuditEntryProperties");
            DropTable("dbo.AuditEntries");
            DropTable("dbo.AccountsData");
        }
    }
}
