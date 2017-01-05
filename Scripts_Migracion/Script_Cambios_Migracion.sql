----------------------------------------------------------------------------
-------------- PENDIENTES PARA MIGRACION JUEVES 29/09/2016 -----------------
----------------------------------------------------------------------------

--****** AGREGAR UN NUEVO EMPLEADO: PRESIDENTE DEL CONSEJO ******--

--SELECT * FROM Simapag_Praga.dbo.CAT_EMPLEADOS WHERE EMPLEADO_ID = '0000000070'
INSERT INTO CAT_EMPLEADOS
	(EMPLEADO_ID,DEPENDENCIA_ID,NO_EMPLEADO,APELLIDO_PATERNO,APELLIDO_MATERNO,NOMBRE,
	 CALLE,COLONIA,CIUDAD,ESTADO,SEXO,ESTATUS,NO_IMSS,USUARIO_CREO,FECHA_CREO,Distrito_ID)
VALUES
	('0000000070','00019','4040','AVILA','VICTORIA','JUAN SEBASTIAN','NA','NA','00001','00001','MASCULINO',
	 'ACTIVO','45973500814844552683','MIGRACION',GETDATE(),'00004')


--****** MIGRAR ORGANIGRAMA ******--

	--SELECT * FROM Simapag_Praga.dbo.CAT_ORGANIGRAMA
	INSERT INTO CAT_ORGANIGRAMA
		(PARAMETRO_ID,DEPENDENCIA_ID,EMPLEADO_ID,TIPO,MODULO,USUARIO_CREO,FECHA_CREO,Abreviatura_Carrera)
	VALUES
		('00001','00020','0000000027','DIRECTOR GENERAL'			,'COMERCIALIZACION','MIGRACION',GETDATE(),'ING.'),
		('00002','00018','0000000026','DIRECTOR DE COMERCIALIZACION','COMERCIALIZACION','MIGRACION',GETDATE(),'C.P'	),
		('00003','00019','0000000070','PRESIDENTE DEL CONSEJO'		,'COMERCIALIZACION','MIGRACION',GETDATE(),'LIC.')

--****** AGREGAR 2 NUEVOS CONCEPTOS COBROS DE PIPAS ******--

	--SELECT * FROM Cat_Cor_Conceptos_Cobros WHERE Nombre LIKE '%subsidiada%'
	INSERT INTO Cat_Cor_Conceptos_Cobros
		(Concepto_ID,Nombre,Tipo_Calculo,Usuario_Creo,Fecha_Creo,impuesto_id,concepto_categoria_id,
		importe_concepto,Estatus,Facturable,M3_Pipa)
	VALUES 
		('00171','PIPA DE CONTINGENCIA SUBSIDIADA 3.5M3','CANTIDAD','MIGRACION',GETDATE(),'00001','00006',129,'ACTIVO','SI',3.50),
		('00172','PIPA DE CONTINGENCIA SUBSIDIADA 10M3'	,'CANTIDAD','MIGRACION',GETDATE(),'00001','00006',369,'ACTIVO','SI',10)


--****** ACTUALIZAR EL IMPORTE DEL CONCEPTO DE MEDIDOR 1/2 P/CONTRATO ****** --

	--SELECT * FROM Cat_Cor_Conceptos_Cobros WHERE Concepto_ID = '00079'
	UPDATE Cat_Cor_Conceptos_Cobros SET importe_concepto = 454 WHERE Concepto_ID = '00079'
	UPDATE Cat_Cor_Conceptos_Cobros SET impuesto_id = '00002' WHERE Concepto_ID = '00079'


--****** MODIFICAR EL EMPLEADO NO. 0000000017 ****** --

	--SELECT * FROM CAT_EMPLEADOS WHERE EMPLEADO_ID = '0000000017'
	UPDATE CAT_EMPLEADOS SET APELLIDO_PATERNO = 'BUENO' WHERE EMPLEADO_ID = '0000000017'
	UPDATE CAT_EMPLEADOS SET APELLIDO_MATERNO = 'BRIONES' WHERE EMPLEADO_ID = '0000000017'

--*** Checar que no haya empleados repetidos... *** --

	SELECT COUNT(*),NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO FROM CAT_EMPLEADOS 
	GROUP BY NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO
	HAVING COUNT(*) > 1

	--SELECT * FROM CAT_EMPLEADOS WHERE NOMBRE = 'ABRAHAM GERARDO'
	--DELETE FROM CAT_EMPLEADOS WHERE EMPLEADO_ID = '0000000064'


--****** MODIFICAR EL NOMBRE DEL DIRECTOR ****** --

	--SELECT * FROM CAT_EMPLEADOS WHERE EMPLEADO_ID = '0000000027'
	UPDATE CAT_EMPLEADOS SET NOMBRE = 'JOSE' WHERE EMPLEADO_ID = '0000000027'
	

--****** QUITAR ESPACIOS A NOMBRE Y APELLIDOS DE USUARIOS ******--

	UPDATE Cat_Cor_Usuarios SET NOMBRE = LTRIM(RTRIM(NOMBRE))
	UPDATE Cat_Cor_Usuarios SET APELLIDO_PATERNO = LTRIM(RTRIM(APELLIDO_PATERNO))
	UPDATE Cat_Cor_Usuarios SET APELLIDO_MATERNO = LTRIM(RTRIM(APELLIDO_MATERNO))