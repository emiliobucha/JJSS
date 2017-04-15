USE [JJSS]
GO
/****** Object:  Table [dbo].[faja]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[faja](
	[id_faja] [int] IDENTITY(1,1) NOT NULL,
	[color] [nvarchar](45) NULL,
	[grado] [int] NULL,
 CONSTRAINT [PK_faja_id_faja] PRIMARY KEY CLUSTERED 
(
	[id_faja] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.faja' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'faja'
GO
/****** Object:  Table [dbo].[estado]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado](
	[id_estado] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_estado_id_estado] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'estado'
GO
/****** Object:  Table [dbo].[barrio]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barrio](
	[id_barrio] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
 CONSTRAINT [PK_barrio_id_barrio] PRIMARY KEY CLUSTERED 
(
	[id_barrio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.barrio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'barrio'
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
	[peso_desde] [int] NULL,
	[peso_hasta] [int] NULL,
	[edad_desde] [int] NULL,
	[edad_hasta] [int] NULL,
 CONSTRAINT [PK_categoria_id_categoria] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.categoria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'categoria'
GO
/****** Object:  Table [dbo].[resultado]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[resultado](
	[id_resultado] [int] IDENTITY(1,1) NOT NULL,
	[tipo_finalizacion] [smallint] NULL,
	[tiempo] [varchar](10) NULL,
	[punto_participante_1] [int] NULL,
	[punto_participante_2] [int] NULL,
	[id_ganador] [int] NULL,
 CONSTRAINT [PK_resultado_id_resultado] PRIMARY KEY CLUSTERED 
(
	[id_resultado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.resultado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'resultado'
GO
/****** Object:  Table [dbo].[pais]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pais](
	[id_pais] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
 CONSTRAINT [PK_pais_id_pais] PRIMARY KEY CLUSTERED 
(
	[id_pais] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.pais' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pais'
GO
/****** Object:  Table [dbo].[ciudad]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ciudad](
	[id_ciudad] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
	[id_pais] [int] NULL,
 CONSTRAINT [PK_ciudad_id_ciudad] PRIMARY KEY CLUSTERED 
(
	[id_ciudad] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.ciudad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ciudad'
GO
/****** Object:  Table [dbo].[categoria_torneo]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria_torneo](
	[id_categoria_torneo] [int] IDENTITY(1,1) NOT NULL,
	[id_categoria] [int] NULL,
	[sexo] [smallint] NULL,
	[id_faja] [int] NULL,
 CONSTRAINT [PK_categoria_torneo_id_categoria_torneo] PRIMARY KEY CLUSTERED 
(
	[id_categoria_torneo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.categoria_torneo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'categoria_torneo'
GO
/****** Object:  Table [dbo].[calle]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[calle](
	[id_calle] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
	[id_ciudad] [int] NULL,
 CONSTRAINT [PK_calle_id_calle] PRIMARY KEY CLUSTERED 
(
	[id_calle] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.calle' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'calle'
GO
/****** Object:  Table [dbo].[direccion]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[direccion](
	[id_direccion] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NULL,
	[id_calle] [int] NULL,
	[id_barrio] [int] NULL,
 CONSTRAINT [PK_direccion_id_direccion] PRIMARY KEY CLUSTERED 
(
	[id_direccion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.direccion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'direccion'
GO
/****** Object:  Table [dbo].[academia]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[academia](
	[id_academia] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
	[telefono] [int] NULL,
	[id_direccion] [int] NULL,
 CONSTRAINT [PK_academia_id_academia] PRIMARY KEY CLUSTERED 
(
	[id_academia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.academia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'academia'
GO
/****** Object:  Table [dbo].[sede]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sede](
	[id_sede] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NULL,
	[id_direccion] [int] NULL,
 CONSTRAINT [PK_sede_id_sede] PRIMARY KEY CLUSTERED 
(
	[id_sede] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.sede' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sede'
GO
/****** Object:  Table [dbo].[torneo]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[torneo](
	[id_torneo] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[nombre] [nvarchar](45) NOT NULL,
	[hora] [varchar](10) NULL,
	[precio] [real] NULL,
	[id_estado] [int] NULL,
	[id_sede] [int] NULL,
 CONSTRAINT [PK_torneo_id_torneo] PRIMARY KEY CLUSTERED 
(
	[id_torneo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.torneo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'torneo'
GO
/****** Object:  Table [dbo].[participante]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[participante](
	[id_participante] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NOT NULL,
	[apellido] [nvarchar](45) NOT NULL,
	[fecha_nacimiento] [date] NULL,
	[telefono] [int] NULL,
	[id_faja] [int] NULL,
	[id_direccion] [int] NULL,
	[id_academia] [int] NULL,
	[id_categoria] [int] NULL,
	[sexo] [smallint] NULL,
	[peso] [real] NULL,
 CONSTRAINT [PK_participante_id_participante] PRIMARY KEY CLUSTERED 
(
	[id_participante] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.participante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'participante'
GO
/****** Object:  Table [dbo].[lucha]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lucha](
	[id_lucha] [int] IDENTITY(1,1) NOT NULL,
	[id_participante1] [int] NOT NULL,
	[id_participante2] [int] NOT NULL,
	[id_resultado] [int] NULL,
	[ronda] [int] NULL,
	[id_torneo] [int] NULL,
 CONSTRAINT [PK_lucha_id_lucha] PRIMARY KEY CLUSTERED 
(
	[id_lucha] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.lucha' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'lucha'
GO
/****** Object:  Table [dbo].[inscripcion]    Script Date: 04/15/2017 01:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[inscripcion](
	[id_inscripcion] [int] IDENTITY(1,1) NOT NULL,
	[hora] [varchar](10) NULL,
	[fecha] [date] NULL,
	[codigo_barra] [bigint] NULL,
	[id_participante] [int] NULL,
	[id_torneo] [int] NULL,
	[id_categoria_torneo] [int] NULL,
 CONSTRAINT [PK_inscripcion_id_inscripcion] PRIMARY KEY CLUSTERED 
(
	[id_inscripcion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.inscripcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'inscripcion'
GO
/****** Object:  Default [DF__academia__nombre__07F6335A]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[academia] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__academia__telefo__08EA5793]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[academia] ADD  DEFAULT (NULL) FOR [telefono]
GO
/****** Object:  Default [DF__academia__id_dir__09DE7BCC]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[academia] ADD  DEFAULT (NULL) FOR [id_direccion]
GO
/****** Object:  Default [DF__barrio__nombre__0BC6C43E]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[barrio] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__calle__nombre__0DAF0CB0]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[calle] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__calle__id_ciudad__0EA330E9]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[calle] ADD  DEFAULT (NULL) FOR [id_ciudad]
GO
/****** Object:  Default [DF__categoria__nombr__108B795B]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__categoria__peso___117F9D94]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [peso_desde]
GO
/****** Object:  Default [DF__categoria__peso___1273C1CD]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [peso_hasta]
GO
/****** Object:  Default [DF__categoria__edad___1367E606]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [edad_desde]
GO
/****** Object:  Default [DF__categoria__edad___145C0A3F]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [edad_hasta]
GO
/****** Object:  Default [DF__categoria__id_ca__164452B1]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria_torneo] ADD  DEFAULT (NULL) FOR [id_categoria]
GO
/****** Object:  Default [DF__categoria___sexo__173876EA]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria_torneo] ADD  DEFAULT (NULL) FOR [sexo]
GO
/****** Object:  Default [DF__categoria__id_fa__182C9B23]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria_torneo] ADD  DEFAULT (NULL) FOR [id_faja]
GO
/****** Object:  Default [DF__ciudad__nombre__1A14E395]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[ciudad] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__ciudad__id_pais__1B0907CE]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[ciudad] ADD  DEFAULT (NULL) FOR [id_pais]
GO
/****** Object:  Default [DF__direccion__numer__1CF15040]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[direccion] ADD  DEFAULT (NULL) FOR [numero]
GO
/****** Object:  Default [DF__direccion__id_ca__1DE57479]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[direccion] ADD  DEFAULT (NULL) FOR [id_calle]
GO
/****** Object:  Default [DF__direccion__id_ba__1ED998B2]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[direccion] ADD  DEFAULT (NULL) FOR [id_barrio]
GO
/****** Object:  Default [DF__faja__color__21B6055D]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[faja] ADD  DEFAULT (NULL) FOR [color]
GO
/****** Object:  Default [DF__faja__grado__22AA2996]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[faja] ADD  DEFAULT (NULL) FOR [grado]
GO
/****** Object:  Default [DF__inscripcio__hora__24927208]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [hora]
GO
/****** Object:  Default [DF__inscripci__fecha__25869641]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [fecha]
GO
/****** Object:  Default [DF__inscripci__codig__267ABA7A]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [codigo_barra]
GO
/****** Object:  Default [DF__inscripci__id_pa__276EDEB3]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [id_participante]
GO
/****** Object:  Default [DF__inscripci__id_to__286302EC]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [id_torneo]
GO
/****** Object:  Default [DF__inscripci__id_ca__29572725]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [id_categoria_torneo]
GO
/****** Object:  Default [DF__lucha__id_result__2B3F6F97]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha] ADD  DEFAULT (NULL) FOR [id_resultado]
GO
/****** Object:  Default [DF__lucha__ronda__2C3393D0]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha] ADD  DEFAULT (NULL) FOR [ronda]
GO
/****** Object:  Default [DF__lucha__id_torneo__2D27B809]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha] ADD  DEFAULT (NULL) FOR [id_torneo]
GO
/****** Object:  Default [DF__pais__nombre__2F10007B]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[pais] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__participa__fecha__30F848ED]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [fecha_nacimiento]
GO
/****** Object:  Default [DF__participa__telef__31EC6D26]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [telefono]
GO
/****** Object:  Default [DF__participa__id_fa__32E0915F]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_faja]
GO
/****** Object:  Default [DF__participa__id_di__33D4B598]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_direccion]
GO
/****** Object:  Default [DF__participa__id_ac__34C8D9D1]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_academia]
GO
/****** Object:  Default [DF__participa__id_ca__35BCFE0A]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_categoria]
GO
/****** Object:  Default [DF__participan__sexo__36B12243]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [sexo]
GO
/****** Object:  Default [DF__participan__peso__37A5467C]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [peso]
GO
/****** Object:  Default [DF__resultado__tipo___398D8EEE]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [tipo_finalizacion]
GO
/****** Object:  Default [DF__resultado__tiemp__3A81B327]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [tiempo]
GO
/****** Object:  Default [DF__resultado__punto__3B75D760]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [punto_participante_1]
GO
/****** Object:  Default [DF__resultado__punto__3C69FB99]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [punto_participante_2]
GO
/****** Object:  Default [DF__resultado__id_ga__3D5E1FD2]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [id_ganador]
GO
/****** Object:  Default [DF__sede__nombre__3F466844]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[sede] ADD  DEFAULT (NULL) FOR [nombre]
GO
/****** Object:  Default [DF__sede__id_direcci__403A8C7D]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[sede] ADD  DEFAULT (NULL) FOR [id_direccion]
GO
/****** Object:  Default [DF__torneo__fecha__4222D4EF]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [fecha]
GO
/****** Object:  Default [DF__torneo__hora__4316F928]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [hora]
GO
/****** Object:  Default [DF__torneo__precio__440B1D61]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [precio]
GO
/****** Object:  Default [DF__torneo__id_estad__44FF419A]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [id_estado]
GO
/****** Object:  Default [DF__torneo__id_sede__45F365D3]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [id_sede]
GO
/****** Object:  ForeignKey [academia$academia_id_direccion_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[academia]  WITH CHECK ADD  CONSTRAINT [academia$academia_id_direccion_fk] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id_direccion])
GO
ALTER TABLE [dbo].[academia] CHECK CONSTRAINT [academia$academia_id_direccion_fk]
GO
/****** Object:  ForeignKey [calle$calle_id_ciudad_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[calle]  WITH CHECK ADD  CONSTRAINT [calle$calle_id_ciudad_fk] FOREIGN KEY([id_ciudad])
REFERENCES [dbo].[ciudad] ([id_ciudad])
GO
ALTER TABLE [dbo].[calle] CHECK CONSTRAINT [calle$calle_id_ciudad_fk]
GO
/****** Object:  ForeignKey [categoria_torneo$categoria_torneo_id_categoria_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria_torneo]  WITH CHECK ADD  CONSTRAINT [categoria_torneo$categoria_torneo_id_categoria_fk] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[categoria_torneo] CHECK CONSTRAINT [categoria_torneo$categoria_torneo_id_categoria_fk]
GO
/****** Object:  ForeignKey [categoria_torneo$categoria_torneo_id_faja_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[categoria_torneo]  WITH CHECK ADD  CONSTRAINT [categoria_torneo$categoria_torneo_id_faja_fk] FOREIGN KEY([id_faja])
REFERENCES [dbo].[faja] ([id_faja])
GO
ALTER TABLE [dbo].[categoria_torneo] CHECK CONSTRAINT [categoria_torneo$categoria_torneo_id_faja_fk]
GO
/****** Object:  ForeignKey [ciudad$ciudad_id_pais_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[ciudad]  WITH CHECK ADD  CONSTRAINT [ciudad$ciudad_id_pais_fk] FOREIGN KEY([id_pais])
REFERENCES [dbo].[pais] ([id_pais])
GO
ALTER TABLE [dbo].[ciudad] CHECK CONSTRAINT [ciudad$ciudad_id_pais_fk]
GO
/****** Object:  ForeignKey [direccion$direccion_id_barrio_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[direccion]  WITH CHECK ADD  CONSTRAINT [direccion$direccion_id_barrio_fk] FOREIGN KEY([id_barrio])
REFERENCES [dbo].[barrio] ([id_barrio])
GO
ALTER TABLE [dbo].[direccion] CHECK CONSTRAINT [direccion$direccion_id_barrio_fk]
GO
/****** Object:  ForeignKey [direccion$direccion_id_calle_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[direccion]  WITH CHECK ADD  CONSTRAINT [direccion$direccion_id_calle_fk] FOREIGN KEY([id_calle])
REFERENCES [dbo].[calle] ([id_calle])
GO
ALTER TABLE [dbo].[direccion] CHECK CONSTRAINT [direccion$direccion_id_calle_fk]
GO
/****** Object:  ForeignKey [inscripcion$inscripcion_id_categoria_torneo_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion]  WITH CHECK ADD  CONSTRAINT [inscripcion$inscripcion_id_categoria_torneo_fk] FOREIGN KEY([id_categoria_torneo])
REFERENCES [dbo].[categoria_torneo] ([id_categoria_torneo])
GO
ALTER TABLE [dbo].[inscripcion] CHECK CONSTRAINT [inscripcion$inscripcion_id_categoria_torneo_fk]
GO
/****** Object:  ForeignKey [inscripcion$inscripcion_id_participante_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion]  WITH CHECK ADD  CONSTRAINT [inscripcion$inscripcion_id_participante_fk] FOREIGN KEY([id_participante])
REFERENCES [dbo].[participante] ([id_participante])
GO
ALTER TABLE [dbo].[inscripcion] CHECK CONSTRAINT [inscripcion$inscripcion_id_participante_fk]
GO
/****** Object:  ForeignKey [inscripcion$inscripcion_id_torneo_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[inscripcion]  WITH CHECK ADD  CONSTRAINT [inscripcion$inscripcion_id_torneo_fk] FOREIGN KEY([id_torneo])
REFERENCES [dbo].[torneo] ([id_torneo])
GO
ALTER TABLE [dbo].[inscripcion] CHECK CONSTRAINT [inscripcion$inscripcion_id_torneo_fk]
GO
/****** Object:  ForeignKey [lucha$lucha_id_participante_1_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_participante_1_fk] FOREIGN KEY([id_participante1])
REFERENCES [dbo].[participante] ([id_participante])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_participante_1_fk]
GO
/****** Object:  ForeignKey [lucha$lucha_id_participante_2_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_participante_2_fk] FOREIGN KEY([id_participante2])
REFERENCES [dbo].[participante] ([id_participante])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_participante_2_fk]
GO
/****** Object:  ForeignKey [lucha$lucha_id_resultado_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_resultado_fk] FOREIGN KEY([id_resultado])
REFERENCES [dbo].[resultado] ([id_resultado])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_resultado_fk]
GO
/****** Object:  ForeignKey [lucha$lucha_id_torneo_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_torneo_fk] FOREIGN KEY([id_torneo])
REFERENCES [dbo].[torneo] ([id_torneo])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_torneo_fk]
GO
/****** Object:  ForeignKey [participante$participante_id_academia_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_academia_fk] FOREIGN KEY([id_academia])
REFERENCES [dbo].[academia] ([id_academia])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_academia_fk]
GO
/****** Object:  ForeignKey [participante$participante_id_categoria_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_categoria_fk] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_categoria_fk]
GO
/****** Object:  ForeignKey [participante$participante_id_direccion_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_direccion_fk] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id_direccion])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_direccion_fk]
GO
/****** Object:  ForeignKey [participante$participante_id_faja_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_faja_fk] FOREIGN KEY([id_faja])
REFERENCES [dbo].[faja] ([id_faja])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_faja_fk]
GO
/****** Object:  ForeignKey [sede$sede_id_direccion_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[sede]  WITH CHECK ADD  CONSTRAINT [sede$sede_id_direccion_fk] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id_direccion])
GO
ALTER TABLE [dbo].[sede] CHECK CONSTRAINT [sede$sede_id_direccion_fk]
GO
/****** Object:  ForeignKey [torneo$torneo_id_estado_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo]  WITH CHECK ADD  CONSTRAINT [torneo$torneo_id_estado_fk] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estado] ([id_estado])
GO
ALTER TABLE [dbo].[torneo] CHECK CONSTRAINT [torneo$torneo_id_estado_fk]
GO
/****** Object:  ForeignKey [torneo$torneo_id_sede_fk]    Script Date: 04/15/2017 01:04:12 ******/
ALTER TABLE [dbo].[torneo]  WITH CHECK ADD  CONSTRAINT [torneo$torneo_id_sede_fk] FOREIGN KEY([id_sede])
REFERENCES [dbo].[sede] ([id_sede])
GO
ALTER TABLE [dbo].[torneo] CHECK CONSTRAINT [torneo$torneo_id_sede_fk]
GO
