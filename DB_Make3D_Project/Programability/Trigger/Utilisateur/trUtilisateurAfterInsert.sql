CREATE TRIGGER [trUtilisateurAfterInsert]
	ON [dbo].[Utilisateur]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		IF (SELECT COUNT (*) FROM [Utilisateur]) = 1
		BEGIN
			UPDATE [Utilisateur]
			SET IsAdmin = 1
		END
	END
