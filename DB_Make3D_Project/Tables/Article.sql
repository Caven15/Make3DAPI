CREATE TABLE [dbo].[Article]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nom] NVARCHAR(100) NOT NULL, 
    [Decription] NVARCHAR(2000) NOT NULL, 
    [Id_utilisateur] INT NOT NULL, 

    CONSTRAINT [FK_Id_utilisateur_A] FOREIGN KEY ([Id_utilisateur]) REFERENCES [Utilisateur]([Id])

)
