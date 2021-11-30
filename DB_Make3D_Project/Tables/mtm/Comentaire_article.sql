CREATE TABLE [dbo].[Comentaire_article]
(
	[Id] INT NOT NULL IDENTITY, 
	[Id_utilisateur] INT NOT NULL, 
	[Id_article] INT NOT NULL,
	[Commentaire] NVARCHAR(250) NOT NULL, 
	[Date_envoi] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[Date_modif] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT [FK_Id_utilisateur_CO] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id]),
    CONSTRAINT [FK_Id_article_CO] FOREIGN KEY ([Id_article]) REFERENCES [Article]([Id]), 
    CONSTRAINT [PK_Comentaire_article] PRIMARY KEY ([Id])
)
