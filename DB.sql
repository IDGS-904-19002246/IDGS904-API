USE [IDGS904_API]
GO
/****** Object:  Table [dbo].[tbl_insumo_producto]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_insumo_producto](
	[fk_id_insumo] [int] NOT NULL,
	[fk_id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_insumo_proveedor]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_insumo_proveedor](
	[fk_id_insumo] [int] NOT NULL,
	[fk_id_proveedor] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [int] NOT NULL,
	[fecha] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_insumos]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_insumos](
	[id_insumo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](32) NOT NULL,
	[cantidad] [int] NOT NULL,
	[cantidad_min] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_insumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_productos]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_productos](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](32) NOT NULL,
	[precio] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[cantidad_min] [int] NOT NULL,
	[img] [nvarchar](max) NULL,
	[descripcion] [varchar](64) NULL,
	[estado] [varchar](4) NULL,
	[pendientes] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_proveedores]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_proveedores](
	[id_proveedor] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](32) NOT NULL,
	[correo] [varchar](32) NOT NULL,
	[telefono] [varchar](15) NOT NULL,
	[direccion] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_usuarios]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](64) NOT NULL,
	[apellidoP] [varchar](64) NOT NULL,
	[apellidoM] [varchar](64) NOT NULL,
	[correo] [varchar](64) NOT NULL,
	[contrasena] [varchar](64) NOT NULL,
	[rol] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_venta_producto]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_venta_producto](
	[id_venta_producto] [int] IDENTITY(1,1) NOT NULL,
	[fk_id_venta] [int] NOT NULL,
	[fk_id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_venta_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_ventas]    Script Date: 2023-07-21 08:18:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ventas](
	[id_venta] [int] IDENTITY(1,1) NOT NULL,
	[fk_id_usuario] [int] NOT NULL,
	[fecha_compra] [date] NOT NULL,
	[status] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_productos] ADD  DEFAULT ((0)) FOR [cantidad]
GO
ALTER TABLE [dbo].[tbl_productos] ADD  DEFAULT ((0)) FOR [cantidad_min]
GO
ALTER TABLE [dbo].[tbl_insumo_producto]  WITH CHECK ADD FOREIGN KEY([fk_id_insumo])
REFERENCES [dbo].[tbl_insumos] ([id_insumo])
GO
ALTER TABLE [dbo].[tbl_insumo_producto]  WITH CHECK ADD FOREIGN KEY([fk_id_producto])
REFERENCES [dbo].[tbl_productos] ([id_producto])
GO
ALTER TABLE [dbo].[tbl_insumo_proveedor]  WITH CHECK ADD FOREIGN KEY([fk_id_insumo])
REFERENCES [dbo].[tbl_insumos] ([id_insumo])
GO
ALTER TABLE [dbo].[tbl_insumo_proveedor]  WITH CHECK ADD FOREIGN KEY([fk_id_proveedor])
REFERENCES [dbo].[tbl_proveedores] ([id_proveedor])
GO
ALTER TABLE [dbo].[tbl_venta_producto]  WITH CHECK ADD FOREIGN KEY([fk_id_venta])
REFERENCES [dbo].[tbl_ventas] ([id_venta])
GO
ALTER TABLE [dbo].[tbl_venta_producto]  WITH CHECK ADD FOREIGN KEY([fk_id_producto])
REFERENCES [dbo].[tbl_productos] ([id_producto])
GO
ALTER TABLE [dbo].[tbl_ventas]  WITH CHECK ADD FOREIGN KEY([fk_id_usuario])
REFERENCES [dbo].[tbl_usuarios] ([id_usuario])
GO
