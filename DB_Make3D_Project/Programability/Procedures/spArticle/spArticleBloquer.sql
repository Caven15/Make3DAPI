CREATE PROCEDURE [dbo].[spArticleBloquer]
	@Id_utilisateur int,
	@Id_article int,
	@Motivation NVARCHAR(200)
AS
BEGIN
	IF (SELECT IsAdmin FROM [Utilisateur] WHERE Id = @Id_utilisateur) = 1
	BEGIN
		INSERT INTO  [Bloquage_article] (Id_utilisateur, Id_article, Motivation)
			VALUES (@Id_utilisateur, @Id_article, @Motivation);
	END
	RETURN 0
END
	