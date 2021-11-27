CREATE PROCEDURE [dbo].[spArticleCreate]
	@Nom NVARCHAR(100), 
    @Description NVARCHAR(2000), 
    @Id_utilisateur INT

AS
BEGIN
	INSERT INTO [Article]([Nom], [Description], [Id_utilisateur])
		VALUES (@Nom, @Description, @Id_utilisateur);
	RETURN 0;
END
