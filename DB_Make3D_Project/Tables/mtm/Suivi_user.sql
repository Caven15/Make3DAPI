CREATE TABLE [dbo].[Suivi_user]
(
	[Id_utilisateur_Source] INT NOT NULL, 
	[Id_utilisateur_Cible] INT NOT NULL

    CONSTRAINT [FK_Id_utilisateursSource_SU] FOREIGN KEY ([Id_utilisateur_Source]) REFERENCES [Utilisateur]([Id]),
	CONSTRAINT [FK_Id_utilisateursCible_SU] FOREIGN KEY ([Id_utilisateur_Cible]) REFERENCES [Utilisateur]([Id]), 
    CONSTRAINT [UK_Suivi_user] UNIQUE ([Id_utilisateur_Source],[Id_utilisateur_Cible])
)
