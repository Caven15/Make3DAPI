CREATE PROCEDURE [dbo].[spCommentaireGetAll]
AS
BEGIN
	SELECT * FROM [Comentaire_article]
	ORDER BY Date_envoi DESC;
	RETURN 0;
END
