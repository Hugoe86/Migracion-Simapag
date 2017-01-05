

INSERT INTO	Simapag_20161015.dbo.Ope_Cor_Convenios_Detalles
		(No_Convenio, No_Pago, Subtotal, Estatus, 
		 Fecha_Vigencia, Fecha_Pago, Abono, Interes_Insoluto,
		 Monto, Saldo, Usuario_Creo, Fecha_Creo,campo_pagointeres_simapag,campo_fecha_pago_simapag,campo_interes_simapag,importe_pago_simpag)


			
			SELECT	C1.No_Convenio,-- No_Convenio
					C2.NO_PAGO,--No_Pago
					C2.SUBTOTAL,-- Subtotal
					'PENDIENTE'
					 AS estatus,--Estatus
						C1.fecha_pago_final 
					AS F_VIGECIA, --Fecha_Vigencia
					C2.vencimient,--Fecha_Pago
					0 as [abono_convenio],--Abono
						0		
					AS INTERES_S_SALDO_INSOLUTO, --Interes_Insoluto
						0						
					AS MONTO,--Monto
					0
					AS SALDO, --Saldo
						'MIGRACION' 
					AS CREO, --Usuario_Creo
						GETDATE() 
					AS F_CREO,--Fecha_Creo
					C2.pagointeres, --campo_pagointeres_simapag
					C2.F_PAGO
					  as [fecha_pago],--campo_fecha_pago_simapag cuando se hizo el pago
					C2.intereses,	--campo_interes_simapag
                    C2.ABONO as [abono_simpag] --importe_pago_simpag
			FROM	
					(SELECT	SBCA.No_Convenio,
							SBCA.Folio_Convenio,
							SBCA.RPU,
							SBCA.fecha_pago_final,
							SBCA.Porcentaje_Interes,
							SBCA.Tipo_Convenio
					FROM	Simapag_20161015.dbo.Ope_Cor_Convenios --- cambiar la base de datos
					AS		SBCA
					WHERE	SBCA.Tipo_Convenio = 'PEC')  -- <-- pagadmvo <> || pagpec =
					AS		C1

					INNER JOIN

					-- FECHA_PAGO ---> FECHE DE VENCIMIENTO
					-- FECHA_VENCIEMIENTO ---> FECHA_ULTIMO_PAGO

					(SELECT SBCA.convenio,
							SBCA.intereses,
							 (SELECT SUM( pa.intereses) FROM Simapag_09_01_2016.dbo.pagopagpec pa
							 WHERE pa.convenio=SBCA.convenio and  pa.consecutivo=SBCA.consecutivo) as pagointeres,
							SBCA.rpu,	
							SBCA.vencimient,			
							SBCA.status,
							CASE
							    WHEN (SELECT top 1 p.pago_inici FROM Simapag_09_01_2016.dbo.convpec p WHERE p.convenio=SBCA.convenio )>0 THEN
							       (SBCA.consecutivo - 1) 
							    ELSE
							     SBCA.consecutivo
							end
							AS NO_PAGO,
								SBCA.no_pagos
							AS TOTAL_PAGOS,
								SBCA.total 
							AS SUBTOTAL,
									(SELECT top 1 pa.fecha FROM Simapag_09_01_2016.dbo.pagopagpec  pa 
								WHERE pa.convenio=SBCA.convenio and
								 pa.consecutivo=SBCA.consecutivo ORDER by  pa.fecha DESC)
							AS F_PAGO,
							(SELECT SUM(pa.importe) FROM Simapag_09_01_2016.dbo.pagopagpec  pa 
								  WHERE pa.convenio=SBCA.convenio and
								   pa.consecutivo=SBCA.consecutivo )
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