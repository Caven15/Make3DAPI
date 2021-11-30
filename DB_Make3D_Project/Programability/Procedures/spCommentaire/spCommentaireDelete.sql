CREATE PROCEDURE [dbo].[spCommentaireDelete]
	@Id int
AS
BEGIN
	DELETE 
	FROM [Comentaire_article]
	WHERE Id = @Id
	RETURN 0
END
	
