--------------------- SCRIPT INSERTAR NUEVOS USUARIOS, PREDIOS, SOLICITUDES Y CONTRATOS -----------------------

USE Simapag_20161015

------------>> BD_ORIGEN: Simapag_09_01_2016
------------>> BD_DESTINO: Simapag_20161015

--SELECT * FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=0 AND rpu NOT IN
--	(SELECT RPU FROM Ope_Cor_Solicitudes_Contrato_Servicio)

--(SELECT * FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu NOT IN
--	(SELECT rpu FROM Simapag_09_01_2016.dbo.insmedidor) AND rpu NOT IN
--(SELECT rpu FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu IN --1921
--(SELECT RPUM FROM Cat_Cor_Usuarios)))

-------------------------------------------------------------------------------
------------------------------  USUARIOS NUEVOS  ------------------------------
-------------------------------------------------------------------------------

INSERT INTO Cat_Cor_Usuarios (
	USUARIO_ID,ESTADO_ID,CIUDAD_ID,CALLE_ID,COLONIA_ID,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO,
	CALLE,COLONIA,NO_EXTERIOR,NO_INTERIOR,TELEFONO_CASA,RPUM,Permitir_Facturacion,
	tipoIdentificacion_ID,FOLIO_IFE,USUARIO_CREO,FECHA_CREO )
	
SELECT 
		RIGHT('0000000000'+CAST((ROW_NUMBER()OVER (ORDER by C1.rpu)+(SELECT MAX(USUARIO_ID)FROM Cat_Cor_Usuarios))AS VARCHAR),10)
	AS Usuario_ID,
		'00001'
	AS Estado_ID,
		'00001'
	AS Ciudad_ID,
		(SELECT CALLE_ID FROM Cat_Cor_Calles WHERE COMENTARIOS = C1.calle)
	AS Calle_ID,
		(SELECT COLONIA_ID FROM Cat_Cor_Colonias WHERE DESCRIPCION = C1.colonia)
	AS Colonia_ID,
	C1.nombre,
	C1.apaterno,
	C1.amaterno,
	C1.ncalle,
	C1.ncolonia,
	C1.num_ext,
	C1.num_int,
	C1.telefono,
	C1.rpu,
		'NO'
	AS Permitir_Facturacion,
		1
	AS TipoIdentificacionID,
		0
	AS Folio_IFE,
		'OLGA LIDIA'
	AS Usuario_creo,
		GETDATE()
	AS Fecha_creo
FROM 
	(SELECT * FROM Simapag_09_01_2016.dbo.solicitud WHERE contratada=0 AND YEAR(fecha_alta)>2014 AND rpu NOT IN
	(SELECT rpu FROM Simapag_09_01_2016.dbo.solicitud WHERE contratada=0 AND YEAR(fecha_alta)>2014 AND rpu IN
	(SELECT RPUM FROM Cat_Cor_Usuarios))
		UNION
	 SELECT * FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu NOT IN
		(SELECT rpu FROM Simapag_09_01_2016.dbo.insmedidor) AND rpu NOT IN
	(SELECT rpu FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu IN --1921
	(SELECT RPUM FROM Cat_Cor_Usuarios)))
AS C1

------------------------------------------------------------------------------------------------
---------------------------------------  PREDIOS NUEVOS   --------------------------------------
------------------------------------------------------------------------------------------------

INSERT INTO Simapag_20161015.dbo.Cat_Cor_Predios (
	Predio_ID,Grupo_Concepto_Cobro_ID,Tipo_Vivienda_ID,Usuario_ID,Colonia_ID,Giro_Actividad_ID,
	Tarifa_ID,Calle_ID,Calle_Referencia1_ID,Estatus,Numero_Exterior,Numero_Interior,Manzana,
	Lote,Fecha_Creo,Descuento_Empleado,Subsidio,Habilitar_Datos_Entrega,Escuela_Publica,
	Numero_Alumnos,Fosa_Septica,Cortado,Congelado,Cobranza,Requerido,Bloqueado,Sancionado,Facturar,
	RPU,Usuario_Creo )

SELECT
		RIGHT('0000000000'+CAST((ROW_NUMBER()OVER(ORDER by C1.rpu)+(SELECT MAX(Predio_ID)FROM Cat_Cor_Predios))AS VARCHAR),10)
	AS Predio_ID,
		(SELECT GC.GRUPO_CONCEPTO_ID FROM Cat_Cor_Grupos_Conceptos GC JOIN Cat_Cor_Giros_Actividades GA ON GA.Giro_ID=GC.giro_id
		WHERE GC.NOMBRE = 'Agua, Drenaje, Sanamiento' AND GA.Clave=C1.actividad AND GA.Descripcion=C1.CVE_GIRO)
	AS Grupo_Concepto_ID,
		'00001' --Popular
	AS Tipo_Vivienda_ID,
		(SELECT top 1 USUARIO_ID FROM Cat_Cor_Usuarios WHERE RPUM = C1.rpu)
	AS Usuario_ID,
		(SELECT top 1 COLONIA_ID FROM Cat_Cor_Colonias WHERE DESCRIPCION = C1.colonia)
	AS Colonia_ID,
		(SELECT top 1 Actividad_Giro_ID FROM Cat_Cor_Giros_Actividades WHERE Clave = C1.actividad AND Descripcion = C1.CVE_GIRO)
	AS Giro_Actividad_ID,
		(SELECT top 1 Tarifa_ID from Cat_Cor_Tarifas WHERE Abreviatura = C1.tarifa)
	AS Tarifa_ID,
		(SELECT top 1 CALLE_ID FROM Cat_Cor_Calles WHERE COMENTARIOS = dbo.fOBTENER_OLD_CALLE(C1.callep,C1.ncallep))
	AS Calle_ID,
		(SELECT top 1 CALLE_ID FROM Cat_Cor_Calles WHERE COMENTARIOS = dbo.fOBTENER_OLD_CALLE(C1.callep,C1.ncallep))
	AS Calle_Referencia_ID,
		'PENDIENTE'
	AS Estatus,
	C1.num_ext,
	C1.num_int,
	C1.manzana,
	C1.predio,
	C1.fecha_alta,
		'NO'
	AS Descuento_Empleado,
		'NO'
	AS Subsidio,
		'NO'
	AS Habilitar_Datos_Entrega,
		'NO'
	AS Escuela_Publica,
		0
	AS Numero_Alumnos,
		'NO'
	AS Fosa_Septica,
		'NO'
	AS Cortado,
		'NO'
	AS Congelado,
		'NO'
	AS Cobranza,
		'NO'
	AS Requerido,
		'NO'
	AS Bloqueado,
		'NO'
	AS Sancionado,
		'NO'
	AS Facturar,
	C1.rpu,
		'OLGA LIDIA'
	AS Usuario_Creo
FROM 
	(SELECT *,DBO.fCAMBIAR_TARIFA(tarifa,'G')AS CVE_GIRO FROM Simapag_09_01_2016.dbo.solicitud WHERE contratada=0 AND YEAR(fecha_alta)>2014 AND rpu NOT IN
		(SELECT rpu FROM Simapag_09_01_2016.dbo.solicitud WHERE contratada=0 AND YEAR(fecha_alta)>2014 AND rpu IN
			(SELECT rpu FROM Cat_Cor_Predios))
		UNION
	 SELECT *,dbo.fCAMBIAR_TARIFA(tarifa,'G')AS CVE_GIRO FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu NOT IN
		(SELECT rpu FROM Simapag_09_01_2016.dbo.insmedidor) AND rpu NOT IN
		(SELECT rpu FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu IN --1921
			(SELECT rpu FROM Cat_Cor_Predios)))
	AS C1

------------------------------------------------------------------------------------------------
----------------------------------  SOLICITUDES (SIN CONTRATO)  --------------------------------
------------------------------------------------------------------------------------------------

INSERT INTO Ope_Cor_Solicitudes_Contrato_Servicio (
	NO_SOLICITUD,PREDIO_ID,GIRO_ID,Usuario_ID,NOMBRE_SOLICITO,ELABORO_ID,ESTATUS,
	FECHA_SOLICITO,OBSERVACIONES,USUARIO_CREO,FECHA_CREO,Comentarios_Solicitud,
	Servicio_ID,Grupo_Concepto_ID,Actividad_Giro_ID,Documento_Acredita,
	Tipo_Verificacion,Aplica_Beneficio,RPU,Fosa_Septica,Predio_Especial,Tipo_Provisional )

	SELECT 
			--RIGHT('0000000000'+CAST((ROW_NUMBER()OVER(ORDER by C1.rpu)+(SELECT MAX(No_solicitud)FROM Ope_Cor_Solicitudes_Contrato_Servicio))AS VARCHAR),10)
			RIGHT('0000000000'+CAST(ROW_NUMBER()OVER(ORDER BY C1.rpu)AS VARCHAR),10)	
		AS No_Solicitud,
			(SELECT Predio_ID FROM Cat_Cor_Predios WHERE RPU = C1.rpu)
		AS Predio_ID,
			(SELECT GIRO_ID FROM Cat_Cor_Giros WHERE Clave = C1.CVE_GIRO)
		AS Giro_ID,
			(SELECT USUARIO_ID FROM Cat_Cor_Usuarios WHERE RPUM = C1.rpu)
		AS Usuario_ID,
			ISNULL(rtrim(C1.apaterno)+' ','') + ISNULL(rtrim(C1.amaterno)+' ','') + ISNULL(rtrim(C1.nombre),'')
		AS Nombre_Solicito,
			'0000000028'
		AS Elaboro_ID,
			'GENERADO'
		AS Estatus,
		C1.fecha_alta,
			ISNULL(rtrim(C1.observa1)+' ','') + ISNULL(rtrim(C1.observa2),'')
		AS Observaciones,
			'OLGA LIDIA'
		AS Usuario_creo,
			C1.fecha_alta
		AS Fecha_creo,
			ISNULL(rtrim(C1.observa1)+' ','') + ISNULL(rtrim(C1.observa2),'')
		AS Comentarios_solicitud,
			'00003'
		AS Servicio_ID,
			(SELECT GC.GRUPO_CONCEPTO_ID FROM Cat_Cor_Grupos_Conceptos GC JOIN Cat_Cor_Giros_Actividades GA ON GA.Giro_ID=GC.giro_id
			WHERE GC.NOMBRE = 'Agua, Drenaje, Sanamiento' AND GA.Clave=C1.actividad AND GA.Descripcion=C1.CVE_GIRO)
		AS Grupo_Concepto_ID,
			(SELECT Actividad_Giro_ID FROM Cat_Cor_Giros_Actividades WHERE Clave = C1.actividad AND Descripcion = C1.CVE_GIRO)
		AS Giro_Actividad_ID,
			1
		AS Documento_acredita,
			'TECNICA'
		AS Tipo_verificaciones,
			'NO'
		AS Aplica_beneficio,
		C1.rpu,
			'NO'
		AS Fosa_Septica,
			'NO'
		AS Predio_especial,
			'NO'
		AS Toma_Provisional
	FROM 
	(SELECT *, 
			dbo.fCAMBIAR_TARIFA(tarifa, 'G')
		AS CVE_GIRO
		FROM Simapag_09_01_2016.dbo.solicitud
	 WHERE contratada=0 AND YEAR(fecha_alta)>2014 AND rpu NOT IN 
	(SELECT RPU FROM Ope_Cor_Solicitudes_Contrato_Servicio)) 
	AS C1

------------------------------------------------------------------------------------------------
----------------------------------  SOLICITEDES (CON CONTRATO)  --------------------------------
------------------------------------------------------------------------------------------------

INSERT INTO Ope_Cor_Solicitudes_Contrato_Servicio (
	NO_SOLICITUD,PREDIO_ID,GIRO_ID,Usuario_ID,NOMBRE_SOLICITO,ELABORO_ID,ESTATUS,
	FECHA_SOLICITO,OBSERVACIONES,USUARIO_CREO,FECHA_CREO,Comentarios_Solicitud,
	Servicio_ID,Grupo_Concepto_ID,Actividad_Giro_ID,Documento_Acredita,
	Tipo_Verificacion,Aplica_Beneficio,RPU,Fosa_Septica,Predio_Especial,Tipo_Provisional )

	SELECT 
			RIGHT('0000000000'+CAST((ROW_NUMBER()OVER(ORDER by C1.rpu)+(SELECT MAX(No_solicitud)FROM Ope_Cor_Solicitudes_Contrato_Servicio))AS VARCHAR),10)
		AS No_Solicitud,
			(SELECT Predio_ID FROM Cat_Cor_Predios WHERE RPU = C1.rpu)
		AS Predio_ID,
			(SELECT GIRO_ID FROM Cat_Cor_Giros WHERE Clave = C1.CVE_GIRO)
		AS Giro_ID,
			(SELECT USUARIO_ID FROM Cat_Cor_Usuarios WHERE RPUM = C1.rpu)
		AS Usuario_ID,
			ISNULL(rtrim(C1.apaterno)+' ','') + ISNULL(rtrim(C1.amaterno)+' ','') + ISNULL(rtrim(C1.nombre),'')
		AS Nombre_Solicito,
			'0000000028'
		AS Elaboro_ID,
			'CONTRATO'
		AS Estatus,
		C1.fecha_alta,
			ISNULL(rtrim(C1.observa1)+' ','') + ISNULL(rtrim(C1.observa2),'')
		AS Observaciones,
			'OLGA LIDIA'
		AS Usuario_creo,
			C1.fecha_alta
		AS Fecha_creo,
			ISNULL(rtrim(C1.observa1)+' ','') + ISNULL(rtrim(C1.observa2),'')
		AS Comentarios_solicitud,
			'00003'
		AS Servicio_ID,
			(SELECT GC.GRUPO_CONCEPTO_ID FROM Cat_Cor_Grupos_Conceptos GC JOIN Cat_Cor_Giros_Actividades GA ON GA.Giro_ID=GC.giro_id
			WHERE GC.NOMBRE = 'Agua, Drenaje, Sanamiento' AND GA.Clave=C1.actividad AND GA.Descripcion=C1.CVE_GIRO)
		AS Grupo_Concepto_ID,
			(SELECT Actividad_Giro_ID FROM Cat_Cor_Giros_Actividades WHERE Clave = C1.actividad AND Descripcion = C1.CVE_GIRO)
		AS Giro_Actividad_ID,
			1
		AS Documento_acredita,
			'TECNICA'
		AS Tipo_verificaciones,
			'NO'
		AS Aplica_beneficio,
		C1.rpu,
			'NO'
		AS Fosa_Septica,
			'NO'
		AS Predio_especial,
			'NO'
		AS Toma_Provisional
	FROM 
	(SELECT *,dbo.fCAMBIAR_TARIFA(tarifa,'G')AS CVE_GIRO 
		FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu NOT IN
			(SELECT rpu FROM Simapag_09_01_2016.dbo.insmedidor) AND rpu NOT IN
		(SELECT rpu FROM Simapag_09_01_2016.dbo.solicitud WHERE YEAR(fecha_alta)>2014 AND contratada=1 AND rpu IN 
			(SELECT RPU FROM Ope_Cor_Solicitudes_Contrato_Servicio)) )
	AS C1

-----------------------------------------------------------------------------------------
---------------------------------  CONTRATOS NUEVOS  ------------------------------------
-----------------------------------------------------------------------------------------

INSERT into Ope_Cor_Contratos(
	NO_CONTRATO,NO_SOLICITUD,FOLIO,USUARIO_ID,PREDIO_ID,TARIFA_ID,ESTATUS,
	FECHA_CONTRATO,FECHA_CREO,USUARIO_CREO,RPU,NO_CUENTA )

SELECT 
		--RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (
		--ORDER BY NO_SOLICITUD
		--) + (
		--SELECT MAX(CONVERT(INTEGER, NO_CONTRATO))
		--FROM Ope_Cor_Contratos )))), 
		--10) 
		RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (ORDER BY NO_SOLICITUD)))),10)
	AS nocontrato,
	s.NO_SOLICITUD,
		--RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (
		--ORDER BY NO_SOLICITUD
		--) + (
		--SELECT MAX(CONVERT(INTEGER, NO_CONTRATO))
		--FROM Ope_Cor_Contratos
		--)))), 10)  
		RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (ORDER BY NO_SOLICITUD)))),10)
	AS [folio],
	s.Usuario_ID,
	p.Predio_ID,
	p.Tarifa_ID,
		'ACTIVO' 
	AS [estatus],
		CASE
			WHEN (SELECT fecha_alta FROM Simapag_09_01_2016.dbo.contrato WHERE p.RPU = rpu) IS NULL THEN '09/01/2016'
			ELSE (SELECT fecha_alta FROM Simapag_09_01_2016.dbo.contrato WHERE p.RPU = rpu)
		END
	AS [fecha_contrato],
		CASE
			WHEN (SELECT fecha_alta FROM Simapag_09_01_2016.dbo.contrato WHERE p.RPU = rpu) IS NULL THEN '09/01/2016'
			ELSE (SELECT fecha_alta FROM Simapag_09_01_2016.dbo.contrato WHERE p.RPU = rpu)
		END
	AS [fecha_creo],
		'Sergio' 
	AS [usuario_creo],
	p.RPU,
		0
FROM Ope_Cor_Solicitudes_Contrato_Servicio s JOIN Cat_Cor_Predios p
on s.PREDIO_ID=p.Predio_ID JOIN Cat_Cor_Usuarios u 
on p.Usuario_ID=u.USUARIO_ID
WHERE s.ESTATUS='CONTRATO'

--SELECT * FROM Simapag_09_01_2016.dbo.solicitud WHERE contratada=0 AND YEAR(fecha_alta)=2016