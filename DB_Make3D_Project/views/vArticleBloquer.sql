CREATE VIEW [dbo].[vArticleBloquer]
	AS 
	SELECT A.Id, A.Nom, A.Description, A.Id_utilisateur, Ba.Motivation, Ba.Date_bloquage, Ba.Id_utilisateur AS Id_Bloqueur 
	FROM [Bloquage_article] AS Ba
	JOIN [Article] AS A 
	ON Ba.Id_article = A.Id
