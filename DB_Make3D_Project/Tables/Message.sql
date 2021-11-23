CREATE TABLE [dbo].[Message]
(
	[Id_message] INT NOT NULL PRIMARY KEY, 
    [Contenu] NVARCHAR(500) NOT NULL, 
    [Date_envoi] DATETIME2 NOT NULL,
    [Id_utilisateur_Source] INT NOT NULL, 
	[Id_utilisateur_Cible] INT NOT NULL

    CONSTRAINT [FK_Id_utilisateursSource_Me] FOREIGN KEY ([Id_utilisateur_Source]) REFERENCES [Utilisateur]([Id_utilisateur]),
	CONSTRAINT [FK_Id_utilisateursCible_Me] FOREIGN KEY ([Id_utilisateur_Cible]) REFERENCES [Utilisateur]([Id_utilisateur])
)
