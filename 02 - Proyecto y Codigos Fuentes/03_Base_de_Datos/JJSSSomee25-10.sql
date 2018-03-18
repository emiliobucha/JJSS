/*
Navicat SQL Server Data Transfer

Source Server         : JJSS -Somee
Source Server Version : 110000
Source Host           : JJSSDB1.mssql.somee.com:1433
Source Database       : JJSSDB1
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 110000
File Encoding         : 65001

Date: 2017-10-25 18:53:30
*/


-- ----------------------------
-- Table structure for academia
-- ----------------------------
DROP TABLE [dbo].[academia]
GO
CREATE TABLE [dbo].[academia] (
[id_academia] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[id_direccion] int NULL DEFAULT NULL ,
[telefono] bigint NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[academia]', RESEED, 5)
GO

-- ----------------------------
-- Table structure for alumno
-- ----------------------------
DROP TABLE [dbo].[alumno]
GO
CREATE TABLE [dbo].[alumno] (
[id_alumno] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(50) NOT NULL ,
[apellido] nvarchar(50) NOT NULL ,
[fecha_nacimiento] date NULL ,
[sexo] smallint NULL ,
[dni] int NOT NULL ,
[telefono] bigint NOT NULL ,
[mail] nvarchar(80) NOT NULL ,
[id_direccion] int NULL ,
[fecha_ingreso] date NOT NULL ,
[telefono_emergencia] bigint NOT NULL ,
[id_usuario] int NULL ,
[baja_logica] smallint NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[alumno]', RESEED, 46)
GO

-- ----------------------------
-- Table structure for alumno_imagen
-- ----------------------------
DROP TABLE [dbo].[alumno_imagen]
GO
CREATE TABLE [dbo].[alumno_imagen] (
[id_alumno] int NOT NULL ,
[imagen] binary(8000) NULL ,
[id_alumnoimagen] int NOT NULL IDENTITY(1,1) 
)


GO
DBCC CHECKIDENT(N'[dbo].[alumno_imagen]', RESEED, 30)
GO

-- ----------------------------
-- Table structure for alumnoxfaja
-- ----------------------------
DROP TABLE [dbo].[alumnoxfaja]
GO
CREATE TABLE [dbo].[alumnoxfaja] (
[id_alumnoxfaja] int NOT NULL IDENTITY(1,1) ,
[id_alumno] int NULL ,
[id_faja] int NULL ,
[fecha] date NULL ,
[actual] smallint NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[alumnoxfaja]', RESEED, 33)
GO

-- ----------------------------
-- Table structure for asistencia_clase
-- ----------------------------
DROP TABLE [dbo].[asistencia_clase]
GO
CREATE TABLE [dbo].[asistencia_clase] (
[id_asistencia] int NOT NULL IDENTITY(1,1) ,
[fecha_hora] datetime NOT NULL ,
[id_clase] int NOT NULL ,
[id_alumno] int NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[asistencia_clase]', RESEED, 5)
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
[sexo] smallint NULL ,
[id_tipo_clase] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[categoria]', RESEED, 36)
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
-- Table structure for categoria_producto
-- ----------------------------
DROP TABLE [dbo].[categoria_producto]
GO
CREATE TABLE [dbo].[categoria_producto] (
[id_categoria] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[categoria_producto]', RESEED, 2)
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
[id_faja] int NULL DEFAULT NULL ,
[id_resultado] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[categoria_torneo]', RESEED, 24)
GO

-- ----------------------------
-- Table structure for ciudad
-- ----------------------------
DROP TABLE [dbo].[ciudad]
GO
CREATE TABLE [dbo].[ciudad] (
[id_ciudad] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(40) NULL ,
[id_provincia] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[ciudad]', RESEED, 8)
GO

-- ----------------------------
-- Table structure for clase
-- ----------------------------
DROP TABLE [dbo].[clase]
GO
CREATE TABLE [dbo].[clase] (
[id_clase] int NOT NULL IDENTITY(1,1) ,
[id_tipo_clase] int NULL ,
[precio] float(53) NULL ,
[id_profe] int NULL ,
[id_ubicacion] int NULL ,
[nombre] nvarchar(50) NULL ,
[baja_logica] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[clase]', RESEED, 53)
GO

-- ----------------------------
-- Table structure for compra
-- ----------------------------
DROP TABLE [dbo].[compra]
GO
CREATE TABLE [dbo].[compra] (
[id_compra] int NOT NULL IDENTITY(1,1) ,
[fecha] datetime NULL ,
[id_proveedor] int NULL ,
[id_producto] int NULL ,
[precio] decimal(18) NULL ,
[cantidad] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[compra]', RESEED, 6)
GO

-- ----------------------------
-- Table structure for detalle_pago_clase
-- ----------------------------
DROP TABLE [dbo].[detalle_pago_clase]
GO
CREATE TABLE [dbo].[detalle_pago_clase] (
[id_detalle] int NOT NULL IDENTITY(1,1) ,
[id_pago_clase] int NULL ,
[monto] decimal(18) NULL ,
[fecha_hora] date NULL ,
[mes] varchar(20) NULL ,
[id_forma_pago] int NOT NULL ,
[recargo] smallint NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[detalle_pago_clase]', RESEED, 16)
GO

-- ----------------------------
-- Table structure for detalle_reserva
-- ----------------------------
DROP TABLE [dbo].[detalle_reserva]
GO
CREATE TABLE [dbo].[detalle_reserva] (
[id_detalle_reserva] int NOT NULL IDENTITY(1,1) ,
[id_reserva] int NULL ,
[id_producto] int NULL ,
[precio] decimal(18) NULL ,
[cantidad] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[detalle_reserva]', RESEED, 11)
GO

-- ----------------------------
-- Table structure for direccion
-- ----------------------------
DROP TABLE [dbo].[direccion]
GO
CREATE TABLE [dbo].[direccion] (
[id_direccion] int NOT NULL IDENTITY(1,1) ,
[numero] int NULL DEFAULT NULL ,
[calle] nvarchar(50) NULL ,
[barrio] nvarchar(50) NULL ,
[departamento] nvarchar(20) NULL ,
[piso] int NULL ,
[id_ciudad] int NULL ,
[torre] nvarchar(20) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[direccion]', RESEED, 41)
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
DBCC CHECKIDENT(N'[dbo].[estado]', RESEED, 7)
GO

-- ----------------------------
-- Table structure for estado_reserva
-- ----------------------------
DROP TABLE [dbo].[estado_reserva]
GO
CREATE TABLE [dbo].[estado_reserva] (
[id_estado_reserva] int NOT NULL IDENTITY(1,1) ,
[id_reserva] int NULL ,
[fecha] datetime NULL ,
[comentario] nvarchar(100) NULL ,
[actual] smallint NULL ,
[id_estado] int NULL 
)


GO

-- ----------------------------
-- Table structure for evento_especial
-- ----------------------------
DROP TABLE [dbo].[evento_especial]
GO
CREATE TABLE [dbo].[evento_especial] (
[id_evento] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(50) NULL ,
[fecha] date NULL ,
[id_estado] int NULL ,
[id_sede] int NULL ,
[fecha_cierre] date NULL ,
[precio] decimal(18) NULL ,
[id_tipo_evento] int NULL ,
[hora_cierre] varchar(10) NULL ,
[hora] varchar(10) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[evento_especial]', RESEED, 6)
GO

-- ----------------------------
-- Table structure for evento_especial_imagen
-- ----------------------------
DROP TABLE [dbo].[evento_especial_imagen]
GO
CREATE TABLE [dbo].[evento_especial_imagen] (
[id_evento_imagen] int NOT NULL IDENTITY(1,1) ,
[id_evento] int NOT NULL ,
[imagen] binary(8000) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[evento_especial_imagen]', RESEED, 4)
GO

-- ----------------------------
-- Table structure for faja
-- ----------------------------
DROP TABLE [dbo].[faja]
GO
CREATE TABLE [dbo].[faja] (
[id_faja] int NOT NULL IDENTITY(1,1) ,
[descripcion] nvarchar(45) NULL ,
[id_tipo_clase] int NULL ,
[orden] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[faja]', RESEED, 66)
GO

-- ----------------------------
-- Table structure for forma_pago
-- ----------------------------
DROP TABLE [dbo].[forma_pago]
GO
CREATE TABLE [dbo].[forma_pago] (
[id_forma_pago] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(40) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[forma_pago]', RESEED, 3)
GO

-- ----------------------------
-- Table structure for horario
-- ----------------------------
DROP TABLE [dbo].[horario]
GO
CREATE TABLE [dbo].[horario] (
[id_horario] int NOT NULL IDENTITY(1,1) ,
[nombre_dia] varchar(9) NULL ,
[hora_desde] varchar(20) NULL ,
[hora_hasta] varchar(20) NULL ,
[id_clase] int NULL ,
[dia] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[horario]', RESEED, 125)
GO

-- ----------------------------
-- Table structure for inscripcion
-- ----------------------------
DROP TABLE [dbo].[inscripcion]
GO
CREATE TABLE [dbo].[inscripcion] (
[id_inscripcion] int NOT NULL IDENTITY(1,1) ,
[hora] varchar(20) NULL DEFAULT NULL ,
[fecha] date NULL DEFAULT NULL ,
[codigo_barra] bigint NULL DEFAULT NULL ,
[id_participante] int NULL DEFAULT NULL ,
[id_torneo] int NULL DEFAULT NULL ,
[id_categoria] int NULL DEFAULT NULL ,
[peso] float(53) NULL ,
[id_faja] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[inscripcion]', RESEED, 15)
GO

-- ----------------------------
-- Table structure for inscripcion_clase
-- ----------------------------
DROP TABLE [dbo].[inscripcion_clase]
GO
CREATE TABLE [dbo].[inscripcion_clase] (
[id_inscripcion] int NOT NULL IDENTITY(1,1) ,
[fecha] datetime NULL ,
[hora] varchar(15) NULL ,
[id_clase] int NULL ,
[id_alumno] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[inscripcion_clase]', RESEED, 34)
GO

-- ----------------------------
-- Table structure for inscripcion_evento
-- ----------------------------
DROP TABLE [dbo].[inscripcion_evento]
GO
CREATE TABLE [dbo].[inscripcion_evento] (
[id_inscripcion] int NOT NULL IDENTITY(1,1) ,
[fecha] date NULL DEFAULT NULL ,
[id_participante] int NULL DEFAULT NULL ,
[id_evento] int NULL DEFAULT NULL ,
[hora] varchar(10) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[inscripcion_evento]', RESEED, 4)
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
-- Table structure for pago_clase
-- ----------------------------
DROP TABLE [dbo].[pago_clase]
GO
CREATE TABLE [dbo].[pago_clase] (
[id_pago_clase] int NOT NULL IDENTITY(1,1) ,
[id_alumno] int NULL ,
[id_clase] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[pago_clase]', RESEED, 13)
GO

-- ----------------------------
-- Table structure for pais
-- ----------------------------
DROP TABLE [dbo].[pais]
GO
CREATE TABLE [dbo].[pais] (
[nombre] nvarchar(30) NULL ,
[id_pais] int NOT NULL IDENTITY(1,1) 
)


GO

-- ----------------------------
-- Table structure for parametro
-- ----------------------------
DROP TABLE [dbo].[parametro]
GO
CREATE TABLE [dbo].[parametro] (
[id_parametro] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(40) NULL ,
[valor] decimal(18) NOT NULL ,
[descripcion] nvarchar(100) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[parametro]', RESEED, 3)
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
[sexo] smallint NULL DEFAULT NULL ,
[dni] int NOT NULL ,
[id_alumno] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[participante]', RESEED, 27)
GO

-- ----------------------------
-- Table structure for participante_evento
-- ----------------------------
DROP TABLE [dbo].[participante_evento]
GO
CREATE TABLE [dbo].[participante_evento] (
[id_participante] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NOT NULL ,
[apellido] nvarchar(45) NOT NULL ,
[fecha_nacimiento] date NULL DEFAULT NULL ,
[sexo] smallint NULL DEFAULT NULL ,
[dni] int NOT NULL ,
[id_alumno] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[participante_evento]', RESEED, 4)
GO

-- ----------------------------
-- Table structure for producto
-- ----------------------------
DROP TABLE [dbo].[producto]
GO
CREATE TABLE [dbo].[producto] (
[id_producto] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL ,
[id_categoria] int NULL ,
[stock] int NULL ,
[precio_venta] decimal(18) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[producto]', RESEED, 7)
GO

-- ----------------------------
-- Table structure for producto_imagen
-- ----------------------------
DROP TABLE [dbo].[producto_imagen]
GO
CREATE TABLE [dbo].[producto_imagen] (
[id_productoimagen] int NOT NULL IDENTITY(1,1) ,
[imagen] binary(8000) NULL ,
[id_producto] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[producto_imagen]', RESEED, 4)
GO

-- ----------------------------
-- Table structure for profesor
-- ----------------------------
DROP TABLE [dbo].[profesor]
GO
CREATE TABLE [dbo].[profesor] (
[id_profesor] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(50) NOT NULL ,
[apellido] nvarchar(50) NOT NULL ,
[fecha_nacimiento] date NULL ,
[sexo] smallint NULL ,
[dni] int NOT NULL ,
[telefono] bigint NOT NULL ,
[mail] nvarchar(80) NOT NULL ,
[telefono_emergencia] bigint NOT NULL ,
[id_usuario] int NULL ,
[fecha_ingreso] date NOT NULL ,
[id_direccion] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[profesor]', RESEED, 6)
GO

-- ----------------------------
-- Table structure for profesor_imagen
-- ----------------------------
DROP TABLE [dbo].[profesor_imagen]
GO
CREATE TABLE [dbo].[profesor_imagen] (
[id_profesorimagen] int NOT NULL IDENTITY(1,1) ,
[imagen] binary(8000) NULL ,
[id_profesor] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[profesor_imagen]', RESEED, 6)
GO

-- ----------------------------
-- Table structure for proveedor
-- ----------------------------
DROP TABLE [dbo].[proveedor]
GO
CREATE TABLE [dbo].[proveedor] (
[id_proveedor] int NOT NULL IDENTITY(1,1) ,
[razon_social] nvarchar(50) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[proveedor]', RESEED, 2)
GO

-- ----------------------------
-- Table structure for provincia
-- ----------------------------
DROP TABLE [dbo].[provincia]
GO
CREATE TABLE [dbo].[provincia] (
[id_provincia] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(45) NULL DEFAULT NULL ,
[id_pais] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[provincia]', RESEED, 3)
GO

-- ----------------------------
-- Table structure for reserva
-- ----------------------------
DROP TABLE [dbo].[reserva]
GO
CREATE TABLE [dbo].[reserva] (
[id_reserva] int NOT NULL IDENTITY(1,1) ,
[id_usuario] int NULL ,
[fecha] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[reserva]', RESEED, 9)
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
-- Table structure for seguridad_grupo
-- ----------------------------
DROP TABLE [dbo].[seguridad_grupo]
GO
CREATE TABLE [dbo].[seguridad_grupo] (
[id_grupo] int NOT NULL IDENTITY(1,1) ,
[nombre] varchar(100) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[seguridad_grupo]', RESEED, 3)
GO

-- ----------------------------
-- Table structure for seguridad_grupoxopcion
-- ----------------------------
DROP TABLE [dbo].[seguridad_grupoxopcion]
GO
CREATE TABLE [dbo].[seguridad_grupoxopcion] (
[id_grupoxopcion] int NOT NULL IDENTITY(1,1) ,
[id_grupo] int NOT NULL ,
[clave_opcion] varchar(100) NOT NULL ,
[ejecutar] smallint NULL DEFAULT ((0)) ,
[modificar] smallint NULL DEFAULT ((0)) ,
[eliminar] smallint NULL DEFAULT ((0)) ,
[ver] smallint NULL DEFAULT ((0)) ,
[agregar] smallint NULL DEFAULT ((0)) 
)


GO
DBCC CHECKIDENT(N'[dbo].[seguridad_grupoxopcion]', RESEED, 27)
GO

-- ----------------------------
-- Table structure for seguridad_opcion
-- ----------------------------
DROP TABLE [dbo].[seguridad_opcion]
GO
CREATE TABLE [dbo].[seguridad_opcion] (
[clave_opcion] varchar(100) NOT NULL ,
[nombre] varchar(100) NOT NULL 
)


GO

-- ----------------------------
-- Table structure for seguridad_usuario
-- ----------------------------
DROP TABLE [dbo].[seguridad_usuario]
GO
CREATE TABLE [dbo].[seguridad_usuario] (
[id_usuario] int NOT NULL IDENTITY(1,1) ,
[login] varchar(30) NOT NULL ,
[clave] varchar(100) NOT NULL ,
[nombre] varchar(100) NOT NULL ,
[mail] varchar(100) NOT NULL ,
[baja_logica] smallint NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[seguridad_usuario]', RESEED, 29)
GO

-- ----------------------------
-- Table structure for seguridad_usuarioxgrupo
-- ----------------------------
DROP TABLE [dbo].[seguridad_usuarioxgrupo]
GO
CREATE TABLE [dbo].[seguridad_usuarioxgrupo] (
[id_usuarioxgrupo] int NOT NULL IDENTITY(1,1) ,
[id_usuario] int NOT NULL ,
[id_grupo] int NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[seguridad_usuarioxgrupo]', RESEED, 27)
GO

-- ----------------------------
-- Table structure for tipo_clase
-- ----------------------------
DROP TABLE [dbo].[tipo_clase]
GO
CREATE TABLE [dbo].[tipo_clase] (
[id_tipo_clase] int NOT NULL ,
[nombre] varchar(50) NULL 
)


GO

-- ----------------------------
-- Table structure for tipo_evento_especial
-- ----------------------------
DROP TABLE [dbo].[tipo_evento_especial]
GO
CREATE TABLE [dbo].[tipo_evento_especial] (
[id_tipo_evento] int NOT NULL IDENTITY(1,1) ,
[nombre] nvarchar(50) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[tipo_evento_especial]', RESEED, 2)
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
[precio_categoria] decimal(18) NULL ,
[id_tipo_clase] int NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[torneo]', RESEED, 25)
GO

-- ----------------------------
-- Table structure for torneo_imagen
-- ----------------------------
DROP TABLE [dbo].[torneo_imagen]
GO
CREATE TABLE [dbo].[torneo_imagen] (
[id_torneo_imagen] int NOT NULL IDENTITY(1,1) ,
[id_torneo] int NOT NULL ,
[imagen] binary(8000) NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[torneo_imagen]', RESEED, 12)
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
-- Indexes structure for table alumno
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table alumno
-- ----------------------------
ALTER TABLE [dbo].[alumno] ADD PRIMARY KEY ([id_alumno])
GO

-- ----------------------------
-- Indexes structure for table alumno_imagen
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table alumno_imagen
-- ----------------------------
ALTER TABLE [dbo].[alumno_imagen] ADD PRIMARY KEY ([id_alumnoimagen])
GO

-- ----------------------------
-- Indexes structure for table alumnoxfaja
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table alumnoxfaja
-- ----------------------------
ALTER TABLE [dbo].[alumnoxfaja] ADD PRIMARY KEY ([id_alumnoxfaja])
GO

-- ----------------------------
-- Indexes structure for table asistencia_clase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table asistencia_clase
-- ----------------------------
ALTER TABLE [dbo].[asistencia_clase] ADD PRIMARY KEY ([id_asistencia])
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
-- Indexes structure for table categoria_producto
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table categoria_producto
-- ----------------------------
ALTER TABLE [dbo].[categoria_producto] ADD PRIMARY KEY ([id_categoria])
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
-- Indexes structure for table clase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table clase
-- ----------------------------
ALTER TABLE [dbo].[clase] ADD PRIMARY KEY ([id_clase])
GO

-- ----------------------------
-- Indexes structure for table compra
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table compra
-- ----------------------------
ALTER TABLE [dbo].[compra] ADD PRIMARY KEY ([id_compra])
GO

-- ----------------------------
-- Indexes structure for table detalle_pago_clase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table detalle_pago_clase
-- ----------------------------
ALTER TABLE [dbo].[detalle_pago_clase] ADD PRIMARY KEY ([id_detalle])
GO

-- ----------------------------
-- Indexes structure for table detalle_reserva
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table detalle_reserva
-- ----------------------------
ALTER TABLE [dbo].[detalle_reserva] ADD PRIMARY KEY ([id_detalle_reserva])
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
-- Indexes structure for table estado_reserva
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table estado_reserva
-- ----------------------------
ALTER TABLE [dbo].[estado_reserva] ADD PRIMARY KEY ([id_estado_reserva])
GO

-- ----------------------------
-- Indexes structure for table evento_especial
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table evento_especial
-- ----------------------------
ALTER TABLE [dbo].[evento_especial] ADD PRIMARY KEY ([id_evento])
GO

-- ----------------------------
-- Indexes structure for table evento_especial_imagen
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table evento_especial_imagen
-- ----------------------------
ALTER TABLE [dbo].[evento_especial_imagen] ADD PRIMARY KEY ([id_evento_imagen])
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
-- Indexes structure for table forma_pago
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table forma_pago
-- ----------------------------
ALTER TABLE [dbo].[forma_pago] ADD PRIMARY KEY ([id_forma_pago])
GO

-- ----------------------------
-- Indexes structure for table horario
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table horario
-- ----------------------------
ALTER TABLE [dbo].[horario] ADD PRIMARY KEY ([id_horario])
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
-- Indexes structure for table inscripcion_clase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table inscripcion_clase
-- ----------------------------
ALTER TABLE [dbo].[inscripcion_clase] ADD PRIMARY KEY ([id_inscripcion])
GO

-- ----------------------------
-- Indexes structure for table inscripcion_evento
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table inscripcion_evento
-- ----------------------------
ALTER TABLE [dbo].[inscripcion_evento] ADD PRIMARY KEY ([id_inscripcion])
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
-- Indexes structure for table pago_clase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table pago_clase
-- ----------------------------
ALTER TABLE [dbo].[pago_clase] ADD PRIMARY KEY ([id_pago_clase])
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
-- Indexes structure for table parametro
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table parametro
-- ----------------------------
ALTER TABLE [dbo].[parametro] ADD PRIMARY KEY ([id_parametro])
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
-- Indexes structure for table participante_evento
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table participante_evento
-- ----------------------------
ALTER TABLE [dbo].[participante_evento] ADD PRIMARY KEY ([id_participante])
GO

-- ----------------------------
-- Indexes structure for table producto
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table producto
-- ----------------------------
ALTER TABLE [dbo].[producto] ADD PRIMARY KEY ([id_producto])
GO

-- ----------------------------
-- Indexes structure for table producto_imagen
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table producto_imagen
-- ----------------------------
ALTER TABLE [dbo].[producto_imagen] ADD PRIMARY KEY ([id_productoimagen])
GO

-- ----------------------------
-- Indexes structure for table profesor
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table profesor
-- ----------------------------
ALTER TABLE [dbo].[profesor] ADD PRIMARY KEY ([id_profesor])
GO

-- ----------------------------
-- Indexes structure for table profesor_imagen
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table profesor_imagen
-- ----------------------------
ALTER TABLE [dbo].[profesor_imagen] ADD PRIMARY KEY ([id_profesorimagen])
GO

-- ----------------------------
-- Indexes structure for table proveedor
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table proveedor
-- ----------------------------
ALTER TABLE [dbo].[proveedor] ADD PRIMARY KEY ([id_proveedor])
GO

-- ----------------------------
-- Indexes structure for table provincia
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table provincia
-- ----------------------------
ALTER TABLE [dbo].[provincia] ADD PRIMARY KEY ([id_provincia])
GO

-- ----------------------------
-- Indexes structure for table reserva
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table reserva
-- ----------------------------
ALTER TABLE [dbo].[reserva] ADD PRIMARY KEY ([id_reserva])
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
-- Indexes structure for table seguridad_grupo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table seguridad_grupo
-- ----------------------------
ALTER TABLE [dbo].[seguridad_grupo] ADD PRIMARY KEY ([id_grupo])
GO

-- ----------------------------
-- Indexes structure for table seguridad_grupoxopcion
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table seguridad_grupoxopcion
-- ----------------------------
ALTER TABLE [dbo].[seguridad_grupoxopcion] ADD PRIMARY KEY ([id_grupoxopcion])
GO

-- ----------------------------
-- Indexes structure for table seguridad_opcion
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table seguridad_opcion
-- ----------------------------
ALTER TABLE [dbo].[seguridad_opcion] ADD PRIMARY KEY ([clave_opcion])
GO

-- ----------------------------
-- Indexes structure for table seguridad_usuario
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table seguridad_usuario
-- ----------------------------
ALTER TABLE [dbo].[seguridad_usuario] ADD PRIMARY KEY ([id_usuario])
GO

-- ----------------------------
-- Indexes structure for table seguridad_usuarioxgrupo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table seguridad_usuarioxgrupo
-- ----------------------------
ALTER TABLE [dbo].[seguridad_usuarioxgrupo] ADD PRIMARY KEY ([id_usuarioxgrupo])
GO

-- ----------------------------
-- Indexes structure for table tipo_clase
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tipo_clase
-- ----------------------------
ALTER TABLE [dbo].[tipo_clase] ADD PRIMARY KEY ([id_tipo_clase])
GO

-- ----------------------------
-- Indexes structure for table tipo_evento_especial
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table tipo_evento_especial
-- ----------------------------
ALTER TABLE [dbo].[tipo_evento_especial] ADD PRIMARY KEY ([id_tipo_evento])
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
-- Indexes structure for table torneo_imagen
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table torneo_imagen
-- ----------------------------
ALTER TABLE [dbo].[torneo_imagen] ADD PRIMARY KEY ([id_torneo_imagen])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[academia]
-- ----------------------------
ALTER TABLE [dbo].[academia] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[alumno]
-- ----------------------------
ALTER TABLE [dbo].[alumno] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[alumno] ADD FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[seguridad_usuario] ([id_usuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[alumno_imagen]
-- ----------------------------
ALTER TABLE [dbo].[alumno_imagen] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[alumnoxfaja]
-- ----------------------------
ALTER TABLE [dbo].[alumnoxfaja] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[alumnoxfaja] ADD FOREIGN KEY ([id_faja]) REFERENCES [dbo].[faja] ([id_faja]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[asistencia_clase]
-- ----------------------------
ALTER TABLE [dbo].[asistencia_clase] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[asistencia_clase] ADD FOREIGN KEY ([id_clase]) REFERENCES [dbo].[clase] ([id_clase]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[categoria]
-- ----------------------------
ALTER TABLE [dbo].[categoria] ADD FOREIGN KEY ([id_tipo_clase]) REFERENCES [dbo].[tipo_clase] ([id_tipo_clase]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[categoria_torneo]
-- ----------------------------
ALTER TABLE [dbo].[categoria_torneo] ADD FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria] ([id_categoria]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[categoria_torneo] ADD FOREIGN KEY ([id_resultado]) REFERENCES [dbo].[resultado] ([id_resultado]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[ciudad]
-- ----------------------------
ALTER TABLE [dbo].[ciudad] ADD FOREIGN KEY ([id_provincia]) REFERENCES [dbo].[provincia] ([id_provincia]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[clase]
-- ----------------------------
ALTER TABLE [dbo].[clase] ADD FOREIGN KEY ([id_ubicacion]) REFERENCES [dbo].[academia] ([id_academia]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[clase] ADD FOREIGN KEY ([id_tipo_clase]) REFERENCES [dbo].[tipo_clase] ([id_tipo_clase]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[compra]
-- ----------------------------
ALTER TABLE [dbo].[compra] ADD FOREIGN KEY ([id_producto]) REFERENCES [dbo].[producto] ([id_producto]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[compra] ADD FOREIGN KEY ([id_proveedor]) REFERENCES [dbo].[proveedor] ([id_proveedor]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[detalle_pago_clase]
-- ----------------------------
ALTER TABLE [dbo].[detalle_pago_clase] ADD FOREIGN KEY ([id_forma_pago]) REFERENCES [dbo].[forma_pago] ([id_forma_pago]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[detalle_pago_clase] ADD FOREIGN KEY ([id_pago_clase]) REFERENCES [dbo].[pago_clase] ([id_pago_clase]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[detalle_reserva]
-- ----------------------------
ALTER TABLE [dbo].[detalle_reserva] ADD FOREIGN KEY ([id_producto]) REFERENCES [dbo].[producto] ([id_producto]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[detalle_reserva] ADD FOREIGN KEY ([id_reserva]) REFERENCES [dbo].[reserva] ([id_reserva]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[direccion]
-- ----------------------------
ALTER TABLE [dbo].[direccion] ADD FOREIGN KEY ([id_ciudad]) REFERENCES [dbo].[ciudad] ([id_ciudad]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[estado_reserva]
-- ----------------------------
ALTER TABLE [dbo].[estado_reserva] ADD FOREIGN KEY ([id_estado]) REFERENCES [dbo].[estado] ([id_estado]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[estado_reserva] ADD FOREIGN KEY ([id_reserva]) REFERENCES [dbo].[reserva] ([id_reserva]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[evento_especial]
-- ----------------------------
ALTER TABLE [dbo].[evento_especial] ADD FOREIGN KEY ([id_sede]) REFERENCES [dbo].[sede] ([id_sede]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[evento_especial] ADD FOREIGN KEY ([id_estado]) REFERENCES [dbo].[estado] ([id_estado]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[evento_especial] ADD FOREIGN KEY ([id_tipo_evento]) REFERENCES [dbo].[tipo_evento_especial] ([id_tipo_evento]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[evento_especial_imagen]
-- ----------------------------
ALTER TABLE [dbo].[evento_especial_imagen] ADD FOREIGN KEY ([id_evento]) REFERENCES [dbo].[evento_especial] ([id_evento]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[faja]
-- ----------------------------
ALTER TABLE [dbo].[faja] ADD FOREIGN KEY ([id_tipo_clase]) REFERENCES [dbo].[tipo_clase] ([id_tipo_clase]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[horario]
-- ----------------------------
ALTER TABLE [dbo].[horario] ADD FOREIGN KEY ([id_clase]) REFERENCES [dbo].[clase] ([id_clase]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[inscripcion]
-- ----------------------------
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria] ([id_categoria]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_participante]) REFERENCES [dbo].[participante] ([id_participante]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_torneo]) REFERENCES [dbo].[torneo] ([id_torneo]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[inscripcion] ADD FOREIGN KEY ([id_faja]) REFERENCES [dbo].[faja] ([id_faja]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[inscripcion_clase]
-- ----------------------------
ALTER TABLE [dbo].[inscripcion_clase] ADD FOREIGN KEY ([id_clase]) REFERENCES [dbo].[clase] ([id_clase]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[inscripcion_clase] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[inscripcion_evento]
-- ----------------------------
ALTER TABLE [dbo].[inscripcion_evento] ADD FOREIGN KEY ([id_evento]) REFERENCES [dbo].[evento_especial] ([id_evento]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[inscripcion_evento] ADD FOREIGN KEY ([id_participante]) REFERENCES [dbo].[participante_evento] ([id_participante]) ON DELETE CASCADE ON UPDATE CASCADE
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
-- Foreign Key structure for table [dbo].[pago_clase]
-- ----------------------------
ALTER TABLE [dbo].[pago_clase] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[pago_clase] ADD FOREIGN KEY ([id_clase]) REFERENCES [dbo].[clase] ([id_clase]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[participante]
-- ----------------------------
ALTER TABLE [dbo].[participante] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[participante_evento]
-- ----------------------------
ALTER TABLE [dbo].[participante_evento] ADD FOREIGN KEY ([id_alumno]) REFERENCES [dbo].[alumno] ([id_alumno]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[producto]
-- ----------------------------
ALTER TABLE [dbo].[producto] ADD FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria_producto] ([id_categoria]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[producto_imagen]
-- ----------------------------
ALTER TABLE [dbo].[producto_imagen] ADD FOREIGN KEY ([id_producto]) REFERENCES [dbo].[producto] ([id_producto]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[profesor]
-- ----------------------------
ALTER TABLE [dbo].[profesor] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[profesor] ADD FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[seguridad_usuario] ([id_usuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[profesor_imagen]
-- ----------------------------
ALTER TABLE [dbo].[profesor_imagen] ADD FOREIGN KEY ([id_profesor]) REFERENCES [dbo].[profesor] ([id_profesor]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[provincia]
-- ----------------------------
ALTER TABLE [dbo].[provincia] ADD FOREIGN KEY ([id_pais]) REFERENCES [dbo].[pais] ([id_pais]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[reserva]
-- ----------------------------
ALTER TABLE [dbo].[reserva] ADD FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[seguridad_usuario] ([id_usuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[sede]
-- ----------------------------
ALTER TABLE [dbo].[sede] ADD FOREIGN KEY ([id_direccion]) REFERENCES [dbo].[direccion] ([id_direccion]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[seguridad_grupoxopcion]
-- ----------------------------
ALTER TABLE [dbo].[seguridad_grupoxopcion] ADD FOREIGN KEY ([clave_opcion]) REFERENCES [dbo].[seguridad_opcion] ([clave_opcion]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[seguridad_grupoxopcion] ADD FOREIGN KEY ([id_grupo]) REFERENCES [dbo].[seguridad_grupo] ([id_grupo]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[seguridad_usuarioxgrupo]
-- ----------------------------
ALTER TABLE [dbo].[seguridad_usuarioxgrupo] ADD FOREIGN KEY ([id_grupo]) REFERENCES [dbo].[seguridad_grupo] ([id_grupo]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[seguridad_usuarioxgrupo] ADD FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[seguridad_usuario] ([id_usuario]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[torneo]
-- ----------------------------
ALTER TABLE [dbo].[torneo] ADD FOREIGN KEY ([id_estado]) REFERENCES [dbo].[estado] ([id_estado]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[torneo] ADD FOREIGN KEY ([id_sede]) REFERENCES [dbo].[sede] ([id_sede]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[torneo] ADD FOREIGN KEY ([id_tipo_clase]) REFERENCES [dbo].[tipo_clase] ([id_tipo_clase]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[torneo_imagen]
-- ----------------------------
ALTER TABLE [dbo].[torneo_imagen] ADD FOREIGN KEY ([id_torneo]) REFERENCES [dbo].[torneo] ([id_torneo]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
