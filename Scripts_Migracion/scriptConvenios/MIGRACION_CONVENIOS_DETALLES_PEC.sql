

INSERT INTO	Simapag_Praga.dbo.Ope_Cor_Convenios_Detalles
		(No_Convenio, No_Pago, Subtotal, Estatus, 
		 Fecha_Vigencia, Fecha_Pago, Abono, Interes_Insoluto,
		 Monto, Saldo, Usuario_Creo, Fecha_Creo,campo_pagointeres_simapag,campo_fecha_pago_simapag,campo_interes_simapag)


			
			SELECT	C1.No_Convenio,
					C2.NO_PAGO,
					C2.SUBTOTAL,
					(CASE 
					    WHEN C2.status=0 THEN 'PENDIENTE'
					    WHEN C2.status=1 THEN 'PAGADO'
					    ELSE ''
					end) AS estatus,
						C1.fecha_pago_final 
					AS F_VIGECIA, 
					C2.F_PAGO,
					C2.ABONO,
						CASE
							WHEN	C2.NO_PAGO = 0
							THEN	0
							ELSE	C2.intereses
						END			
					AS INTERES_S_SALDO_INSOLUTO,
						C2.SUBTOTAL + C2.intereses						
					AS MONTO,
						CASE
							WHEN	C2.status = 1
							THEN	0.00
							ELSE	(C2.SUBTOTAL + C2.intereses) - (C2.ABONO)
						END
					AS SALDO,
						'MIGRACION' 
					AS CREO,
						GETDATE() 
					AS F_CREO,
					C2.pagointeres,
					CASE
					   WHEN	C2.status = 1 THEN
					        C2.F_PAGO
					    ELSE
					    null
					end	 as [fecha_pago],
					C2.intereses	

			FROM	
					(SELECT	SBCA.No_Convenio,
							SBCA.Folio_Convenio,
							SBCA.RPU,
							SBCA.fecha_pago_final,
							SBCA.Porcentaje_Interes,
							SBCA.Tipo_Convenio
					FROM	Simapag_Praga.dbo.Ope_Cor_Convenios --- cambiar la base de datos
					AS		SBCA
					WHERE	SBCA.Tipo_Convenio = 'PEC')  -- <-- pagadmvo <> || pagpec =
					AS		C1

					INNER JOIN

					-- FECHA_PAGO ---> FECHE DE VENCIMIENTO
					-- FECHA_VENCIEMIENTO ---> FECHA_ULTIMO_PAGO

					(SELECT SBCA.convenio,
							SBCA.intereses,
							SBCA.pagointeres,
							SBCA.rpu,				
							SBCA.status,
								(SBCA.consecutivo - 1) 
							AS NO_PAGO,
								SBCA.no_pagos
							AS TOTAL_PAGOS,
								SBCA.total 
							AS SUBTOTAL,
								SBCA.vencimient
							AS F_PAGO,
								CASE
									WHEN	SBCA.pagado = 0
									THEN	0
									ELSE	(SBCA.pagado  + SBCA.intereses)
								END
							AS ABONO
							--AS TOTAL_SN_REDONDEAR,
							--AS REDONDEO						  
					FROM	Simapag_09_01_2016.dbo.pagpec -- <-- pagadmvo || pagpec
					AS		SBCA) 
			AS		C2
			ON		C1.Folio_Convenio = C2.convenio
			AND		C1.RPU = C2.rpu
			ORDER 
			BY		C2.convenio, C2.rpu, C2.F_PAGO