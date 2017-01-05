-------------------------------------------------------------------------------------------------------
----                     MAPEO DE DATOS TABLA: CONVENIOS (ADECUACIONES)                              --
-------------------------------------------------------------------------------------------------------

----	NO SCRIPT AJEC:		15
----	REGISTROS MIGRADOS: 3297
----	TIEMPO DE EJEC:		00.00.02


-- ¡ SE AÑADIRA EL CAMPO RPU Y CONVENIO EL CUAL DEBE DE SER ELIMINADO AL FINALIZAR LA MIGRACION !
-- EL CAMPO MOTIVO_ID, PUEDE HACER EFERENCIA A LA TABLA DE MOTIVOS_SOLICITUD LA CUAL 
-- HASTA EL MOMETNO SE ENCUENTRA VACIA (SE DEBERA DE CHECAR)


--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ADD RPU VARCHAR(12) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ADD Folio_Anterior INT NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ADD Tipo_Convenio VARCHAR(200) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Credencial_Elector char (15) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Mensualidades_Adeudo int  NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Mes_Inicial datetime  NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Fecha datetime  NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Adeudo money  NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Tratamiento varchar (100) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Nombre varchar (100) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Puesto varchar (50) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Usuario varchar (100) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Usuario_Creo varchar (100) NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Fecha_Creo datetime  NULL
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios  ADD Fecha_Cancelacion DATETIME  NULL


--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ALTER COLUMN Fecha_Creo DATETIME NULL;
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios DROP	COLUMN Folio_Anterior;
--ALTER TABLE Simapag_20161015.dbo.Ope_Cor_Convenios ADD Folio_Convenio INT IDENTITY(4000,1);

--USE		Simapag_20161015;
--DBCC	CHECKIDENT (Ope_Cor_Convenios, RESEED, 0);
--USE		master;
use Simapag_09_01_2016

SET IDENTITY_INSERT Simapag_20161015.dbo.Ope_Cor_Convenios ON

INSERT INTO Simapag_20161015.dbo.Ope_Cor_Convenios
			(No_Convenio,Folio_Convenio,RPU,Usuario,Mensualidades_Adeudo,Fecha,Adeudo,Solicitado_Por,
			 Porsentaje_Recargo,Pago_Inicial,Observaciones,Tipo_Convenio,Estatus,Nombre,Predio_ID,Motivo_Cancelacion_Id,
			 Motivo_Cancelacion,Fecha_Cancelacion,fecha_pago_final,Porcentaje_Interes,saldo_convenio,Mes_Inicial, Total_Descuento,
			 Usuario_Creo,Fecha_Creo,Cantidad_Recargo,Periodos_Adeudo)
			
			SELECT				
					
						RIGHT('000000000'+ LTRIM(RTRIM(STR(ROW_NUMBER() OVER(ORDER BY C1.FECHA_ELABORACION )))), 10) 
					AS ID,
						CONVERT(INT,C1.convenio) 
					AS CONVENIO,
					C1.rpu,
					C1.usuario,
					C1.MESESUALIDADES_ADEUDO,
						DATEADD(MONTH, 1, C1.FECHA_ELABORACION)
					AS FECHA,
					C1.adeudo, 
					C1.PERSONA_SOLICITO,
					C1.PORCENTAJE_RECARGO,
					C1.pago_inici,
						C1.motivo 
					AS OBSERVACION,		
					C1.TIPO_CONVENIO,			  
						CASE
							WHEN C1.ESTATUS = '' THEN 'PENDIENTE'
							WHEN C1.ESTATUS = 0 THEN 'PENDIENTE'
							WHEN C1.ESTATUS = 1 THEN 'PAGADO'							
							WHEN C1.ESTATUS = 2 THEN 'INTERMEDIO'
							WHEN C1.ESTATUS = 3 THEN 'CANCELADO'						
							WHEN C1.ESTADO = 2 THEN 'INTERMEDIO'
							WHEN C1.ESTADO = 3 THEN 'CANCELADO'
							--WHEN C1.ESTATUS = 4 THEN 'PEC2'
							ELSE 'PENDIENTE'
						END
					AS ESTATUS,
						(SELECT	(RTRIM(SBCA.NOMBRE) + SPACE(1) + RTRIM(SBCA.APELLIDO_PATERNO) + SPACE(1) + RTRIM(SBCA.APELLIDO_MATERNO)) 
						 FROM	Simapag_20161015.dbo.Cat_Cor_Usuarios 
						 AS		SBCA 
						 WHERE	SBCA.RPUM = C1.rpu)
					AS N_USUARIO,
						(SELECT	SBCA.Predio_ID 
						 FROM	Simapag_20161015.dbo.Cat_Cor_Predios 
						 AS		SBCA 
						 WHERE	SBCA.RPU = C1.rpu)
					AS PREDIO,
						(SELECT	SBCA.Motivo_Cancelacion_Id 
						 FROM	Simapag_20161015.dbo.Cat_Cor_Motivos_Cancelacion 
						 AS		SBCA 
						 WHERE	SBCA.Motivo = C1.MOTIVO_CANC)	
					AS ID_MOTIVO_CANC,
					C1.MOTIVO_CANC,
					F_CANC,
					C1.ULTIMO_PAGO,
					C1.PORCENTAJE_INTERES,
					C1.SALDO,
					C1.MES_INICIAL,
						CASE
							WHEN	C1.PORCENTAJE_RECARGO > 0
							THEN	(C1.CANTIDAD_RECARGO * C1.PORCENTAJE_RECARGO) / 100
							ELSE	0.00
						END
					AS TOTAL_DESCUENTOS,				
					C1.usuario
					AS U_CREO,
						C1.FECHA_ELABORACION
					AS F_CREO ,
					    C1.CANTIDAD_RECARGO
					AS Cantidad_recargo,
					    C1.periodo

			FROM(
					SELECT DISTINCT
								CASE	
									WHEN	SBC1.estado = ''
									THEN	0
									ELSE	SBC1.estado
								END
							AS ESTADO,
							SBC1.convenio, 
							SBC1.rpu, 
							SBC1.usuario,
								CONVERT(INT,SBC1.no_pagos) -- - 1  <-- SE RETIRO EL MENOS UNO
							AS MESESUALIDADES_ADEUDO,
								SBC1.fecha 
							AS FECHA_ELABORACION,
								SBC1.total 
							AS ADEUDO,
								SBC1.nombre 
							AS PERSONA_SOLICITO,
								'ADMINISTRATIVO'	--estado 
							AS TIPO_CONVENIO, -- <-- DEINIFIRA EL TIPO DE CONVENIO QUE ES
								(SELECT	TOP 1
											CASE
												WHEN	SBCA.status = ''
												THEN	0
												ELSE	SBCA.status
											END
										AS ESTATUS
								 FROM	Simapag_09_01_2016.dbo.pagadmvo
								 AS		SBCA
								 WHERE	SBCA.convenio = SBC1.convenio
								 AND	SBCA.rpu = SBC1.rpu
								 ORDER
								 BY		SBCA.consecutivo DESC) 
							AS ESTATUS,
								CONVERT(FLOAT,SBC1.dcto) 
							AS PORCENTAJE_RECARGO, 
							SBC1.pago_inici, 
								--CASE 
								--	WHEN	YEAR(SBC1.fecha)
								--	BETWEEN	2005
								--	AND		2012
								--	THEN	2									
								--	ELSE	0.00 -- %
								--END 
								0.00 -- %
							AS PORCENTAJE_INTERES,
							--SBC1.pagosde,
							SBC1.motivo,
							   CASE SBC1.estado
									WHEN	3
									THEN	(SELECT	CASE 
													WHEN	SBCA.motivo LIKE 'AC.%'
													OR		SBCA.motivo LIKE 'ACTA%'
													THEN	'ACTA DE SESION ORDINARIA'
													
													WHEN	SBCA.motivo LIKE 'PRESCRIPCION F%'
													THEN	'PRESCRIPCIÓN FISCAL'
													
													WHEN	SBCA.motivo LIKE '% X %'
													OR		RTRIM(SBCA.motivo) LIKE '%PRESCRIPCIÓN'
													OR		RTRIM(SBCA.motivo) LIKE 'PRESCRIPCIÓN%'
													OR		RTRIM(SBCA.motivo) LIKE 'PRESCRIPCION%'
													THEN	'PRESCRIPCIÓN DE ADEUDO' 
															
													WHEN	SBCA.motivo LIKE 'SOLICITUD%'
													OR		SBCA.motivo LIKE 'A SOL%'
													OR		SBCA.motivo LIKE 'A PET%'
													OR		SBCA.motivo LIKE 'PET%'
													THEN	'SOLICITUD DEL USUARIO'
													
													WHEN	SBCA.motivo LIKE 'ACUERDO%'
													THEN	'ACUERDO CD SESION ORDINARIA'
													
												END								
										 FROM	Simapag_09_01_2016.dbo.cancadmvo
										 AS		SBCA
										 WHERE	SBCA.rpu = SBC1.rpu
										 AND	SBCA.convenio = SBC1.convenio )
									
									ELSE	NULL
							   END
							AS MOTIVO_CANC,
							   CASE SBC1.estado
									WHEN	3
									THEN	(SELECT	SBCA.fecha							
										 FROM	Simapag_09_01_2016.dbo.cancadmvo
										 AS		SBCA
										 WHERE	SBCA.rpu = SBC1.rpu
										 AND	SBCA.convenio = SBC1.convenio )
							   END
							AS F_CANC,
								(SELECT	TOP 1
										SBCA.vencimient
								 FROM	Simapag_09_01_2016.dbo.pagadmvo
								 AS		SBCA
								 WHERE	SBCA.convenio = SBC1.convenio
								 AND		SBCA.rpu = SBC1.rpu
								 ORDER BY	SBCA.consecutivo DESC, SBCA.fecha DESC)
							AS ULTIMO_PAGO,							
								CASE
									WHEN	(SELECT	COUNT(SBCA.consecutivo)
											 FROM	Simapag_09_01_2016.dbo.pagadmvo
											 AS		SBCA
											 WHERE	SBCA.convenio = SBC1.convenio
											 AND	SBCA.rpu = SBC1.rpu) = 0 	
									THEN	SBC1.total
									
									WHEN	(SELECT	TOP 1
														CASE
															WHEN	SBCA.status = ''
															THEN	0
															ELSE	SBCA.status
														END
													AS ESTATUS
											 FROM	Simapag_09_01_2016.dbo.pagadmvo
											 AS		SBCA
											 WHERE	SBCA.convenio = SBC1.convenio
											 AND	SBCA.rpu = SBC1.rpu
											 ORDER
											 BY		SBCA.consecutivo DESC) = 1 	
									THEN	0.00
									ELSE	(SELECT	SUM(SBCA.total + SBCA.intereses)
											 FROM	Simapag_09_01_2016.dbo.pagadmvo
											 AS		SBCA
											 WHERE	SBCA.convenio = SBC1.convenio
											 AND	SBCA.rpu = SBC1.rpu) 
								END
							AS SALDO,
								(SELECT	TOP 1
										SBCA.vencimient
								 FROM	Simapag_09_01_2016.dbo.pagadmvo
								 AS		SBCA
								 WHERE	SBCA.convenio = SBC1.convenio
								 AND	SBCA.consecutivo = 2)
							AS MES_INICIAL,
								SBC1.recagua + SBC1.recalcan + SBC1.recsanea
							AS CANTIDAD_RECARGO,
								SBC1.agua + SBC1.alcan + SBC1.sanea + 
								SBC1.recagua + SBC1.recalcan + SBC1.recsanea +
								SBC1.rezagua + SBC1.rezalcan + SBC1.rezsanea +
								SBC1.iva
							AS TOTAL,
							SBC1.periodo
							
							
					FROM	Simapag_09_01_2016.dbo.convadmvo 
					AS		SBC1
					WHERE	SYS.fn_PhysLocFormatter(%%physloc%%) = (SELECT	TOP 1
																				SYS.fn_PhysLocFormatter(%%physloc%%) 
																			AS  [File:Page:Slot]
																	 FROM	Simapag_09_01_2016.dbo.convadmvo
																	 AS		SBCA
																	 WHERE	SBCA.RPU = SBC1.RPU
																	 AND	SBCA.CONVENIO = SBC1.CONVENIO  
																	 ORDER 
																	 BY		SYS.fn_PhysLocFormatter(%%physloc%%) DESC)
																-- AÑADIENDO LA CONDICION DE TOMAR LOS FOLIOS NO REPETIDOS 

					UNION


					-- SELECCION DE LA TABLA DE CONVENIO PEC

					SELECT	DISTINCT
								CASE	
									WHEN	SBC2.estado = ''
									THEN	0
									ELSE	SBC2.estado
								END
							AS ESTADO,
							SBC2.convenio, 
							SBC2.rpu, 
							SBC2.usuario,
								CASE
									WHEN	SBC2.convenio = 1
									THEN	0
									ELSE	CONVERT(INT,SBC2.no_pagos) -- - 1  <-- SE RETIRO EL MENOS UNO  
								END
							AS MESESUALIDADES_ADEUDO,
								SBC2.fecha 
							AS FECHA_ELABORACION,
								SBC2.total 
							AS ADEUDO,
								SBC2.nombre 
							AS PERSONA_SOLICITO,
								'PEC'
							AS TIPO_CONVENIO, 
								(SELECT	TOP 1
											CASE
												WHEN	SBCA.status = ''
												THEN	0
												ELSE	SBCA.status
											END
										AS ESTATUS
								 FROM	Simapag_09_01_2016.dbo.pagpec
								 AS		SBCA
								 WHERE	SBCA.convenio = SBC2.convenio
								 AND	SBCA.rpu = SBC2.rpu
								 ORDER
								 BY		SBCA.consecutivo DESC)  
							AS ESTATUS,
								0 
							AS PORCENTAJE_RECARGO, 
							SBC2.pago_inici,
								--CASE 
								--	WHEN	YEAR(SBC2.fecha)
								--	BETWEEN	2005
								--	AND		2012
								--	THEN	2									
								--	ELSE	1
								--END 
								0.00 -- %
							AS PORCENTAJE_INTERES,
							--SBC2.pagosde,
							SBC2.motivo,
								CASE 
									WHEN	SBC2.estado = 3
									THEN	(SELECT	CASE 
														WHEN	SBCA.motivo LIKE 'AC.%'
														OR		SBCA.motivo LIKE 'ACTA%'
														THEN	'ACTA DE SESION ORDINARIA'

														WHEN	SBCA.motivo LIKE 'PRESCRIPCION F%'
														THEN	'PRESCRIPCIÓN FISCAL'

														WHEN	SBCA.motivo LIKE '% X %'
														OR		RTRIM(SBCA.motivo) LIKE '%PRESCRIPCIÓN'
														OR		RTRIM(SBCA.motivo) LIKE 'PRESCRIPCIÓN%'
														OR		RTRIM(SBCA.motivo) LIKE 'PRESCRIPCION%'
														THEN	'PRESCRIPCIÓN DE ADEUDO' 

														WHEN	SBCA.motivo LIKE 'SOLICITUD%'
														OR		SBCA.motivo LIKE 'A SOL%'
														OR		SBCA.motivo LIKE 'A PET%'
														OR		SBCA.motivo LIKE 'PET%'
														THEN	'SOLICITUD DEL USUARIO'

														WHEN	SBCA.motivo LIKE 'ACUERDO%'
														THEN	'ACUERDO CD SESION ORDINARIA'

														ELSE	SBCA.motivo
													END
												
											FROM	Simapag_09_01_2016.dbo.cancpec
											AS		SBCA
											WHERE	SBCA.rpu = SBC2.rpu
											AND	SBCA.convenio = SBC2.convenio )
											
									ELSE	NULL
								END
							AS MOTIVO_CANC,    
								CASE 
									WHEN	SBC2.estado = 3
									THEN	(SELECT SBCA.fecha												
											FROM	Simapag_09_01_2016.dbo.cancpec
											AS		SBCA
											WHERE	SBCA.rpu = SBC2.rpu
											AND	SBCA.convenio = SBC2.convenio )
											
									ELSE	NULL
								END
							AS  F_CANC,   
								(SELECT	TOP 1
										SBCA.vencimient
								 FROM	Simapag_09_01_2016.dbo.pagpec
								 AS		SBCA
								 WHERE	SBCA.convenio = SBC2.convenio
								 AND	SBCA.rpu = SBC2.rpu
								 ORDER 
								 BY		SBCA.consecutivo DESC, SBCA.fecha DESC)
							AS ULTIMO_PAGO,						
								CASE
									WHEN	(SELECT	COUNT(SBCA.consecutivo)
											 FROM	Simapag_09_01_2016.dbo.pagpec
											 AS		SBCA
											 WHERE	SBCA.convenio = SBC2.convenio
											 AND	SBCA.rpu = SBC2.rpu) = 0 	
									THEN	SBC2.total
									
									WHEN	(SELECT	TOP 1
														CASE
															WHEN	SBCA.status = ''
															THEN	0
															ELSE	SBCA.status
														END
													AS ESTATUS
											 FROM	Simapag_09_01_2016.dbo.pagpec
											 AS		SBCA
											 WHERE	SBCA.convenio = SBC2.convenio
											 AND	SBCA.rpu = SBC2.rpu
											 ORDER
											 BY		SBCA.consecutivo DESC) = 1 	
									THEN	0.00
									ELSE	(SELECT	SUM(SBCA.total + SBCA.intereses)
											 FROM	Simapag_09_01_2016.dbo.pagpec
											 AS		SBCA
											 WHERE	SBCA.convenio = SBC2.convenio
											 AND	SBCA.rpu = SBC2.rpu) 
								END
							AS SALDO,
								
								
								(SELECT	TOP 1
										SBCA.vencimient
								 FROM	Simapag_09_01_2016.dbo.pagpec
								 AS		SBCA
								 WHERE	SBCA.convenio = SBC2.convenio
								 AND	SBCA.consecutivo = 2)
							AS MES_INICIAL,
								SBC2.recagua + SBC2.recalcan + SBC2.recsanea
							AS CANTIDAD_RECARGO,
								SBC2.agua + SBC2.alcan + SBC2.sanea + 
								SBC2.recagua + SBC2.recalcan + SBC2.recsanea +
								SBC2.rezagua + SBC2.rezalcan + SBC2.rezsanea +
								SBC2.iva
							AS TOTAL,
							SBC2.periodo
							
					FROM	 Simapag_09_01_2016.dbo.convpec 
					AS		SBC2
					WHERE	SYS.fn_PhysLocFormatter(%%physloc%%) = (SELECT	TOP 1
																				SYS.fn_PhysLocFormatter(%%physloc%%) 
																			AS  [File:Page:Slot]
																	 FROM	Simapag_09_01_2016.dbo.convpec
																	 AS		SBCA
																	 WHERE	SBCA.RPU = SBC2.RPU
																	 AND	SBCA.CONVENIO = SBC2.CONVENIO  
																	 ORDER 
																	 BY		SYS.fn_PhysLocFormatter(%%physloc%%) DESC)
																-- AÑADIENDO LA CONDICION DE TOMAR LOS FOLIOS NO REPETIDOS 
					)
			AS		C1




SET IDENTITY_INSERT Simapag_20161015.dbo.Ope_Cor_Convenios OFF

