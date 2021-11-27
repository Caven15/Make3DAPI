CREATE TRIGGER [trBloquage_articleOnInsert]
	ON [dbo].[Bloquage_article]
	AFTER INSERT
	AS
	BEGIN
		DELETE 
		FROM [Signalement_article]
		WHERE Id_article IN (SELECT Id_article FROM inserted);
	END
