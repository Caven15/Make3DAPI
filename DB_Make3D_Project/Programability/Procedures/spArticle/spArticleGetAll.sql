﻿CREATE PROCEDURE [dbo].[spArticleGetAll]
AS
BEGIN
	SELECT * FROM [Article];
	RETURN 0;
END
