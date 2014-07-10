USE [url_shortner_db]
GO

/****** Object:  StoredProcedure [dbo].[URLShortner_Code_Select]    Script Date: 07/09/2014 20:11:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[URLShortner_Code_Select]
	-- Add the parameters for the stored procedure here
	@Code varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT URL FROM dbo.URLShortner WHERE ShortCode = @Code
END



GO


