/*
Navicat SQL Server Data Transfer

Source Server         : SQLServer
Source Server Version : 100000
Source Host           : MELINA-PC\SQLEXPRESS:1433
Source Database       : JJSS
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 100000
File Encoding         : 65001

Date: 2017-04-20 00:51:42
*/


-- ----------------------------
-- Table structure for academia
-- ----------------------------
DROP TABLE [dbo].[academia]
GO
CREATE TABLE [dbo].[academia] (
[id_academia] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[telefono] int NULL DEFAULT NULL ,
[id_direccion] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of academia
-- ----------------------------
SET IDENTITY_INSERT [dbo].[academia] ON
GO
SET IDENTITY_INSERT [dbo].[academia] OFF
GO

-- ----------------------------
-- Table structure for barrio
-- ----------------------------
DROP TABLE [dbo].[barrio]
GO
CREATE TABLE [dbo].[barrio] (
[id_barrio] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of barrio
-- ----------------------------
SET IDENTITY_INSERT [dbo].[barrio] ON
GO
SET IDENTITY_INSERT [dbo].[barrio] OFF
GO

-- ----------------------------
-- Table structure for calle
-- ----------------------------
DROP TABLE [dbo].[calle]
GO
CREATE TABLE [dbo].[calle] (
[id_calle] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[id_ciudad] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of calle
-- ----------------------------
SET IDENTITY_INSERT [dbo].[calle] ON
GO
SET IDENTITY_INSERT [dbo].[calle] OFF
GO

-- ----------------------------
-- Table structure for categoria
-- ----------------------------
DROP TABLE [dbo].[categoria]
GO
CREATE TABLE [dbo].[categoria] (
[id_categoria] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[peso_desde] float(53) NULL DEFAULT NULL ,
[peso_hasta] float(53) NULL DEFAULT NULL ,
[edad_desde] int NULL DEFAULT NULL ,
[edad_hasta] int NULL DEFAULT NULL ,
[sexo] smallint NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[categoria]', RESEED, 34)
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'categoria', 
'COLUMN', N'sexo')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'Mujer Hombre'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'categoria'
, @level2type = 'COLUMN', @level2name = N'sexo'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Mujer Hombre'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'categoria'
, @level2type = 'COLUMN', @level2name = N'sexo'
GO

-- ----------------------------
-- Records of categoria
-- ----------------------------
SET IDENTITY_INSERT [dbo].[categoria] ON
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'1', N'Rooster Juvenile Male', N'0', N'53.5', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'2', N'Light Feather Juvenile Male', N'53.5', N'58.5', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'3', N'Feather Juvenile Male', N'58.5', N'64', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'4', N'Light Juvenile Male', N'64', N'69', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'5', N'Middle Juvenile Male', N'69', N'74', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'6', N'Medium Heavy Juvenile Male', N'74', N'79.3', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'7', N'Heavy Juvenile Male', N'79', N'84.3', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'8', N'Super Heavy Juvenile Male', N'84.3', N'89.3', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'9', N'Ultra Heavy Juvenile Male', N'89.3', N'999', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'10', N'Open Class Juvenile Male', N'69', N'999', N'0', N'16', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'11', N'Rooster Adult, Master and Senior Male', N'0', N'57.5', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'12', N'Light Feather Adult, Master and Senior Male', N'57.5', N'64', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'13', N'Feather Adult, Master and Senior Male', N'64', N'70', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'14', N'Light Adult, Master and Senior Male', N'70', N'76', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'15', N'Middle Adult, Master and Senior Male', N'76', N'82.3', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'16', N'Medium Heavy Adult, Master and Senior Male', N'82.3', N'88.3', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'17', N'Heavy Adult, Master and Senior Male', N'88.3', N'94.3', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'18', N'Super Heavy Adult, Master and Senior Male', N'94.3', N'100.5', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'19', N'Ultra Heavy Adult, Master and Senior Male', N'100.5', N'999', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'20', N'Open Class Adult, Master and Senior Male', N'0', N'999', N'17', N'99', N'1')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'21', N'Light Feather Adult, Master and Senior Female', N'0', N'53.5', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'22', N'Feather Adult, Master and Senior Female', N'53.5', N'58.5', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'23', N'Light Adult, Master and Senior Female', N'58.5', N'64', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'24', N'Middle Adult, Master and Senior Female', N'64', N'69', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'25', N'Medium Heavy Adult, Master and Senior Female', N'69', N'74', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'26', N'Heavy Adult, Master and Senior Female', N'74', N'999', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'27', N'Open Class Adult, Master and Senior Male', N'0', N'999', N'17', N'99', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'28', N'Light Feather Juvenile Female', N'0', N'48.3', N'0', N'16', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'29', N'Feather Juvenile Female', N'48.3', N'52.5', N'0', N'16', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'30', N'Light Juvenile Female', N'52.5', N'56.5', N'0', N'16', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'31', N'Middle Juvenile Female', N'56.5', N'60.5', N'0', N'16', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'32', N'Medium Heavy Juvenile Female', N'60.5', N'65', N'0', N'16', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'33', N'Heavy Juvenile Female', N'65', N'999', N'0', N'16', N'0')
GO
GO
INSERT INTO [dbo].[categoria] ([id_categoria], [nombre], [peso_desde], [peso_hasta], [edad_desde], [edad_hasta], [sexo]) VALUES (N'34', N'Open Class Juvenile Female', N'60.5', N'999', N'0', N'16', N'0')
GO
GO
SET IDENTITY_INSERT [dbo].[categoria] OFF
GO

-- ----------------------------
-- Table structure for categoria_torneo
-- ----------------------------
DROP TABLE [dbo].[categoria_torneo]
GO
CREATE TABLE [dbo].[categoria_torneo] (
[id_categoria_torneo] int NOT NULL IDENTITY(1,1) ,
[id_categoria] int NULL DEFAULT NULL ,
[sexo] smallint NULL DEFAULT NULL ,
[id_faja] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of categoria_torneo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[categoria_torneo] ON
GO
INSERT INTO [dbo].[categoria_torneo] ([id_categoria_torneo], [id_categoria], [sexo], [id_faja]) VALUES (N'1', N'24', N'0', N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[categoria_torneo] OFF
GO

-- ----------------------------
-- Table structure for ciudad
-- ----------------------------
DROP TABLE [dbo].[ciudad]
GO
CREATE TABLE [dbo].[ciudad] (
[id_ciudad] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[id_pais] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of ciudad
-- ----------------------------
SET IDENTITY_INSERT [dbo].[ciudad] ON
GO
SET IDENTITY_INSERT [dbo].[ciudad] OFF
GO

-- ----------------------------
-- Table structure for direccion
-- ----------------------------
DROP TABLE [dbo].[direccion]
GO
CREATE TABLE [dbo].[direccion] (
[id_direccion] int NOT NULL IDENTITY(1,1) ,
[numero] int NULL DEFAULT NULL ,
[id_calle] int NULL DEFAULT NULL ,
[id_barrio] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of direccion
-- ----------------------------
SET IDENTITY_INSERT [dbo].[direccion] ON
GO
SET IDENTITY_INSERT [dbo].[direccion] OFF
GO

-- ----------------------------
-- Table structure for estado
-- ----------------------------
DROP TABLE [dbo].[estado]
GO
CREATE TABLE [dbo].[estado] (
[id_estado] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[estado]', RESEED, 4)
GO

-- ----------------------------
-- Records of estado
-- ----------------------------
SET IDENTITY_INSERT [dbo].[estado] ON
GO
INSERT INTO [dbo].[estado] ([id_estado], [nombre]) VALUES (N'1', N'InscripcionAbierta')
GO
GO
INSERT INTO [dbo].[estado] ([id_estado], [nombre]) VALUES (N'2', N'InscripcionCerrada')
GO
GO
INSERT INTO [dbo].[estado] ([id_estado], [nombre]) VALUES (N'3', N'EnCurso')
GO
GO
INSERT INTO [dbo].[estado] ([id_estado], [nombre]) VALUES (N'4', N'Finalizado')
GO
GO
SET IDENTITY_INSERT [dbo].[estado] OFF
GO

-- ----------------------------
-- Table structure for faja
-- ----------------------------
DROP TABLE [dbo].[faja]
GO
CREATE TABLE [dbo].[faja] (
[id_faja] int NOT NULL IDENTITY(1,1) ,
[color] nvarchar(45) NULL DEFAULT NULL ,
[grado] int NULL DEFAULT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[faja]', RESEED, 2)
GO

-- ----------------------------
-- Records of faja
-- ----------------------------
SET IDENTITY_INSERT [dbo].[faja] ON
GO
INSERT INTO [dbo].[faja] ([id_faja], [color], [grado]) VALUES (N'1', N'Rojo', N'1')
GO
GO
INSERT INTO [dbo].[faja] ([id_faja], [color], [grado]) VALUES (N'2', N'Azul', N'2')
GO
GO
SET IDENTITY_INSERT [dbo].[faja] OFF
GO

-- ----------------------------
-- Table structure for inscripcion
-- ----------------------------
DROP TABLE [dbo].[inscripcion]
GO
CREATE TABLE [dbo].[inscripcion] (
[id_inscripcion] int NOT NULL IDENTITY(1,1) ,
[hora] varchar(10) NULL DEFAULT NULL ,
[fecha] date NULL DEFAULT NULL ,
[codigo_barra] bigint NULL DEFAULT NULL ,
[id_participante] int NULL DEFAULT NULL ,
[id_torneo] int NULL DEFAULT NULL ,
[id_categoria_torneo] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of inscripcion
-- ----------------------------
SET IDENTITY_INSERT [dbo].[inscripcion] ON
GO
INSERT INTO [dbo].[inscripcion] ([id_inscripcion], [hora], [fecha], [codigo_barra], [id_participante], [id_torneo], [id_categoria_torneo]) VALUES (N'1', N'12:00', N'2017-04-20', N'123456789', N'1', N'1', N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[inscripcion] OFF
GO

-- ----------------------------
-- Table structure for lucha
-- ----------------------------
DROP TABLE [dbo].[lucha]
GO
CREATE TABLE [dbo].[lucha] (
[id_lucha] int NOT NULL IDENTITY(1,1) ,
[id_participante1] int NOT NULL ,
[id_participante2] int NOT NULL ,
[id_resultado] int NULL DEFAULT NULL ,
[ronda] int NULL DEFAULT NULL ,
[id_torneo] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of lucha
-- ----------------------------
SET IDENTITY_INSERT [dbo].[lucha] ON
GO
SET IDENTITY_INSERT [dbo].[lucha] OFF
GO

-- ----------------------------
-- Table structure for pais
-- ----------------------------
DROP TABLE [dbo].[pais]
GO
CREATE TABLE [dbo].[pais] (
[id_pais] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of pais
-- ----------------------------
SET IDENTITY_INSERT [dbo].[pais] ON
GO
SET IDENTITY_INSERT [dbo].[pais] OFF
GO

-- ----------------------------
-- Table structure for participante
-- ----------------------------
DROP TABLE [dbo].[participante]
GO
CREATE TABLE [dbo].[participante] (
[id_participante] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NOT NULL ,
[apellido] nvarchar(45) NOT NULL ,
[fecha_nacimiento] date NULL DEFAULT NULL ,
[telefono] int NULL DEFAULT NULL ,
[id_faja] int NULL DEFAULT NULL ,
[id_direccion] int NULL DEFAULT NULL ,
[id_academia] int NULL DEFAULT NULL ,
[id_categoria] int NULL DEFAULT NULL ,
[sexo] smallint NULL DEFAULT NULL ,
[peso] real NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of participante
-- ----------------------------
SET IDENTITY_INSERT [dbo].[participante] ON
GO
INSERT INTO [dbo].[participante] ([id_participante], [nombre], [apellido], [fecha_nacimiento], [telefono], [id_faja], [id_direccion], [id_academia], [id_categoria], [sexo], [peso]) VALUES (N'1', N'Josesito', N'Peres', N'1997-01-01', null, N'1', null, null, null, N'0', N'66')
GO
GO
SET IDENTITY_INSERT [dbo].[participante] OFF
GO

-- ----------------------------
-- Table structure for resultado
-- ----------------------------
DROP TABLE [dbo].[resultado]
GO
CREATE TABLE [dbo].[resultado] (
[id_resultado] int NOT NULL IDENTITY(1,1) ,
[tipo_finalizacion] smallint NULL DEFAULT NULL ,
[tiempo] varchar(10) NULL DEFAULT NULL ,
[punto_participante_1] int NULL DEFAULT NULL ,
[punto_participante_2] int NULL DEFAULT NULL ,
[id_ganador] int NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of resultado
-- ----------------------------
SET IDENTITY_INSERT [dbo].[resultado] ON
GO
SET IDENTITY_INSERT [dbo].[resultado] OFF
GO

-- ----------------------------
-- Table structure for sede
-- ----------------------------
DROP TABLE [dbo].[sede]
GO
CREATE TABLE [dbo].[sede] (
[id_sede] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[id_direccion] int NULL DEFAULT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[sede]', RESEED, 3)
GO

-- ----------------------------
-- Records of sede
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sede] ON
GO
INSERT INTO [dbo].[sede] ([id_sede], [nombre], [id_direccion]) VALUES (N'1', N'Sucuchito', null)
GO
GO
INSERT INTO [dbo].[sede] ([id_sede], [nombre], [id_direccion]) VALUES (N'2', N'Irak', null)
GO
GO
INSERT INTO [dbo].[sede] ([id_sede], [nombre], [id_direccion]) VALUES (N'3', N'Capitalinas', null)
GO
GO
SET IDENTITY_INSERT [dbo].[sede] OFF
GO

-- ----------------------------
-- Table structure for torneo
-- ----------------------------
DROP TABLE [dbo].[torneo]
GO
CREATE TABLE [dbo].[torneo] (
[id_torneo] int NOT NULL IDENTITY(1,1) ,
[fecha] date NULL DEFAULT NULL ,
[nombre] nvarchar(45) NOT NULL ,
[hora] varchar(10) NULL DEFAULT NULL ,
[id_estado] int NULL DEFAULT NULL ,
[id_sede] int NULL DEFAULT NULL ,
[fecha_cierre] date NULL ,
[hora_cierre] varchar(10) NULL ,
[precio_absoluto] decimal(18) NULL ,
[precio_categoria] decimal(18) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[torneo]', RESEED, 5)
GO

-- ----------------------------
-- Records of torneo
-- ----------------------------
SET IDENTITY_INSERT [dbo].[torneo] ON
GO
INSERT INTO [dbo].[torneo] ([id_torneo], [fecha], [nombre], [hora], [id_estado], [id_sede], [fecha_cierre], [hora_cierre], [precio_absoluto], [precio_categoria]) VALUES (N'1', N'2017-04-20', N'El Torneo con Bronca', N'13:00', N'1', N'2', N'2017-04-27', N'09:00', N'345', N'34')
GO
GO
INSERT INTO [dbo].[torneo] ([id_torneo], [fecha], [nombre], [hora], [id_estado], [id_sede], [fecha_cierre], [hora_cierre], [precio_absoluto], [precio_categoria]) VALUES (N'2', N'2017-04-24', N'El Torneo', N'12:00', N'1', N'2', N'2017-04-30', N'12:00', N'24', N'24')
GO
GO
INSERT INTO [dbo].[torneo] ([id_torneo], [fecha], [nombre], [hora], [id_estado], [id_sede], [fecha_cierre], [hora_cierre], [precio_absoluto], [precio_categoria]) VALUES (N'5', N'2017-04-19', N'Palta Tournament', N'12:00', N'1', N'1', N'2017-04-22', N'12:00', N'1234', N'1243')
GO
GO
SET IDENTITY_INSERT [dbo].[torneo] OFF
GO

-- ----------------------------
-- Indexes structure for table academia
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table academia
-- ----------------------------
ALTER TABLE [dbo].[academia] ADD PRIMARY KEY ([id_academia])
GO

-- ----------------------------
-- Indexes structure for table barrio
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table barrio
-- ----------------------------
ALTER TABLE [dbo].[barrio] ADD PRIMARY KEY ([id_barrio])
GO

-- ----------------------------
-- Indexes structure for table calle
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table calle
-- ----------------------------
ALTER TABLE [dbo].[calle] ADD PRIMARY KEY ([id_calle])
GO

-- ----------------------------
-- Indexes structure for table categoria
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table categoria
-- ----------------------------
ALTER TABLE [dbo].[categoria] ADD PRIMARY KEY ([id_categoria])
GO

-- ----------------------------
-- Indexes structure for table categoria_torneo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table categoria_torneo
-- ----------------------------
ALTER TABLE [dbo].[categoria_torneo] ADD PRIMARY KEY ([id_categoria_torneo])
GO

-- ----------------------------
-- Indexes structure for table ciudad
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ciudad
-- ----------------------------
ALTER TABLE [dbo].[ciudad] ADD PRIMARY KEY ([id_ciudad])
GO

-- ----------------------------
-- Indexes structure for table direccion
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table direccion
-- ----------------------------
ALTER TABLE [dbo].[direccion] ADD PRIMARY KEY ([id_direccion])
GO

-- ----------------------------
-- Indexes structure for table estado
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table estado
-- ----------------------------
ALTER TABLE [dbo].[estado] ADD PRIMARY KEY ([id_estado])
GO

-- ----------------------------
-- Indexes structure for table faja
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table faja
-- ----------------------------
ALTER TABLE [dbo].[faja] ADD PRIMARY KEY ([id_faja])
GO

-- ----------------------------
-- Indexes structure for table inscripcion
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table inscripcion
-- ----------------------------
ALTER TABLE [dbo].[inscripcion] ADD PRIMARY KEY ([id_inscripcion])
GO

-- ----------------------------
-- Indexes structure for table lucha
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table lucha
-- ----------------------------
ALTER TABLE [dbo].[lucha] ADD PRIMARY KEY ([id_lucha])
GO

-- ----------------------------
-- Indexes structure for table pais
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table pais
-- ----------------------------
ALTER TABLE [dbo].[pais] ADD PRIMARY KEY ([id_pais])
GO

-- ----------------------------
-- Indexes structure for table participante
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table participante
-- ----------------------------
ALTER TABLE [dbo].[participante] ADD PRIMARY KEY ([id_participante])
GO

-- ----------------------------
-- Indexes structure for table resultado
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table resultado
-- ----------------------------
ALTER TABLE [dbo].[resultado] ADD PRIMARY KEY ([id_resultado])
GO

-- ----------------------------
-- Indexes structure for table sede
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table sede
-- ----------------------------
ALTER TABLE [dbo].[sede] ADD PRIMARY KEY ([id_sede])
GO

-- ----------------------------
-- Indexes structure for table torneo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table torneo
-- ----------------------------
ALTER TABLE [dbo].[torneo] ADD PRIMARY KEY ([id_torneo])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[academia]
-- ----------------------------
ALTER TABLE [dbo].[academia] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[calle]
-- ----------------------------
ALTER TABLE [dbo].[calle] ADD FOREIGN KEY ([id_ciudad]) REFERENCES [dbo].[ciudad] ([id_ciudad]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[categoria_torneo]
-- ----------------------------
ALTER TABLE [dbo].[categoria_torneo] ADD FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria] ([id_categoria]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[categoria_torneo] ADD FOREIGN KEY ([id_faja]) REFERENCES [dbo].[faja] ([id_faja]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[ciudad]
-- ----------------------------
ALTER TABLE [dbo].[ciudad] ADD FOREIGN KEY ([id_pais]) REFERENCES [dbo].[pais] ([id_pais]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[direccion]
-- ----------------------------
ALTER TABLE [dbo].[direccion] ADD FOREIGN KEY ([id_barrio]) REFERENCES [dbo].[barrio] ([id_barrio]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[direccion] ADD FOREIGN KEY ([id_calle]) REFERENCES [dbo].[calle] ([id_calle]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[inscripcion]
-- ----------------------------
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_categoria_torneo]) REFERENCES [dbo].[categoria_torneo] ([id_categoria_torneo]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_participante]) REFERENCES [dbo].[participante] ([id_participante]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_torneo]) REFERENCES [dbo].[torneo] ([id_torneo]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[lucha]
-- ----------------------------
ALTER TABLE [dbo].[lucha] ADD FOREIGN KEY ([id_participante1]) REFERENCES [dbo].[participante] ([id_participante]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[lucha] ADD FOREIGN KEY ([id_participante2]) REFERENCES [dbo].[participante] ([id_participante]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[lucha] ADD FOREIGN KEY ([id_resultado]) REFERENCES [dbo].[resultado] ([id_resultado]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[lucha] ADD FOREIGN KEY ([id_torneo]) REFERENCES [dbo].[torneo] ([id_torneo]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[participante]
-- ----------------------------
ALTER TABLE [dbo].[participante] ADD FOREIGN KEY ([id_academia]) REFERENCES [dbo].[academia] ([id_academia]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[participante] ADD FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria] ([id_categoria]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[participante] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[participante] ADD FOREIGN KEY ([id_faja]) REFERENCES [dbo].[faja] ([id_faja]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[sede]
-- ----------------------------
ALTER TABLE [dbo].[sede] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[torneo]
-- ----------------------------
ALTER TABLE [dbo].[torneo] ADD FOREIGN KEY ([id_estado]) REFERENCES [dbo].[estado] ([id_estado]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[torneo] ADD FOREIGN KEY ([id_sede]) REFERENCES [dbo].[sede] ([id_sede]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
