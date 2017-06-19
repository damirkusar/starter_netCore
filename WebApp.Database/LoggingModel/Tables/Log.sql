CREATE TABLE [LoggingModel].[Log] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Logged]    DATETIME      NOT NULL,
    [Host]      VARCHAR (150) NOT NULL,
    [AppName]   VARCHAR (150) NOT NULL,
    [Level]     VARCHAR (50)  NOT NULL,
    [Message]   VARCHAR (512) NOT NULL,
    [Logger]    VARCHAR (250) NULL,
    [Callsite]  VARCHAR (512) NULL,
    [Exception] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Model.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);

