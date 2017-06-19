CREATE TABLE [SecurityModel].[UserLogins] (
    [LoginProvider]       VARCHAR (256)    NOT NULL,
    [ProviderKey]         VARCHAR (256)    NOT NULL,
    [ProviderDisplayName] VARCHAR (256)    NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [SecurityModel].[Users] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId]
    ON [SecurityModel].[UserLogins]([UserId] ASC);

