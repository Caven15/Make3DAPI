CREATE PROCEDURE [dbo].[spArticleSignalement]
	@Id_utilisateur int ,
	@Id_article int
AS
BEGIN
	INSERT INTO  [Signalement_article] (Id_article, Id_utilisateur)
	VALUES (@Id_article, @Id_utilisateur);
	RETURN 0
END
	