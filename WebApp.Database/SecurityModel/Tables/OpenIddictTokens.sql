CREATE TABLE [SecurityModel].[OpenIddictTokens] (
    [Id]              NVARCHAR (450) NOT NULL,
    [ApplicationId]   NVARCHAR (450) NULL,
    [AuthorizationId] NVARCHAR (450) NULL,
    [Subject]         NVARCHAR (MAX) NULL,
    [Type]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_OpenIddictTokens] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OpenIddictTokens_OpenIddictApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [SecurityModel].[OpenIddictApplications] ([Id]),
    CONSTRAINT [FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId] FOREIGN KEY ([AuthorizationId]) REFERENCES [SecurityModel].[OpenIddictAuthorizations] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_OpenIddictTokens_AuthorizationId]
    ON [SecurityModel].[OpenIddictTokens]([AuthorizationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OpenIddictTokens_ApplicationId]
    ON [SecurityModel].[OpenIddictTokens]([ApplicationId] ASC);

