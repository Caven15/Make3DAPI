CREATE TABLE [dbo].[Signalement_article]
(
	[Id_utilisateur] INT NOT NULL, 
	[Id_article] INT NOT NULL

    CONSTRAINT [FK_Id_utilisateur_SI] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id]),
	CONSTRAINT [FK_Id_article_SI] FOREIGN KEY ([Id_article]) REFERENCES [Article]([Id])
)
