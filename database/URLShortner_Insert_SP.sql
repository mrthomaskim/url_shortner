USE [url_shortner_db]
GO

/****** Object:  StoredProcedure [dbo].[URLShortner_Insert]    Script Date: 07/09/2014 20:11:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[URLShortner_Insert]
	-- Add the parameters for the stored procedure here
	@URL VARCHAR(MAX),	@Code VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO dbo.URLShortner 
	        ( ShortCode, URL, Status, Deleted )
	VALUES  ( @Code, -- ShortCode - varchar(50)
	          @URL, -- URL - varchar(max)
	          1, -- Status - bit
	          0  -- Deleted - bit
	          )
END




GO


