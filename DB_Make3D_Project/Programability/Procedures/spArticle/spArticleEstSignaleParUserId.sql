CREATE PROCEDURE [dbo].[spArticleEstSignaleParUserId]
	@Id_article int,
	@Id_utilisateur int
AS
BEGIN
	SELECT COUNT(*)
	FROM [Signalement_article] 
	WHERE Id_article = @Id_article AND Id_utilisateur = @Id_utilisateur;
END
	
