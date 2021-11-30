CREATE PROCEDURE [dbo].[spCommentaireGetAllByArticleId]
		@Id_article INT

AS
BEGIN
	SELECT * FROM [Comentaire_article]
	WHERE Id_article = @Id_article
	ORDER BY Date_envoi DESC
	RETURN 0;
END
