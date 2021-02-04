namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
           Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'88fc9bf9-cb07-499f-b237-c045f08887c8', N'guest', N'AHXbgRI3X8dtb+TsqutzUy/NwM70lESD/am5daT31ii7ZljHzsLy22njho0DLtzVBw==', N'4e2c5803-328e-4cff-99b1-925a327384f2', N'ApplicationUser')
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'f0f08e51-85eb-4ef4-b4b5-2e1502214e8f', N'admin', N'AIbQS78SvwBxJbH5G+Z4CeVp2ZIyvgUHfPvg0DaoTQXOCRGXEMCdvEnanvEKpwioVQ==', N'18807a89-5cfd-4517-b41b-dbc0785458b4', N'ApplicationUser')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0611611f-faf4-4b48-b5d3-15b5162e090f', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f0f08e51-85eb-4ef4-b4b5-2e1502214e8f', N'0611611f-faf4-4b48-b5d3-15b5162e090f')

");
        }
        
        public override void Down()
        {
        }
    }
}
