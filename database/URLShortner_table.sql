USE [url_shortner_db]
GO

/****** Object:  Table [dbo].[URLShortner]    Script Date: 07/09/2014 20:09:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[URLShortner](
	[ShortCode] [varchar](50) NOT NULL,
	[URL] [varchar](max) NULL,
	[Status] [bit] NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_URLShortner] PRIMARY KEY CLUSTERED 
(
	[ShortCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


