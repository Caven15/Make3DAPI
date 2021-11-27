CREATE TRIGGER [trSignalement_articleInsteadOfInsert]
	ON [dbo].[Signalement_article]
	INSTEAD OF INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @Id_article INT, @Id_utilisateur INT;
		SELECT @Id_article = Id_article , @Id_utilisateur = Id_utilisateur FROM inserted;
		IF @Id_utilisateur != (SELECT Id_utilisateur FROM [Article] WHERE Id = @Id_article)
		BEGIN
			INSERT INTO  [Signalement_article] (Id_article, Id_utilisateur)
				VALUES (@Id_article, @Id_utilisateur);
		END
	END
