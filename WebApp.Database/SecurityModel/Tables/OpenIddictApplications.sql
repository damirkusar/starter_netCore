CREATE TABLE [SecurityModel].[OpenIddictApplications] (
    [Id]                NVARCHAR (450) NOT NULL,
    [ClientId]          NVARCHAR (450) NULL,
    [ClientSecret]      NVARCHAR (MAX) NULL,
    [DisplayName]       NVARCHAR (MAX) NULL,
    [LogoutRedirectUri] NVARCHAR (MAX) NULL,
    [RedirectUri]       NVARCHAR (MAX) NULL,
    [Type]              NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_OpenIddictApplications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_OpenIddictApplications_ClientId]
    ON [SecurityModel].[OpenIddictApplications]([ClientId] ASC) WHERE ([ClientId] IS NOT NULL);

