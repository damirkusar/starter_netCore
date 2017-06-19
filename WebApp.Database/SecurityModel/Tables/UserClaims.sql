CREATE TABLE [SecurityModel].[UserClaims] (
    [Id]     INT              IDENTITY (1, 1) NOT NULL,
    [Type]   VARCHAR (256)    NOT NULL,
    [Value]  VARCHAR (256)    NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [SecurityModel].[Users] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId]
    ON [SecurityModel].[UserClaims]([UserId] ASC);

