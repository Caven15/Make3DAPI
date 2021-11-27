CREATE TABLE [dbo].[Bloquage_article]
(
	[Id_utilisateur] INT NOT NULL, 
	[Id_article] INT NOT NULL,
    [Motivation] NVARCHAR(200) NOT NULL, 
    [Date_bloquage] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT [FK_Id_utilisateursSource_BLA] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id]),
	CONSTRAINT [FK_Id_utilisateursCible_BLA] FOREIGN KEY ([Id_article]) REFERENCES [Article]([Id]), 
    CONSTRAINT [UK_Bloquage_article] UNIQUE ([Id_article])
)
