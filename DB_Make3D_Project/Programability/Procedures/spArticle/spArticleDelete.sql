CREATE PROCEDURE [dbo].[spArticleDelete]
	@Id int 
AS
BEGIN
	DELETE 
	FROM [Article]
	WHERE Id = @Id
END