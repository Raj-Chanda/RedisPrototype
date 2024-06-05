USE [RedisDB]
GO
/****** Object:  Table [dbo].[Truck]    Script Date: 6/5/2024 7:54:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Truck](
	[Id] [uniqueidentifier] NOT NULL,
	[TruckNumber] [varchar](20) NOT NULL,
	[EngineNumber] [varchar](20) NULL,
	[ChasisNumber] [varchar](20) NULL,
	[Wheels] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
