use Simpag_09_10_2016


SELECT SBCA.convenio,
							(SELECT SUM( pa.intereses) FROM Simpag_09_10_2016.dbo.pagopagadmvo pa
							 WHERE pa.convenio=SBCA.convenio and  pa.consecutivo=SBCA.consecutivo) as pagointeres,
							SBCA.intereses,
							SBCA.rpu,	
							SBCA.vencimient,
							SBCA.status,
							CASE
							  WHEN (SELECT top 1 c.pago_inici from convadmvo c WHERE c.convenio=SBCA.convenio)>0 THEN
								(SBCA.consecutivo - 1)
							 ELSE
							    SBCA.consecutivo
							end 	 
							AS NO_PAGO,
								SBCA.no_pagos
							AS TOTAL_PAGOS,
								SBCA.total 
							AS SUBTOTAL,
									(SELECT top 1 pa.fecha FROM Simpag_09_10_2016.dbo.pagopagadmvo  pa 
								WHERE pa.convenio=SBCA.convenio and
								 pa.consecutivo=SBCA.consecutivo ORDER by  pa.fecha DESC)
							AS F_PAGO,
							   (SELECT SUM(pa.importe) FROM Simpag_09_10_2016.dbo.pagopagadmvo  pa 
								  WHERE pa.convenio=SBCA.convenio and
								   pa.consecutivo=SBCA.consecutivo )
							    AS ABONO
							--AS TOTAL_SN_REDONDEAR,
							--AS REDONDEO						  
					FROM	Simpag_09_10_2016.dbo.pagadmvo -- <-- pagadmvo || pagpec
					AS		SBCA 
					WHERE SBCA.convenio=749
					
					
SELECT  pa.fecha FROM Simpag_09_10_2016.dbo.pagopagadmvo  pa 
								WHERE pa.convenio=749 and
								 pa.consecutivo=5 ORDER by  pa.fecha DESC