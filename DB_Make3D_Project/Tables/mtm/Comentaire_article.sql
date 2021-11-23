CREATE TABLE [dbo].[Comentaire_article]
(
	[Id_utilisateur] INT NOT NULL, 
	[Id_article] INT NOT NULL,
	[Commentaire] NVARCHAR(250) NOT NULL, 

    CONSTRAINT [FK_Id_utilisateur_CO] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id_utilisateur]),
    CONSTRAINT [FK_Id_article_CO] FOREIGN KEY ([Id_article]) REFERENCES [Article]([Id_article])
)
