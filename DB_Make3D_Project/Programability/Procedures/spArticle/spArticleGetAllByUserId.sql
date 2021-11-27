CREATE PROCEDURE [dbo].[spArticleGetAllByUserId]
		@Id_utilisateur int
AS
	SELECT * 
	FROM [Article]
	WHERE Id_utilisateur = @Id_utilisateur
RETURN 0
