CREATE PROCEDURE [dbo].[spCommentaireUpdate]
	@Commentaire VARCHAR(250),
	@Id INT
	
AS
	BEGIN
		UPDATE [Comentaire_article] 
		SET  Commentaire = @Commentaire, 
			 Date_modif = GETDATE()
			 WHERE Id = @Id;
		RETURN 0
	END
