USE [url_shortner_db]
GO

/****** Object:  StoredProcedure [dbo].[URLShortner_URL_Select]    Script Date: 07/09/2014 20:12:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[URLShortner_URL_Select]
	-- Add the parameters for the stored procedure here
	@URL varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ShortCode FROM dbo.URLShortner WHERE URL = @URL
END




GO


