CREATE PROCEDURE [dbo].[spFichierCreate]
    @Name NVARCHAR(80), 
    @Id_article INT
AS
BEGIN
	INSERT INTO [Fichier]([Name], [Id_article])
		VALUES (@Name, @Id_article);
	RETURN 0;
END
