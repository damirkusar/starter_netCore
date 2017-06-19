CREATE TABLE [SecurityModel].[RoleClaims] (
    [Id]     INT              IDENTITY (1, 1) NOT NULL,
    [Type]   VARCHAR (256)    NOT NULL,
    [Value]  VARCHAR (256)    NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [SecurityModel].[Roles] ([RoleId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId]
    ON [SecurityModel].[RoleClaims]([RoleId] ASC);

