CREATE TABLE [SecurityModel].[OpenIddictScopes] (
    [Id]          NVARCHAR (450) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_OpenIddictScopes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

