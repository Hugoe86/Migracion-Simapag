

use Simapag_Praga



DELETE FROM Convenio_Desglosado_Detalles
DELETE FROM Convenio_Desglosado
DELETE FROM Ope_Cor_Convenios_Detalles
DELETE FROM Ope_Cor_Convenios
DELETE FROM Ope_Cor_Convenios_Pagos_Detalle
DELETE FROM Ope_Cor_Convenios_Pagos

DELETE FROM Ope_Cor_Caj_Movimientos_Cobros WHERE
NO_RECIBO in(SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE LEN(LTRIM(RTRIM(no_pago_convenio)))>0 )

DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE LEN(LTRIM(RTRIM(no_pago_convenio)))>0



-- son los convenios que no checan con su convenio detallado los de tipo pec
SELECT  c.Fecha_Creo, c.Folio_Convenio, c.Adeudo,cd.Total,SUM(cde.Importe)
from Ope_Cor_Convenios c JOIN Convenio_Desglosado cd
on c.No_Convenio=cd.No_Convenio JOIN Convenio_Desglosado_Detalles cde
on cde.Convenio_Desglosado_ID=cd.Convenio_Desglosado_ID
WHERE c.Estatus='PENDIENTE' and c.Tipo_Convenio='PEC'
GROUP BY cd.Convenio_Desglosado_ID,c.Adeudo,cd.Total,c.Folio_Convenio,c.Fecha_Creo
HAVING
 ABS( (SUM(cde.Importe) - c.Adeudo))>1


-- son los convenios que no checan con su convenio detallado los de tipo administrativo
SELECT  c.Fecha_Creo, c.Folio_Convenio, c.Adeudo,cd.Total,SUM(cde.Importe)
from Ope_Cor_Convenios c JOIN Convenio_Desglosado cd
on c.No_Convenio=cd.No_Convenio JOIN Convenio_Desglosado_Detalles cde
on cde.Convenio_Desglosado_ID=cd.Convenio_Desglosado_ID
WHERE c.Estatus='PENDIENTE' and c.Tipo_Convenio='ADMINISTRATIVO'
GROUP BY cd.Convenio_Desglosado_ID,c.Adeudo,cd.Total,c.Folio_Convenio,c.Fecha_Creo
HAVING
 ABS( (SUM(cde.Importe) - c.Adeudo))>1
 
 
 

-- convenios con mensualidades duplicados administrativos
SELECT c.Folio_Convenio,c.Tipo_Convenio,c.Estatus ,COUNT(cd.No_Pago)
FROM Ope_Cor_Convenios c JOIN Ope_Cor_Convenios_Detalles cd
on c.No_Convenio=cd.No_Convenio 
WHERE c.Tipo_Convenio='ADMINISTRATIVO' AND cd.No_Pago=0 and c.Estatus='PENDIENTE'
GROUP by c.Folio_Convenio,c.Tipo_Convenio,c.Estatus,cd.No_Pago
HAVING COUNT(cd.No_Pago)>1
ORDER BY c.Folio_Convenio

-- convenios con mensualidades duplicados pec
SELECT c.Folio_Convenio,c.Tipo_Convenio,c.Estatus ,COUNT(cd.No_Pago)
FROM Ope_Cor_Convenios c JOIN Ope_Cor_Convenios_Detalles cd
on c.No_Convenio=cd.No_Convenio 
WHERE c.Tipo_Convenio='PEC' AND cd.No_Pago=0 and c.Estatus='PENDIENTE'
GROUP by c.Folio_Convenio,c.Tipo_Convenio,c.Estatus,cd.No_Pago
HAVING COUNT(cd.No_Pago)>1
ORDER BY c.Folio_Convenio







SELECT c.No_Convenio, c.Folio_Convenio,c.RPU,c.Estatus as [estatus_convenio],c.Tipo_Convenio,
cd.Estatus as [estatus_mensualidad],cd.No_Pago,cd.Fecha_Pago,cd.Monto, cd.Subtotal,cd.Interes_Insoluto,cd.campo_interes_simapag,cd.campo_pagointeres_simapag,cd.campo_fecha_pago_simapag
FROM Ope_Cor_Convenios c JOIN Ope_Cor_Convenios_Detalles cd
on c.No_Convenio=cd.No_Convenio
WHERE c.Estatus='PENDIENTE' and c.Folio_Convenio=3263
ORDER by c.Folio_Convenio DESC


-- consulta para los convenios reportes
SELECT c.No_Convenio
    ,c.Adeudo
    ,c.Total_Descuento
	,c.Folio_Convenio
	,c.RPU
	,c.Estatus AS [estatus_convenio]
	,c.Tipo_Convenio
	,cd.Estatus AS [estatus_mensualidad]
	,cd.No_Pago
	,CONVERT(VARCHAR(10), cd.Fecha_Pago,103) as [fecha_vencimiento_mensualidad]
	,cd.Monto
	,cd.Subtotal
	,cd.Interes_Insoluto
	,cd.campo_interes_simapag
	,cd.campo_pagointeres_simapag
	,isnull(CONVERT(VARCHAR(10), cd.campo_fecha_pago_simapag,103),'') as [fecha_pago_simapag]
	,cd.Subtotal + cd.campo_pagointeres_simapag as [pago_simapag]
FROM Ope_Cor_Convenios c
JOIN Ope_Cor_Convenios_Detalles cd ON c.No_Convenio = cd.No_Convenio
--WHERE c.Estatus = 'PENDIENTE' 
ORDER BY c.Folio_Convenio DESC



SELECT SUM(TOTAL_RECIBOS) FROM Ope_Cor_Caj_Recibos_Cobros WHERE  LEN(LTRIM(RTRIM(no_pago_convenio)))>0


SELECT * from Ope_Cor_Convenios_Detalles WHERE Estatus='PENDIENTE' AND Saldo=0



