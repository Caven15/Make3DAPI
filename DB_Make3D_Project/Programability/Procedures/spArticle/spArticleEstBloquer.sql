CREATE PROCEDURE [dbo].[spArticleEstBloquer]
	@Id_article int
AS
BEGIN
	SELECT COUNT(*)
	FROM [Bloquage_article] 
	WHERE Id_article = @Id_article;
END
	