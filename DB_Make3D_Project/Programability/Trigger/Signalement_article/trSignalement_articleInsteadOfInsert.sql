CREATE TRIGGER [trSignalement_articleInsteadOfInsert]
	ON [dbo].[Signalement_article]
	INSTEAD OF INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @Id_article INT, @Id_utilisateur INT;
		SELECT @Id_article = Id_article , @Id_utilisateur = Id_utilisateur FROM inserted;
		IF ((@Id_utilisateur != (SELECT Id_utilisateur FROM [Article] WHERE Id = @Id_article))-- si l'utilisateur n'est pas le créateur
		AND ((SELECT COUNT(*) FROM [Bloquage_article] WHERE Id_article = @Id_article) = 0)-- si l'article n'est pas bloqué
		AND ((SELECT COUNT(*) FROM [Signalement_article] WHERE Id_article = @Id_article AND Id_utilisateur = @Id_utilisateur) = 0))-- si l'article n'est pas déjà signalé par l'utilisateur
			BEGIN
				INSERT INTO  [Signalement_article] (Id_article, Id_utilisateur)
					VALUES (@Id_article, @Id_utilisateur);
			END
	END
