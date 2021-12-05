CREATE PROCEDURE [dbo].[spFichierGetById]
		@Id int
AS
	SELECT * 
	FROM [Fichier]
	WHERE Id = @Id
RETURN 0
