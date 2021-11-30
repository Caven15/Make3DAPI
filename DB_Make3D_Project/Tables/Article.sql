CREATE TABLE [dbo].[Article]
(
	[Id] INT NOT NULL IDENTITY, 
    [Nom] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
    [Id_utilisateur] INT NOT NULL,
    [Date_envoi] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[Date_modif] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT [FK_Id_utilisateur_A] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id]), 
    CONSTRAINT [PK_Article] PRIMARY KEY ([Id])

)
