CREATE PROCEDURE [dbo].[spArticleUpdate]
	@Nom NVARCHAR(100), 
    @Description NVARCHAR(2000), 
    @Id INT
AS
BEGIN
	UPDATE [Article]
	SET [Nom] = @Nom ,
		[Description] = @Description ,
		Date_modif = GETDATE()
	WHERE Id = @Id ;
	RETURN 0
END
