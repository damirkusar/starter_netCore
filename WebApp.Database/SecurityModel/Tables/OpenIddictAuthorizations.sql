CREATE TABLE [SecurityModel].[OpenIddictAuthorizations] (
    [Id]            NVARCHAR (450) NOT NULL,
    [ApplicationId] NVARCHAR (450) NULL,
    [Scope]         NVARCHAR (MAX) NULL,
    [Subject]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_OpenIddictAuthorizations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [SecurityModel].[OpenIddictApplications] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_OpenIddictAuthorizations_ApplicationId]
    ON [SecurityModel].[OpenIddictAuthorizations]([ApplicationId] ASC);

