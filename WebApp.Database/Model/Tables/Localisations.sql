CREATE TABLE [Model].[Localisations] (
    [LocalisationKeyId] INT           NOT NULL,
    [LanguageIsoAlpha2] CHAR (2)      NOT NULL,
    [Value]             VARCHAR (512) NOT NULL,
    CONSTRAINT [PK_Localisations] PRIMARY KEY CLUSTERED ([LocalisationKeyId] ASC, [LanguageIsoAlpha2] ASC),
    CONSTRAINT [CK_Localisations_Value] CHECK ((0)<len([Value])),
    CONSTRAINT [FK_Localisations_Languages] FOREIGN KEY ([LanguageIsoAlpha2]) REFERENCES [StaticModel].[Languages] ([IsoAlpha2]),
    CONSTRAINT [FK_Localisations_LocalisationKeys] FOREIGN KEY ([LocalisationKeyId]) REFERENCES [StaticModel].[LocalisationKeys] ([LocalisationKeyId])
);



