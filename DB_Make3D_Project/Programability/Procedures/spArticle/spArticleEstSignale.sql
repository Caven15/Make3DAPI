CREATE PROCEDURE [dbo].[spArticleEstSignale]
	@Id_article int
AS
BEGIN
	SELECT COUNT(*)
	FROM [Signalement_article] 
	WHERE Id_article = @Id_article;
END
	

