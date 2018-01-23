namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'23460cab-e522-449c-990a-75044e1d8fa9', N'admin@vidly.com', 0, N'AHoQqy5ZTeeXE75ruBzIedROge8fMMtrvrh/lLdEeZ4JUMBM1E5Tq+uMn4f24BFHAw==', N'b8d9b621-6681-4cb8-bfa0-adec8349adf7', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'33daac15-111a-4dfa-b72a-c37e4061f42d', N'guest@vidly.com', 0, N'AI6AL7b7h18fL/OZoUfIxunseP8VfNDswEU7IfrpE3QsVAFthOQrhNdDyuxv+zzCQg==', N'40ea9e60-372f-4fa7-a634-00b257c3846a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'34dfa56a-e83b-4ce6-9934-7106e36307ac', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'23460cab-e522-449c-990a-75044e1d8fa9', N'34dfa56a-e83b-4ce6-9934-7106e36307ac')
 ");
        }
        
        public override void Down()
        {
        }
    }
}
