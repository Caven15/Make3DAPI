CREATE TABLE [dbo].[Fichier]
(
	[Id] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(80) NOT NULL,
    [Id_article] INT NOT NULL,

    CONSTRAINT [FK_Id_article_F] FOREIGN KEY ([Id_article]) REFERENCES [Article]([Id]), 
    CONSTRAINT [PK_Fichier] PRIMARY KEY ([Id])
)
