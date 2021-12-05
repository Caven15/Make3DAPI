CREATE PROCEDURE [dbo].[spFichierGetByArticleId]
		@Id_article int
AS
	SELECT * 
	FROM [Fichier]
	WHERE Id_article = @Id_article
RETURN 0
