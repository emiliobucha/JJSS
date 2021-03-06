USE [master]
GO
/****** Object:  Database [JJSS]    Script Date: 15/04/2017 23:59:25 ******/
CREATE DATABASE [JJSS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JJSS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\JJSS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'JJSS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\JJSS_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [JJSS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JJSS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JJSS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JJSS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JJSS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JJSS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JJSS] SET ARITHABORT OFF 
GO
ALTER DATABASE [JJSS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JJSS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [JJSS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JJSS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JJSS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JJSS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JJSS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JJSS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JJSS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JJSS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JJSS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JJSS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JJSS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JJSS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JJSS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JJSS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JJSS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JJSS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JJSS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [JJSS] SET  MULTI_USER 
GO
ALTER DATABASE [JJSS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JJSS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JJSS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JJSS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [JJSS]
GO
/****** Object:  Table [dbo].[academia]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[barrio]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[calle]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categoria]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categoria_torneo]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ciudad]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[direccion]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[estado]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[faja]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[inscripcion]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[lucha]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pais]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[participante]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[resultado]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sede]    Script Date: 15/04/2017 23:59:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[torneo]    Script Date: 15/04/2017 23:59:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[torneo](
	[id_torneo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](45) NOT NULL,
	[precio_absoluto] [real] NULL,
	[id_estado] [int] NULL,
	[id_sede] [int] NULL,
	[precio_categoria] [real] NULL,
	[fecha] [date] NULL,
	[fecha_cierre] [date] NULL,
	[hora] [nchar](10) NULL,
	[hora_cierre] [nchar](10) NULL,
 CONSTRAINT [PK_torneo_id_torneo] PRIMARY KEY CLUSTERED 
(
	[id_torneo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[academia] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[academia] ADD  DEFAULT (NULL) FOR [telefono]
GO
ALTER TABLE [dbo].[academia] ADD  DEFAULT (NULL) FOR [id_direccion]
GO
ALTER TABLE [dbo].[barrio] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[calle] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[calle] ADD  DEFAULT (NULL) FOR [id_ciudad]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [peso_desde]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [peso_hasta]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [edad_desde]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT (NULL) FOR [edad_hasta]
GO
ALTER TABLE [dbo].[categoria_torneo] ADD  DEFAULT (NULL) FOR [id_categoria]
GO
ALTER TABLE [dbo].[categoria_torneo] ADD  DEFAULT (NULL) FOR [sexo]
GO
ALTER TABLE [dbo].[categoria_torneo] ADD  DEFAULT (NULL) FOR [id_faja]
GO
ALTER TABLE [dbo].[ciudad] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[ciudad] ADD  DEFAULT (NULL) FOR [id_pais]
GO
ALTER TABLE [dbo].[direccion] ADD  DEFAULT (NULL) FOR [numero]
GO
ALTER TABLE [dbo].[direccion] ADD  DEFAULT (NULL) FOR [id_calle]
GO
ALTER TABLE [dbo].[direccion] ADD  DEFAULT (NULL) FOR [id_barrio]
GO
ALTER TABLE [dbo].[faja] ADD  DEFAULT (NULL) FOR [color]
GO
ALTER TABLE [dbo].[faja] ADD  DEFAULT (NULL) FOR [grado]
GO
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [hora]
GO
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [fecha]
GO
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [codigo_barra]
GO
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [id_participante]
GO
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [id_torneo]
GO
ALTER TABLE [dbo].[inscripcion] ADD  DEFAULT (NULL) FOR [id_categoria_torneo]
GO
ALTER TABLE [dbo].[lucha] ADD  DEFAULT (NULL) FOR [id_resultado]
GO
ALTER TABLE [dbo].[lucha] ADD  DEFAULT (NULL) FOR [ronda]
GO
ALTER TABLE [dbo].[lucha] ADD  DEFAULT (NULL) FOR [id_torneo]
GO
ALTER TABLE [dbo].[pais] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [fecha_nacimiento]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [telefono]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_faja]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_direccion]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_academia]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [id_categoria]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [sexo]
GO
ALTER TABLE [dbo].[participante] ADD  DEFAULT (NULL) FOR [peso]
GO
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [tipo_finalizacion]
GO
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [tiempo]
GO
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [punto_participante_1]
GO
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [punto_participante_2]
GO
ALTER TABLE [dbo].[resultado] ADD  DEFAULT (NULL) FOR [id_ganador]
GO
ALTER TABLE [dbo].[sede] ADD  DEFAULT (NULL) FOR [nombre]
GO
ALTER TABLE [dbo].[sede] ADD  DEFAULT (NULL) FOR [id_direccion]
GO
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [precio_absoluto]
GO
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [id_estado]
GO
ALTER TABLE [dbo].[torneo] ADD  DEFAULT (NULL) FOR [id_sede]
GO
ALTER TABLE [dbo].[academia]  WITH CHECK ADD  CONSTRAINT [academia$academia_id_direccion_fk] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id_direccion])
GO
ALTER TABLE [dbo].[academia] CHECK CONSTRAINT [academia$academia_id_direccion_fk]
GO
ALTER TABLE [dbo].[calle]  WITH CHECK ADD  CONSTRAINT [calle$calle_id_ciudad_fk] FOREIGN KEY([id_ciudad])
REFERENCES [dbo].[ciudad] ([id_ciudad])
GO
ALTER TABLE [dbo].[calle] CHECK CONSTRAINT [calle$calle_id_ciudad_fk]
GO
ALTER TABLE [dbo].[categoria_torneo]  WITH CHECK ADD  CONSTRAINT [categoria_torneo$categoria_torneo_id_categoria_fk] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[categoria_torneo] CHECK CONSTRAINT [categoria_torneo$categoria_torneo_id_categoria_fk]
GO
ALTER TABLE [dbo].[categoria_torneo]  WITH CHECK ADD  CONSTRAINT [categoria_torneo$categoria_torneo_id_faja_fk] FOREIGN KEY([id_faja])
REFERENCES [dbo].[faja] ([id_faja])
GO
ALTER TABLE [dbo].[categoria_torneo] CHECK CONSTRAINT [categoria_torneo$categoria_torneo_id_faja_fk]
GO
ALTER TABLE [dbo].[ciudad]  WITH CHECK ADD  CONSTRAINT [ciudad$ciudad_id_pais_fk] FOREIGN KEY([id_pais])
REFERENCES [dbo].[pais] ([id_pais])
GO
ALTER TABLE [dbo].[ciudad] CHECK CONSTRAINT [ciudad$ciudad_id_pais_fk]
GO
ALTER TABLE [dbo].[direccion]  WITH CHECK ADD  CONSTRAINT [direccion$direccion_id_barrio_fk] FOREIGN KEY([id_barrio])
REFERENCES [dbo].[barrio] ([id_barrio])
GO
ALTER TABLE [dbo].[direccion] CHECK CONSTRAINT [direccion$direccion_id_barrio_fk]
GO
ALTER TABLE [dbo].[direccion]  WITH CHECK ADD  CONSTRAINT [direccion$direccion_id_calle_fk] FOREIGN KEY([id_calle])
REFERENCES [dbo].[calle] ([id_calle])
GO
ALTER TABLE [dbo].[direccion] CHECK CONSTRAINT [direccion$direccion_id_calle_fk]
GO
ALTER TABLE [dbo].[inscripcion]  WITH CHECK ADD  CONSTRAINT [inscripcion$inscripcion_id_categoria_torneo_fk] FOREIGN KEY([id_categoria_torneo])
REFERENCES [dbo].[categoria_torneo] ([id_categoria_torneo])
GO
ALTER TABLE [dbo].[inscripcion] CHECK CONSTRAINT [inscripcion$inscripcion_id_categoria_torneo_fk]
GO
ALTER TABLE [dbo].[inscripcion]  WITH CHECK ADD  CONSTRAINT [inscripcion$inscripcion_id_participante_fk] FOREIGN KEY([id_participante])
REFERENCES [dbo].[participante] ([id_participante])
GO
ALTER TABLE [dbo].[inscripcion] CHECK CONSTRAINT [inscripcion$inscripcion_id_participante_fk]
GO
ALTER TABLE [dbo].[inscripcion]  WITH CHECK ADD  CONSTRAINT [inscripcion$inscripcion_id_torneo_fk] FOREIGN KEY([id_torneo])
REFERENCES [dbo].[torneo] ([id_torneo])
GO
ALTER TABLE [dbo].[inscripcion] CHECK CONSTRAINT [inscripcion$inscripcion_id_torneo_fk]
GO
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_participante_1_fk] FOREIGN KEY([id_participante1])
REFERENCES [dbo].[participante] ([id_participante])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_participante_1_fk]
GO
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_participante_2_fk] FOREIGN KEY([id_participante2])
REFERENCES [dbo].[participante] ([id_participante])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_participante_2_fk]
GO
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_resultado_fk] FOREIGN KEY([id_resultado])
REFERENCES [dbo].[resultado] ([id_resultado])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_resultado_fk]
GO
ALTER TABLE [dbo].[lucha]  WITH CHECK ADD  CONSTRAINT [lucha$lucha_id_torneo_fk] FOREIGN KEY([id_torneo])
REFERENCES [dbo].[torneo] ([id_torneo])
GO
ALTER TABLE [dbo].[lucha] CHECK CONSTRAINT [lucha$lucha_id_torneo_fk]
GO
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_academia_fk] FOREIGN KEY([id_academia])
REFERENCES [dbo].[academia] ([id_academia])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_academia_fk]
GO
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_categoria_fk] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_categoria_fk]
GO
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_direccion_fk] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id_direccion])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_direccion_fk]
GO
ALTER TABLE [dbo].[participante]  WITH CHECK ADD  CONSTRAINT [participante$participante_id_faja_fk] FOREIGN KEY([id_faja])
REFERENCES [dbo].[faja] ([id_faja])
GO
ALTER TABLE [dbo].[participante] CHECK CONSTRAINT [participante$participante_id_faja_fk]
GO
ALTER TABLE [dbo].[sede]  WITH CHECK ADD  CONSTRAINT [sede$sede_id_direccion_fk] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id_direccion])
GO
ALTER TABLE [dbo].[sede] CHECK CONSTRAINT [sede$sede_id_direccion_fk]
GO
ALTER TABLE [dbo].[torneo]  WITH CHECK ADD  CONSTRAINT [torneo$torneo_id_estado_fk] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estado] ([id_estado])
GO
ALTER TABLE [dbo].[torneo] CHECK CONSTRAINT [torneo$torneo_id_estado_fk]
GO
ALTER TABLE [dbo].[torneo]  WITH CHECK ADD  CONSTRAINT [torneo$torneo_id_sede_fk] FOREIGN KEY([id_sede])
REFERENCES [dbo].[sede] ([id_sede])
GO
ALTER TABLE [dbo].[torneo] CHECK CONSTRAINT [torneo$torneo_id_sede_fk]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.academia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'academia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.barrio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'barrio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.calle' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'calle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.categoria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'categoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.categoria_torneo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'categoria_torneo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.ciudad' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ciudad'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.direccion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'direccion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'estado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.faja' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'faja'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.inscripcion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'inscripcion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.lucha' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'lucha'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.pais' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'pais'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.participante' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'participante'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.resultado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'resultado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.sede' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sede'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'jjss.torneo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'torneo'
GO
USE [master]
GO
ALTER DATABASE [JJSS] SET  READ_WRITE 
GO
