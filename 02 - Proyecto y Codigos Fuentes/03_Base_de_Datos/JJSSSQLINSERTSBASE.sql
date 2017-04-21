DBCC CHECKIDENT (estado, RESEED, 0)
GO
INSERT INTO [dbo].[estado] (nombre) VALUES ('InscripcionAbierta'),('InscripcionCerrada'),('EnCurso'),('Finalizado')
GO
DBCC CHECKIDENT (categoria, RESEED, 0)
GO
INSERT INTO [dbo].[categoria] (nombre,peso_desde,peso_hasta,edad_desde,edad_hasta) 
VALUES
('Rooster Juvenile Male',0,53.50,0,16,1),
('Light Feather Juvenile Male',53.50,58.50,0,16,1),
('Feather Juvenile Male',58.50,64,0,16,1),
('Light Juvenile Male',64,69,0,16,1),
('Middle Juvenile Male',69,74,0,16,1),
('Medium Heavy Juvenile Male',74,79.3,0,16,1),
('Heavy Juvenile Male',79,84.30,0,16,1),
('Super Heavy Juvenile Male',84.30,89.3,0,16,1),
('Ultra Heavy Juvenile Male',89.3,999,0,16,1),
('Open Class Juvenile Male',69,999,0,16,1),

('Rooster Adult, Master and Senior Male',0,57.50,17,99,1),
('Light Feather Adult, Master and Senior Male',57.50,64,17,99,1),
('Feather Adult, Master and Senior Male',64,70,17,99,1),
('Light Adult, Master and Senior Male',70,76,17,99,1),
('Middle Adult, Master and Senior Male',76,82.3,17,99,1),
('Medium Heavy Adult, Master and Senior Male',82.3,88.3,17,99,1),
('Heavy Adult, Master and Senior Male',88.3,94.3,17,99,1),
('Super Heavy Adult, Master and Senior Male',94.3,100.5,17,99,1),
('Ultra Heavy Adult, Master and Senior Male',100.5,999,17,99,1),
('Open Class Adult, Master and Senior Male',0,999,17,99,1),

('Light Feather Adult, Master and Senior Female',0,53.5,17,99,0),
('Feather Adult, Master and Senior Female',53.5,58.5,17,99,0),
('Light Adult, Master and Senior Female',58.5,64,17,99,0),
('Middle Adult, Master and Senior Female',64,69,17,99,0),
('Medium Heavy Adult, Master and Senior Female',69,74,17,99,0),
('Heavy Adult, Master and Senior Female',74,999,17,99,0),
('Open Class Adult, Master and Senior Male',0,999,17,99,0),

('Light Feather Juvenile Female',0,48.3,0,16,0),
('Feather Juvenile Female',48.3,52.5,0,16,0),
('Light Juvenile Female',52.5,56.5,0,16,0),
('Middle Juvenile Female',56.5,60.5,0,16,0),
('Medium Heavy Juvenile Female',60.5,65,0,16,0),
('Heavy Juvenile Female',65,999,0,16,0),
('Open Class Juvenile Female',60.5,999,0,16,0)
