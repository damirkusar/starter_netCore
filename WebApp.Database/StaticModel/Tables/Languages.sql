CREATE TABLE [StaticModel].[Languages] (
    [IsoAlpha2] CHAR (2)     NOT NULL,
    [Name]      VARCHAR (64) NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([IsoAlpha2] ASC) WITH (IGNORE_DUP_KEY = ON),
    CONSTRAINT [CK_Languages_IsoAlpha2] CHECK ((2)=len([IsoAlpha2])),
    CONSTRAINT [CK_Languages_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [UQ_Languages_Name] UNIQUE NONCLUSTERED ([Name] ASC) WITH (FILLFACTOR = 98)
);

