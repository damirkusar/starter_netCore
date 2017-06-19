CREATE VIEW [Facade].[Localisations]
--WITH SCHEMABINDING
AS

    SELECT [LocalisationKeys].[Key]
		  ,Languages.IsoAlpha2 AS [LanguageIsoAlpha2]
		  ,COALESCE([Localisations].Value, [LocalisationKeys].DefaultValue) AS Value
	   FROM [StaticModel].[LocalisationKeys]
	   CROSS APPLY StaticModel.Languages
	   LEFT OUTER JOIN Model.[Localisations]
				 ON [LocalisationKeys].LocalisationKeyId = [Localisations].LocalisationKeyId
				AND Languages.IsoAlpha2 = [Localisations].[LanguageIsoAlpha2]
GO


CREATE TRIGGER [Facade].[TR_Localisations_IOD]
   ON [Facade].[Localisations]
   INSTEAD OF DELETE
AS 
BEGIN
    SET NOCOUNT ON;

    DELETE Localisations
	   FROM deleted
	   INNER JOIN StaticModel.LocalisationKeys
			 ON deleted.[Key] = LocalisationKeys.[Key]
	   INNER JOIN StaticModel.Localisations
			 ON deleted.LanguageIsoAlpha2 = Localisations.LanguageIsoAlpha2
		     AND LocalisationKeys.LocalisationKeyId = Localisations.LocalisationKeyId
END
GO

CREATE TRIGGER [Facade].[TR_Localisations_IOU]
   ON [Facade].[Localisations]
   INSTEAD OF UPDATE
AS 
BEGIN
    SET NOCOUNT ON;

    MERGE StaticModel.Localisations trgt
	   USING 
	   (
		  SELECT inserted.LanguageIsoAlpha2
				,LocalisationKeys.LocalisationKeyId
				,LocalisationKeys.DefaultValue
				,inserted.Value
			 FROM inserted
			 INNER JOIN StaticModel.LocalisationKeys
				    ON inserted.[Key] = LocalisationKeys.[Key]
	   ) src
	   ON trgt.LocalisationKeyId = src.LocalisationKeyId
	   AND trgt.LanguageIsoAlpha2 = src.LanguageIsoAlpha2
	   WHEN NOT MATCHED AND NOT (src.Value IS NULL OR 0 = LEN(src.Value)) THEN
		  INSERT (LocalisationKeyId, LanguageIsoAlpha2, Value) VALUES (src.LocalisationKeyId, src.LanguageIsoAlpha2, src.Value)
	   WHEN MATCHED AND (src.Value IS NULL OR 0 = LEN(src.Value) OR src.Value = src.DefaultValue) THEN
		  DELETE
	   WHEN MATCHED AND (trgt.Value != src.Value) THEN
		  UPDATE SET trgt.Value = src.Value;


END
GO



CREATE TRIGGER [Facade].[TR_Localisations_IOI]
   ON [Facade].[Localisations]
   INSTEAD OF INSERT
AS 
BEGIN
    SET NOCOUNT ON;

    INSERT INTO StaticModel.LocalisationKeys ([Key], DefaultValue)
	   SELECT inserted.[Key], inserted.Value
		  FROM inserted
		  WHERE inserted.LanguageIsoAlpha2 = 'EN'

    INSERT INTO StaticModel.Localisations (LocalisationKeyId, LanguageIsoAlpha2, Value)
	   SELECT LocalisationKeys.LocalisationKeyId, inserted.LanguageIsoAlpha2, inserted.Value
		  FROM inserted
		  INNER JOIN StaticModel.LocalisationKeys
				ON inserted.[Key] = LocalisationKeys.[Key]
		  WHERE inserted.LanguageIsoAlpha2 <> 'EN'
		    AND inserted.Value <> LocalisationKeys.DefaultValue
END