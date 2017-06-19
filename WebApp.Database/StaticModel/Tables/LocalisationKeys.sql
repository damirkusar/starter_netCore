CREATE TABLE [StaticModel].[LocalisationKeys] (
    [LocalisationKeyId] INT           IDENTITY (1, 1) NOT NULL,
    [Key]               VARCHAR (64)  NOT NULL,
    [DefaultValue]      VARCHAR (512) NOT NULL,
    CONSTRAINT [PK_LocalisationKeys] PRIMARY KEY CLUSTERED ([LocalisationKeyId] ASC),
    CONSTRAINT [CK_LocalisationKeys_DefaultValue] CHECK ((0)<len([DefaultValue])),
    CONSTRAINT [CK_LocalisationKeys_Key] CHECK ((0)<len([Key])),
    CONSTRAINT [UQ_LocalisationKeys_Key] UNIQUE NONCLUSTERED ([Key] ASC) WITH (FILLFACTOR = 98)
);

