CREATE PROCEDURE [dbo].[spArticleGetAll]
AS
BEGIN
	SELECT * FROM [Article]
	ORDER BY Date_envoi DESC;
	RETURN 0;
END
