CREATE TABLE [dbo].[Bloquage_article]
(
	[Id_utilisateur] INT NOT NULL, 
	[Id_article] INT NOT NULL,
    [Motivation] NVARCHAR(200) NOT NULL, 
    [Date_bloquage] DATETIME2 NOT NULL,

    CONSTRAINT [FK_Id_utilisateursSource_BLA] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id_utilisateur]),
	CONSTRAINT [FK_Id_utilisateursCible_BLA] FOREIGN KEY ([Id_article]) REFERENCES [Article]([Id_article])
)
