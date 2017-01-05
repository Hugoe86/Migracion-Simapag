use  Simapag_Roma




SELECT * FROM Ope_Cor_Convenios WHERE Folio_Convenio=3263

SELECT * FROM Ope_Cor_Convenios_Detalles WHERE No_Convenio='0000003256'

UPDATE Ope_Cor_Convenios_Detalles set Fecha_Pago='25/07/2016' WHERE No_Pago=6
and No_Convenio='0000003256'

UPDATE Ope_Cor_Convenios_Detalles set Fecha_Pago='26/07/2016' WHERE No_Pago=6
and No_Convenio='0000003256'

SELECT  (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
cd.No_Pago + 1) * 1 * cd.Subtotal)/100
from Ope_Cor_Convenios_Detalles cd
WHERE No_Convenio='0000003256' AND No_Pago<>0
ORDER by No_Pago


SELECT ROUND( (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
cd.No_Pago + 1) * 1 * cd.Subtotal)/100,0)
from Ope_Cor_Convenios_Detalles cd
WHERE No_Convenio='0000003256' AND No_Pago<>0
ORDER by No_Pago

update  Ope_Cor_Convenios_Detalles set Interes_Insoluto =
				(SELECT top 1 (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
				cd.No_Pago + 1) * 1 * cd.Subtotal)/100
				from Ope_Cor_Convenios_Detalles cd
				WHERE cd.No_Convenio=Ope_Cor_Convenios_Detalles.No_Convenio
				and Ope_Cor_Convenios_Detalles.No_Pago=cd.No_Pago
				)
WHERE  No_Pago<>0
and No_Convenio in (
SELECT No_Convenio FROM Ope_Cor_Convenios WHERE YEAR(Fecha_Creo) IN (2016,2015,2014,2013)
AND Estatus='PENDIENTE'
)

update  Ope_Cor_Convenios_Detalles set Interes_Insoluto =
				(SELECT top 1 (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
				cd.No_Pago + 1) * 2 * cd.Subtotal)/100
				from Ope_Cor_Convenios_Detalles cd
				WHERE cd.No_Convenio=Ope_Cor_Convenios_Detalles.No_Convenio
				and Ope_Cor_Convenios_Detalles.No_Pago=cd.No_Pago
				)
WHERE  No_Pago<>0
and No_Convenio in (
SELECT No_Convenio FROM Ope_Cor_Convenios WHERE YEAR(Fecha_Creo) IN (2012,2011,2010,2009,2008,2007,2006,2005)
AND Estatus='PENDIENTE'
)

UPDATE Ope_Cor_Convenios_Detalles SET Monto=Subtotal + Interes_Insoluto

UPDATE Ope_Cor_Convenios_Detalles set Abono=Monto
WHERE Estatus='PAGADO'


UPDATE Ope_Cor_Convenios set saldo_convenio=
(SELECT SUM(cd.Saldo) from Ope_Cor_Convenios_Detalles cd WHERE cd.No_Convenio= Ope_Cor_Convenios.No_Convenio)


DELETE FROM Ope_Cor_Convenios_Pagos
INSERT INTO Ope_Cor_Convenios_Pagos(No_Pago,No_Convenio,Total_Pago,Estatus,Usuario_Creo
,Fecha_Creo,Interes_Pago_Vencido,Comentarios)

SELECT RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (
						ORDER BY c.Fecha_Creo
						)))), 10) AS ID
	,c.No_Convenio AS [no_convenio]
	,(cd.campo_pagointeres_simapag + cd.Subtotal) AS [total_pago]
	,'PAGADO' AS [estatus]
	,'MIGRACION' as [usuario_creo]
    ,campo_fecha_pago_simapag AS [fecha_creo]
	,CASE 
		WHEN (cd.campo_pagointeres_simapag - cd.Interes_Insoluto) > 0
			THEN cd.campo_pagointeres_simapag - cd.Interes_Insoluto
		ELSE 0
		END AS [interes_pago_vencido]
	,cd.No_Pago AS comentario
FROM Ope_Cor_Convenios c
JOIN Ope_Cor_Convenios_Detalles cd ON c.No_Convenio = cd.No_Convenio
WHERE c.Estatus = 'PENDIENTE'
	AND cd.Estatus = 'PAGADO'




INSERT INTO Ope_Cor_Convenios_Pagos_Detalle(No_Pago,No_Convenio,No_Mensualidad)
SELECT No_Pago,No_Convenio, CONVERT(int, Comentarios)
from Ope_Cor_Convenios_Pagos


