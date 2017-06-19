CREATE TABLE [SecurityModel].[Roles] (
    [RoleId]           UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX)   NULL,
    [Name]             VARCHAR (64)     NOT NULL,
    [NormalizedName]   VARCHAR (64)     NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [SecurityModel].[Roles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);

