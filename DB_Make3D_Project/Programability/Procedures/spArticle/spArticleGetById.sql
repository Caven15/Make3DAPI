CREATE PROCEDURE [dbo].[spArticleGetById]
		@Id int
AS
	SELECT * 
	FROM [Article]
	WHERE Id = @Id
RETURN 0
