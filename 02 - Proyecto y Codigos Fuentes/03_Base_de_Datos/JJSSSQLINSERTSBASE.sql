DBCC CHECKIDENT (estado, RESEED, 0)
GO
INSERT INTO [dbo].[estado] (nombre) VALUES ('InscripcionAbierta'),('InscripcionCerrada'),('EnCurso'),('Finalizado')
GO
DBCC CHECKIDENT (categoria, RESEED, 0)
GO
INSERT INTO [dbo].[categoria] (nombre,peso_desde,peso_hasta,edad_desde,edad_hasta) 
VALUES
('Rooster Juvenile Male',0,53.50,0,16,'Hombre'),
('Light Feather Juvenile Male',53.50,58.50,0,16,'Hombre'),
('Feather Juvenile Male',58.50,64,0,16,'Hombre'),
('Light Juvenile Male',64,69,0,16,'Hombre'),
('Middle Juvenile Male',69,74,0,16,'Hombre'),
('Medium Heavy Juvenile Male',74,79.3,0,16,'Hombre'),
('Heavy Juvenile Male',79,84.30,0,16,'Hombre'),
('Super Heavy Juvenile Male',84.30,89.3,0,16,'Hombre'),
('Ultra Heavy Juvenile Male',89.3,999,0,16,'Hombre'),
('Open Class Juvenile Male',69,999,0,16,'Hombre'),

('Rooster Adult, Master and Senior Male',0,57.50,17,99,'Hombre'),
('Light Feather Adult, Master and Senior Male',57.50,64,17,99,'Hombre'),
('Feather Adult, Master and Senior Male',64,70,17,99,'Hombre'),
('Light Adult, Master and Senior Male',70,76,17,99,'Hombre'),
('Middle Adult, Master and Senior Male',76,82.3,17,99,'Hombre'),
('Medium Heavy Adult, Master and Senior Male',82.3,88.3,17,99,'Hombre'),
('Heavy Adult, Master and Senior Male',88.3,94.3,17,99,'Hombre'),
('Super Heavy Adult, Master and Senior Male',94.3,100.5,17,99,'Hombre'),
('Ultra Heavy Adult, Master and Senior Male',100.5,999,17,99,'Hombre'),
('Open Class Adult, Master and Senior Male',0,999,17,99,'Hombre'),

('Light Feather Adult, Master and Senior Female',0,53.5,17,99,'Mujer'),
('Feather Adult, Master and Senior Female',53.5,58.5,17,99,'Mujer'),
('Light Adult, Master and Senior Female',58.5,64,17,99,'Mujer'),
('Middle Adult, Master and Senior Female',64,69,17,99,'Mujer'),
('Medium Heavy Adult, Master and Senior Female',69,74,17,99,'Mujer'),
('Heavy Adult, Master and Senior Female',74,999,17,99,'Mujer'),
('Open Class Adult, Master and Senior Male',0,999,17,99,'Mujer'),

('Light Feather Juvenile Female',0,48.3,0,16,'Mujer'),
('Feather Juvenile Female',48.3,52.5,0,16,'Mujer'),
('Light Juvenile Female',52.5,56.5,0,16,'Mujer'),
('Middle Juvenile Female',56.5,60.5,0,16,'Mujer'),
('Medium Heavy Juvenile Female',60.5,65,0,16,'Mujer'),
('Heavy Juvenile Female',65,999,0,16,'Mujer'),
('Open Class Juvenile Female',60.5,999,0,16,'Mujer')
