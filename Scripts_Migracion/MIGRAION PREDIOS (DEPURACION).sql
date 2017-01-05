----	NO SCRIPT AJEC:		09
----	REGISTROS MIGRADOS: 39867
----	TIEMPO DE EJEC:		00.02.12

----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Fosa_Septica CHAR(2) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Numero_Alumnos NUMERIC(10,2) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Subsidio INT NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Descuento_Empleado INT NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Empleado_ID CHAR(10) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD CONSTRAINT FK_EMPLEADO
----FOREIGN KEY (Empleado_ID) REFERENCES Simapag_res.dbo.CAT_EMPLEADOS(EMPLEADO_ID)

----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Colonia_Entrega CHAR(5) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Calle_Entrega CHAR(5) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Numero_Exterior_Entrega CHAR(50) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Numero_Interior_Entrega CHAR(20) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios ADD Habilitar_Datos_Entrega CHAR(2) NULL

--ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD Escuela_Publica CHAR (2) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD Notificacion_Pago CHAR (2) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD Colonia_Entregas VARCHAR (150) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD Calle_Entregas VARCHAR (150) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD Numero_Exterior_Entregas VARCHAR (100) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD Numero_Interior_Entregas VARCHAR (100) NULL
----ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD tiene_cuenta CHAR (2) NULL
------ALTER TABLE Simapag_res.dbo.Cat_Cor_Predios  ADD fecha_activacion SMALLDATETIME  NULL

INSERT INTO	Simapag_20161015.dbo.Cat_Cor_Predios
			(Predio_ID, RPU, No_Cuenta, Fecha_Creo, Colonia_ID,Numero_Interior, Numero_Exterior, Lote,
			 Manzana, Longitud, Latitud, poblacion_id, Descuento_Empleado, Escuela_Publica, Subsidio, Numero_Alumnos, Calle_Referencia1_ID,
			 Calle_Referencia2_ID, Datos_Complementarios, Calle_ID, Grupo_Concepto_Cobro_ID, Ruta_Reparto_ID, Region_ID, Tarifa_ID,
			 Estatus, Cortado, Bloqueado, Facturar, Cobranza, Requerido, Aplica_Notificacion, Fecha_Inicio_Facturacion,
			 Sancionado, No_Orden_Reparto, Giro_Actividad_ID, Usuario_ID, Zona_ID, Fosa_Septica, Habilitar_Datos_Entrega, Usuario_Creo,Clave_Envio)

						 
			SELECT	RIGHT('0000000000'+ LTRIM(RTRIM(STR(ROW_NUMBER() OVER(ORDER BY C1.rpu ASC)))), 10)
					AS ID_PREDIO,
					C1.rpu,
					C1.cuenta,
					CASE
						when C1.fecha_alta = '1899-12-30' THEN
							'1900-01-01'
						else
							c1.fecha_alta
						END	
					as Fecha_alta,
					C1.COLONIA,
					C1.num_int,
					C1.NUM_EXT,
						CASE
							WHEN	C1.LOTE IS NULL
							THEN	''
							ELSE	C1.LOTE
						END
					AS LOTE,
						CASE
							WHEN	C1.MANZANA IS NULL
							THEN	''
							ELSE	C1.MANZANA
						END
					AS MANZANA,
					C1.LONGIUD,
					C1.LATITUD,
						(SELECT	top 1 SBCA.poblacion_id
						 FROM	Simapag_20161015.dbo.Cat_cor_Poblacion
						 AS		SBCA
						 WHERE	SBCA.clave_poblacion = C1.POBLACION)
					AS POBLACION,
					C1.DESCUENTO_EMPLEADO,
					C1.Escuela_Publica,
					C1.SUBSIDIO,
					C1.NUMERO_ALUMNOS,
					C1.REFERENCIA1,
					C1.REFERENCIA2,
						REPLACE(C1.DOMICILIO, '"', '')
					AS DATOS_COMPLEMENTARIOS,
					C1.CALLE,
						CASE
							WHEN	C1.TIENE_SERV_AGUA = '1' 
							THEN	(SELECT	GRUPO_CONCEPTO_ID 
									 FROM		Simapag_20161015.dbo.Cat_Cor_Grupos_Conceptos 
									 WHERE		giro_id = (	SELECT	Giro_ID 
															FROM	Simapag_20161015.dbo.Cat_Cor_Giros_Actividades 
															WHERE	Clave = C1.ACTIVIDAD 
															AND		Descripcion = C1.CVE_GIRO)
									 
									 AND		NOMBRE LIKE '%Drenaje%')
							ELSE	(SELECT	GRUPO_CONCEPTO_ID 
									 FROM		Simapag_20161015.dbo.Cat_Cor_Grupos_Conceptos 
									 WHERE		giro_id = (	SELECT	Giro_ID 
															FROM	Simapag_20161015.dbo.Cat_Cor_Giros_Actividades 
															WHERE	Clave = C1.ACTIVIDAD 
															AND		Descripcion = C1.CVE_GIRO) 
									AND NOMBRE LIKE 'AGUA')
						END 
					AS GRUPOCONCEPTOID,
						(SELECT	top 1 SBCA.Ruta_Reparto_ID 
						 FROM	Simapag_20161015.dbo.Cat_Cor_Rutas_Reparto 
						 AS		SBCA 
						 WHERE		SBCA.No_Ruta = C1.ruta)						 
					AS RUTA,
						(SELECT	top 1 SBCA.Region_ID 
						 FROM		Simapag_20161015.dbo.Cat_Cor_Regiones 
						 AS			SBCA 
						 WHERE		SBCA.Comentarios = C1.REGION ) 
					AS REGION,
						(SELECT	top 1 SBCA.Tarifa_ID 
						 FROM		Simapag_20161015.dbo.Cat_Cor_Tarifas 
						 AS			SBCA 
						 WHERE		SBCA.Abreviatura = C1.CVE_TARIFA ) 
					AS TARIFA,
						CASE
							WHEN	C1.SUSPENDIDO = 1 AND C1.TOTAL = 0 
							THEN	'SUSPENDIDO'
							
							WHEN	C1.SUSPENDIDO = 1 AND C1.TOTAL <> 0 
							THEN	'INACTIVO'
							
							WHEN	C1.suspendido = 2 
							THEN	'CANCELADO'
							
							WHEN	C1.suspendido = 3 
							THEN	'INACTIVO'
							
							WHEN	C1.suspendido = 4
							THEN	'INACTIVO'
							
							WHEN	C1.suspendido = 5 
							THEN	'RESCINDIDO'
										
							ELSE	'ACTIVO'
						END 
					AS ESTATUS,
					(SELECT case 
						    when 	SBCG.corte=0 then 'NO'
					    	when 	SBCG.corte=1 then 'SI'
							end
						 FROM	Simapag_09_01_2016.dbo.Recibos
						 AS		SBCG  
						 WHERE	SBCG.RPU = C1.rpu ) as CORTADO,
						CASE
							WHEN	C1.suspendido = 2 OR (C1.suspendido = 1 AND C1.total < 1) 
							THEN	'NO'	 
							
							WHEN	C1.suspendido = 0 OR C1.suspendido LIKE '' 
							THEN	'NO'
							
							ELSE	'SI' 
						END		
					AS BLOQUEADO,
						CASE 
							WHEN	C1.suspendido = 0 OR C1.suspendido LIKE '' 
							THEN	'SI'
							ELSE	'NO' 
						END 
					AS FACTURAR,
						CASE
							WHEN	C1.suspendido = 4
							THEN	'SI'	
							ELSE	'NO' 
						END 
					AS COBRANZA,
						CASE
							WHEN	C1.suspendido = 3 
							THEN	'SI'	 
							ELSE	'NO' 
						END 
					AS REQUERIDO,
						CASE
							WHEN	C1.suspendido = 3
							THEN	'SI'	 
							ELSE	'NO' 
						END 
					AS APLICA_NOTIFICACION,
					--	CASE 
					--		WHEN	C1.suspendido = 0 OR C1.suspendido LIKE '' 
					--		THEN	C1.facturacion
					--		ELSE	'' 
					--	END 
					--AS FECH_FACTURAR,
					CASE
						when C1.fecha_alta = '1899-12-30' THEN
							'1900-01-01'
						else
							c1.fecha_alta
						END	
					AS FECHA_INICIO_FACTURAR,
						CASE	
							WHEN	C1.sancion = '0' 
							THEN	'NO'
							ELSE	'SI' 
						END 
					AS SANCIONADO,
						CAST(SUBSTRING(C1.cuenta,8,4) AS INT) 
					AS ORDEN_REPARTO,
						(SELECT	top 1 SBCA.Actividad_Giro_ID 
						 FROM	Simapag_20161015.dbo.Cat_Cor_Giros_Actividades 
						 AS		SBCA  
						 WHERE	SBCA.Clave = C1.actividad 
						 AND	SBCA.Descripcion = C1.CVE_GIRO ) 
					AS GIRO_ACTIVIDAD,
						(SELECT	top 1 SBCA.USUARIO_ID 
						 FROM	Simapag_20161015.dbo.Cat_Cor_Usuarios 
						 AS		SBCA  
						 WHERE	SBCA.RPUM = C1.rpu ) 
					AS USUARIO,
					NULL AS ZONA,
						0
					AS FOSA_SEPTICA,
						'NO'
					AS HABILITAR_DATOS_ENTREGA,
						'MIGRACION'
					AS U_CREO,
						(SELECT top 1 SBCA.cve_envio
						 FROM	Simapag_09_01_2016.dbo.padron
						 AS		SBCA
						 WHERE	SBCA.rpu = C1.rpu)
					AS CLAVE_ENVIO					
					
			FROM	(SELECT 	SBC1.rpu,
							SBC1.cuenta,
							SBC1.ruta,
							SBC1.fecha_alta,
							SBC1.actividad,
							SBC1.num_int,
								SBC1.sector
							AS REGION,		
								SBC1.agua
							AS TIENE_SERV_AGUA,				
								RTRIM(SBC1.ncalle) + SPACE(1) + RTRIM(SBC1.ncolonia)
							AS DOMICILIO,
								dbo.fCAMBIAR_TARIFA(SBC1.tarifa, 'G')
							AS CVE_GIRO,
								dbo.fCAMBIAR_TARIFA(SBC1.tarifa, 'T')
							AS CVE_TARIFA,
								dbo.fOBTENER_LOTEV3(SBC1.ncalle)
							AS LOTE,
								CASE	
									WHEN	SBC1.manzana = ''
									THEN	dbo.fOBTENER_MANZANAV3(SBC1.ncalle)
									ELSE	SBC1.manzana
								END
							AS MANZANA,
								CASE
									WHEN	SBC1.suspendido = ''
									THEN	0
									ELSE	SBC1.suspendido
								END
							AS SUSPENDIDO,		
								(SELECT	SBCA.facturacion
								 FROM	Simapag_09_01_2016.dbo.recibos
								 AS		SBCA
								 WHERE	SBCA.rpu = SBC1.rpu)
							AS FACTURACION,		
								(SELECT	SBCA.sancion
								 FROM	Simapag_09_01_2016.dbo.recibos
								 AS		SBCA
								 WHERE	SBCA.rpu = SBC1.rpu)
							AS SANCION,
								(SELECT DISTINCT	
										CONVERT(NUMERIC(20,13), SBCA.Latitude)   
								 FROM	Simapag_20161015.dbo.Hoja1$
								 AS		SBCA 
								 WHERE	SBCA.rpu = SBC1.rpu )
							AS LATITUD,
								(SELECT DISTINCT	
										CONVERT(NUMERIC(20,12), SBCA.Longitude) 
								 FROM	Simapag_20161015.dbo.Hoja1$
								 AS		SBCA 
								 WHERE	SBCA.rpu = SBC1.rpu) 
							AS LONGIUD,
								(SELECT top 1	SBCA.CALLE_ID
								 FROM	Simapag_20161015.dbo.Cat_Cor_Calles
								 AS		SBCA
								 WHERE	SBCA.COMENTARIOS = dbo.fOBTENER_OLD_CALLE(SBC1.calle, SBC1.ncalle))
							AS CALLE,
								CASE
									WHEN	ISNUMERIC(SBC1.colonia) = 0
									THEN	(SELECT top 1 	SBCA.COLONIA_ID
											 FROM	Simapag_20161015.dbo.Cat_Cor_Colonias
											 AS		SBCA
											 WHERE	SBCA.DESCRIPCION =
																	 (SELECT	TOP 1
																			SBCA.DESCRIPCION
																	 FROM	Simapag_20161015.dbo.Cat_Cor_Colonias
																	 AS		SBCA
																	 WHERE	RTRIM(SBC1.ncolonia) LIKE '%' + RTRIM(SBCA.NOMBRE) + '%'
																	 ORDER
																	 BY		SBCA.COLONIA_ID))	
											 
									ELSE	(SELECT top 1	SBCA.COLONIA_ID
											 FROM	Simapag_20161015.dbo.Cat_Cor_Colonias
											 AS		SBCA
											 WHERE	SBCA.DESCRIPCION = SBC1.colonia)
								END
							AS COLONIA,
								CASE
									WHEN	(SELECT top 1 	SBCA.total
											 FROM	Simapag_09_01_2016.dbo.recibos
											 AS		SBCA
											 WHERE	SBCA.rpu = SBC1.rpu) > 0
									THEN	1
									ELSE	0
								END
							AS TOTAL,
								CASE
									WHEN	SBC1.num_ext = ''
									OR		SBC1.num_ext IS NULL
									THEN	dbo.fOBTENER_NUMERO_EXTERIORV3(SBC1.ncalle)
									ELSE	SBC1.num_ext
								END							
							AS NUM_EXT,		
								CASE SBC1.tarifa
									WHEN	'CE'
									THEN	1
									ELSE	0
								END
							AS DESCUENTO_EMPLEADO,
								CASE
									WHEN	SBC1.ent_calle <> ''
									THEN	(SELECT top 1 	SBCA.CALLE_ID
											 FROM	Simapag_20161015.dbo.Cat_Cor_Calles
											 AS		SBCA
											 WHERE	SBCA.COMENTARIOS = dbo.fOBTENER_OLD_CALLE(SBC1.ent_calle, ''))
								END
							AS REFERENCIA1,
								CASE
									WHEN	SBC1.ycalle <> ''
									THEN	(SELECT top 1	SBCA.CALLE_ID
											 FROM	Simapag_20161015.dbo.Cat_Cor_Calles
											 AS		SBCA
											 WHERE	SBCA.COMENTARIOS = dbo.fOBTENER_OLD_CALLE(SBC1.ycalle, ''))
								END
							AS REFERENCIA2,
									CASE
										WHEN	SBC1.poblacion <> ''
										THEN	SBC1.poblacion
										ELSE	(SELECT top 1 	SBCA.poblacion_id 
												 FROM	Simapag_20161015.dbo.Cat_cor_Poblacion 
												 AS		SBCA  
												WHERE	SBCA.clave_poblacion = SUBSTRING(SBC1.cuenta,3,2) ) 
									END
							AS POBLACION,
								CASE	SBC1.pobescolar
									WHEN	0
									THEN	NULL
									ELSE	SBC1.pobescolar							
								END
							AS NUMERO_ALUMNOS,
								SBC1.subsidio
							AS SUBSIDIO,
								CASE		
									WHEN	SBC1.pobescolar = 0
									THEN	'NO'
									ELSE	'SI'							
								END
							AS Escuela_Publica
							
					FROM	Simapag_09_01_2016.dbo.padron
					AS		SBC1
					WHERE	SBC1.rpu <> '' AND SBC1.sector <> 99 AND SBC1.sector <> '') 
			AS		C1
			/*WHERE  (SELECT	TOP 1	SBCA.Tarifa_ID 
						 FROM		Simapag_20161015.dbo.Cat_Cor_Tarifas AS			SBCA 
						 WHERE		SBCA.Abreviatura = C1.CVE_TARIFA ) is null or
						 (SELECT	 top 1	SBCA.Region_ID 
						 FROM		Simapag_20161015.dbo.Cat_Cor_Regiones 
						 AS			SBCA 
						 WHERE		SBCA.Comentarios = C1.REGION )  is null*/
			ORDER 
			BY		C1.fecha_alta
			
			
			
			
			----todos los congelados bloqueados 
			