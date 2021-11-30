CREATE PROCEDURE [dbo].[spCommentaireGetById]
		@Id INT

AS
BEGIN
	SELECT * FROM [Comentaire_article]
	WHERE Id = @Id
	RETURN 0;
END
