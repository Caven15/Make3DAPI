CREATE PROCEDURE [dbo].[spUtilisateurRegister]
    @Nom NVARCHAR(80),
    @Prenom NVARCHAR(80),
    @Email NVARCHAR(80),
    @DateNaissance NVARCHAR(50),
    @Password VARCHAR(50)
AS

BEGIN
    DECLARE @passwordHash BINARY(64), @securityStamp UNIQUEIDENTIFIER;

    SET @securityStamp = NEWID()
    SET @passwordHash = dbo.fHasher (TRIM(@Password), @securityStamp)

    INSERT INTO [Utilisateur] (Nom, Prenom, Email, DateNaissance, PasswordHash, SecurityStamp)
    VALUES (TRIM(@Nom), TRIM(@Prenom), TRIM(@Email), CONVERT(NVARCHAR(50),@DateNaissance), @passwordHash, @securityStamp)
END