use Simapag_29_07_2016

--3263
SELECT * FROM convadmvo WHERE convenio=3196
SELECT pagointeres, * from pagadmvo WHERE convenio=3196
SELECT * FROM pagopagadmvo WHERE convenio=3196

SELECT * FROM convadmvo WHERE convenio=3263
SELECT pagointeres, * from pagadmvo WHERE convenio=3263
SELECT * FROM pagopagadmvo WHERE convenio=3263


SELECT * FROM convadmvo WHERE convenio=549
SELECT pagointeres,pagado, * from pagadmvo WHERE convenio=549
SELECT * FROM pagopagadmvo WHERE convenio=549

SELECT * FROM convadmvo WHERE convenio=3263
SELECT pagointeres,, * from pagadmvo WHERE convenio=3263
SELECT * FROM pagopagadmvo WHERE convenio=3263



SELECT SBCA.convenio,
							SBCA.pagointeres,
							SBCA.intereses,
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
							,SBCA.pagado
							--AS TOTAL_SN_REDONDEAR,
							--AS REDONDEO						  
					FROM	Simapag_29_07_2016.dbo.pagadmvo  SBCA WHERE convenio=3282