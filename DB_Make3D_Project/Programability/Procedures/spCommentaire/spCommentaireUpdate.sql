CREATE PROCEDURE [dbo].[spCommentaireUpdate]
	@Commentaire VARCHAR(250)
	
AS
	BEGIN
		UPDATE [Comentaire_article] 
		SET  Commentaire = @Commentaire, 
			 Date_modif = GETDATE();
		RETURN 0
	END
