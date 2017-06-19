CREATE TABLE [SecurityModel].[Users] (
    [UserId]               UNIQUEIDENTIFIER   NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [Email]                VARCHAR (320)      NOT NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [FirstName]            VARCHAR (64)       NOT NULL,
    [LastName]             VARCHAR (64)       NOT NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [NormalizedEmail]      VARCHAR (320)      NULL,
    [NormalizedUserName]   VARCHAR (320)      NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [PhoneNumber]          VARCHAR (32)       NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [UserName]             VARCHAR (320)      NOT NULL,
    [Image]                VARCHAR (MAX)      NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [SecurityModel].[Users]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [SecurityModel].[Users]([NormalizedEmail] ASC);

