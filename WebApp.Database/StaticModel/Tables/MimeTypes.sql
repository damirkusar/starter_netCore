CREATE TABLE [StaticModel].[MimeTypes] (
    [FileExtension] VARCHAR (16)  NOT NULL,
    [Name]          VARCHAR (128) NOT NULL,
    [ContentType]   VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_MimeTypes] PRIMARY KEY CLUSTERED ([FileExtension] ASC, [Name] ASC, [ContentType] ASC),
    CONSTRAINT [CK_MimeTypes_ContentType] CHECK ((0)<len([ContentType])),
    CONSTRAINT [CK_MimeTypes_Name] CHECK ((0)<len([Name]))
);

