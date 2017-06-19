CREATE TABLE [StaticModel].[Countries] (
    [IsoAlpha2]   CHAR (2)     NOT NULL,
    [IsoAlpha3]   CHAR (3)     NOT NULL,
    [Name]        VARCHAR (64) NOT NULL,
    [NumericCode] CHAR (3)     NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([IsoAlpha2] ASC) WITH (FILLFACTOR = 98),
    CONSTRAINT [CK_Countries_IsoAlpha2] CHECK ((2)=len([IsoAlpha2])),
    CONSTRAINT [CK_Countries_IsoAlpha3] CHECK ((3)=len([IsoAlpha3])),
    CONSTRAINT [CK_Countries_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Countries_NumericalCode] CHECK ((0)<len([NumericCode])),
    CONSTRAINT [UQ_Countries_IsoAlpha3] UNIQUE NONCLUSTERED ([IsoAlpha3] ASC) WITH (FILLFACTOR = 98),
    CONSTRAINT [UQ_Countries_Name] UNIQUE NONCLUSTERED ([Name] ASC) WITH (FILLFACTOR = 98),
    CONSTRAINT [UQ_Countries_NumericalCode] UNIQUE NONCLUSTERED ([NumericCode] ASC) WITH (FILLFACTOR = 98)
);

