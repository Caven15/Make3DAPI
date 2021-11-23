CREATE TABLE [dbo].[Bloquage_user]
(
	[Id_utilisateur_Source] INT NOT NULL, 
	[Id_utilisateur_Cible] INT NOT NULL

    CONSTRAINT [FK_Id_utilisateursSource_BLU] FOREIGN KEY ([Id_utilisateur_Source]) REFERENCES [Utilisateur]([Id]),
	CONSTRAINT [FK_Id_utilisateursCible_BLU] FOREIGN KEY ([Id_utilisateur_Cible]) REFERENCES [Utilisateur]([Id])
)
