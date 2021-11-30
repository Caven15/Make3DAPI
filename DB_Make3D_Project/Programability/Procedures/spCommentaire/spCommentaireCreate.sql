CREATE PROCEDURE [dbo].[spCommentaireCreate]
	@Commentaire VARCHAR(250),
    @Id_article INT,
    @Id_utilisateur INT
AS
	BEGIN
		INSERT INTO [Comentaire_article] (Commentaire, Id_article, Id_utilisateur)
		VALUES (@Commentaire, @Id_article, @Id_utilisateur);
		RETURN 0
	END
