# cursobasico de c#
```
USE [db_almacen]
GO
/****** Object:  Table [dbo].[c_typeProduct]    Script Date: 06/11/2019 01:22:18 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_typeProduct](
	[id_typeProduct] [int] IDENTITY(1,1) NOT NULL,
	[flddescription] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_c_typeProduct] PRIMARY KEY CLUSTERED 
(
	[id_typeProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[product]    Script Date: 06/11/2019 01:22:18 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id_product] [int] IDENTITY(1,1) NOT NULL,
	[fldname] [nvarchar](500) NOT NULL,
	[fldprice] [decimal](18, 2) NOT NULL,
	[flddateOn] [datetime] NOT NULL,
	[fldactive] [bit] NOT NULL,
	[id_fktypeProduct] [int] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[id_product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_c_typeProduct] FOREIGN KEY([id_fktypeProduct])
REFERENCES [dbo].[c_typeProduct] ([id_typeProduct])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_c_typeProduct]
GO
```
