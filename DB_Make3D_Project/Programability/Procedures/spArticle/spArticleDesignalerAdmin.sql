CREATE PROCEDURE [dbo].[spArticleDesignalerAdmin]
	@Id_utilisateur int ,
	@Id_article int
AS
BEGIN
	IF (SELECT IsAdmin FROM [Utilisateur] WHERE Id = @Id_utilisateur) = 1
	BEGIN
		DELETE 
		FROM [Signalement_article]
		WHERE Id_article = @Id_article;
	END
	RETURN 0
END
	
