CREATE PROCEDURE [dbo].[spArticleDesignaler]
	@Id_utilisateur int ,
	@Id_article int
AS
BEGIN
	DELETE 
	FROM [Signalement_article]
	WHERE Id_article = @Id_article AND Id_utilisateur = @Id_utilisateur;
	RETURN 0
END
	
