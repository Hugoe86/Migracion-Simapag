USE SIMAPAG_20161015

--SELECT * FROM Cat_Cor_Metodos_Pago

INSERT INTO Cat_Cor_Metodos_Pago (Metodo_Pago_Id,Clave,Nombre,Estatus,Usuario_Creo)
VALUES
	('00001','01','EFECTIVO',				'ACTIVO','MIGRACION'),
	('00002','02','CHEQUE',					'ACTIVO','MIGRACION'),
	('00003','03','TRANSFERENCIA',			'ACTIVO','MIGRACION'),
	('00004','04','TARJETAS DE CR�DITO',	'ACTIVO','MIGRACION'),
	('00005','05','MONEDEROS ELECTR�NICOS',	'ACTIVO','MIGRACION'),
	('00006','06','DINERO ELECTR�NICO',		'ACTIVO','MIGRACION'),
	('00007','07','TARJETAS DIGITALES',		'ACTIVO','MIGRACION'),
	('00008','08','VALES DE DESPENSA',		'ACTIVO','MIGRACION'),
	('00009','09','BIENES',					'ACTIVO','MIGRACION'),
	('00010','10','SERVICIO',				'ACTIVO','MIGRACION'),
	('00011','11','POR CUENTA DE TERCERO',	'ACTIVO','MIGRACION'),
	('00012','12','DACI�N EN PAGO',			'ACTIVO','MIGRACION'),
	('00013','13','PAGO POR SUBROGACI�N',	'ACTIVO','MIGRACION'),
	('00014','14','PAGO POR CONSIGNACI�N',	'ACTIVO','MIGRACION'),
	('00015','15','CONDONACI�N',			'ACTIVO','MIGRACION'),
	('00016','16','CANCELACI�N',			'ACTIVO','MIGRACION'),
	('00017','17','COMPENSACI�N',			'ACTIVO','MIGRACION'),
	('00018','98','NA',						'ACTIVO','MIGRACION'),
	('00019','99','OTROS',					'ACTIVO','MIGRACION')
	