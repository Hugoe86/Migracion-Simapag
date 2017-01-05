use Simapag_20161015

--Calcula los intereses insolutos con el 1% solo para los convenios pendientes
update  Ope_Cor_Convenios_Detalles set Interes_Insoluto =
				ROUND((SELECT top 1 (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
				cd.No_Pago + 1) * 1 * cd.Subtotal)/100
				from Ope_Cor_Convenios_Detalles cd
				WHERE cd.No_Convenio=Ope_Cor_Convenios_Detalles.No_Convenio
				and Ope_Cor_Convenios_Detalles.No_Pago=cd.No_Pago
				),0)
WHERE  No_Pago<>0
and No_Convenio in (
SELECT No_Convenio FROM Ope_Cor_Convenios WHERE YEAR(Fecha_Creo) IN (2016,2015,2014,2013)
--AND Estatus='PENDIENTE'
)

--Calcula los intereses insolutos con el 2% solo para los convenios pendientes
update  Ope_Cor_Convenios_Detalles set Interes_Insoluto =
				ROUND((SELECT top 1 (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
				cd.No_Pago + 1) * 2 * cd.Subtotal)/100
				from Ope_Cor_Convenios_Detalles cd
				WHERE cd.No_Convenio=Ope_Cor_Convenios_Detalles.No_Convenio
				and Ope_Cor_Convenios_Detalles.No_Pago=cd.No_Pago
				),0)
WHERE  No_Pago<>0
and No_Convenio in (
SELECT No_Convenio FROM Ope_Cor_Convenios WHERE YEAR(Fecha_Creo) IN (2012,2011,2010)
--AND Estatus='PENDIENTE'
)
--Calcula los intereses insolutos con el 0% solo para los convenios pendientes
update  Ope_Cor_Convenios_Detalles set Interes_Insoluto =
				ROUND((SELECT top 1 (((SELECT max(cd1.No_Pago) FROM Ope_Cor_Convenios_Detalles  cd1 WHERE cd1.No_Convenio=cd.No_Convenio)-
				cd.No_Pago + 1) * 0 * cd.Subtotal)/100
				from Ope_Cor_Convenios_Detalles cd
				WHERE cd.No_Convenio=Ope_Cor_Convenios_Detalles.No_Convenio
				and Ope_Cor_Convenios_Detalles.No_Pago=cd.No_Pago
				),0)
WHERE  No_Pago<>0
and No_Convenio in (
SELECT No_Convenio FROM Ope_Cor_Convenios WHERE YEAR(Fecha_Creo) IN (2009,2008,2007,2006,2005)
--AND Estatus='PENDIENTE'
)



-- acualiza los montos, abano y saldo
UPDATE Ope_Cor_Convenios_Detalles SET Monto=Subtotal + Interes_Insoluto
WHERE  No_Convenio in (
SELECT No_Convenio FROM Ope_Cor_Convenios --WHERE Estatus='PENDIENTE'
)
--------------------------------------------------------------------------------------
UPDATE Ope_Cor_Convenios_Detalles SET Abono= (CASE 
                                                WHEN ( isnull(campo_pagointeres_simapag,0) + isnull(importe_pago_simpag,0))> Monto THEN
                                                       Monto
                                                    ELSE
                                                      isnull(campo_pagointeres_simapag,0) + isnull(importe_pago_simpag,0)
                                              end)
---------------------------------------------------------------------------------------

UPDATE Ope_Cor_Convenios_Detalles SET Saldo=Monto - Abono

---------------------------------------------------------------------------------------

UPDATE Ope_Cor_Convenios_Detalles SET Estatus=(CASE 
                                                 WHEN  Monto=Abono THEN
                                                      'PAGADO'
                                                  ELSE
                                                      'PENDIENTE'
                                              end)
                                              
---------------------------------------------------------------------------------------
                                              
UPDATE Ope_Cor_Convenios SET Estatus = (
                                        CASE
                                          WHEN(SELECT SUM(cd.Saldo)FROM Ope_Cor_Convenios_Detalles cd 
                                          WHERE cd.No_Convenio=Ope_Cor_Convenios.No_Convenio) = 0 THEN 'PAGADO'
                                          ELSE
                                            'PENDIENTE'
                                        end 
                                       )
 where Estatus='PENDIENTE'

--UPDATE Ope_Cor_Convenios_Detalles set Abono=Monto
--WHERE Estatus='PAGADO' AND
--No_Convenio in (
--SELECT No_Convenio FROM Ope_Cor_Convenios WHERE  Estatus='PENDIENTE'
--)

--UPDATE Ope_Cor_Convenios_Detalles set Saldo=Monto
--WHERE Estatus='PENDIENTE'


UPDATE Ope_Cor_Convenios set saldo_convenio=
(SELECT SUM(cd.Saldo) from Ope_Cor_Convenios_Detalles cd WHERE cd.No_Convenio= Ope_Cor_Convenios.No_Convenio)


-- actualizamos en la tabla de convenio en el campo de intereses

UPDATE Ope_Cor_Convenios set Porcentaje_Interes=1
WHERE  YEAR(Fecha_Creo) IN (2016,2015,2014,2013)

UPDATE Ope_Cor_Convenios set Porcentaje_Interes=2
WHERE  YEAR(Fecha_Creo) IN (2012,2011,2010)

UPDATE Ope_Cor_Convenios set Porcentaje_Interes=0
WHERE  YEAR(Fecha_Creo) IN (2009,2008,2007,2006,2005)




-- inserta en la tabla de pagos convenios

INSERT INTO Ope_Cor_Convenios_Pagos(No_Pago,No_Convenio,Total_Pago,Estatus,Usuario_Creo
,Fecha_Creo,Interes_Pago_Vencido,Comentarios,Codigo_Barras)

SELECT RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (
						ORDER BY c.Fecha_Creo
						)))), 10) AS ID
	,c.No_Convenio AS [no_convenio]
	,(cd.campo_pagointeres_simapag + cd.importe_pago_simpag) AS [total_pago]
	,'PAGADO' AS [estatus]
	,'MIGRACION' as [usuario_creo]
    ,campo_fecha_pago_simapag AS [fecha_creo]
	,CASE 
		WHEN (cd.campo_pagointeres_simapag - cd.Interes_Insoluto) > 0
			THEN cd.campo_pagointeres_simapag - cd.Interes_Insoluto
		ELSE 0
		END AS [interes_pago_vencido]
	,cd.No_Pago AS comentario
	,RIGHT('000000000' + LTRIM(RTRIM(STR(ROW_NUMBER() OVER (
						ORDER BY c.Fecha_Creo
						)))), 10) + 'C'
FROM Ope_Cor_Convenios c
JOIN Ope_Cor_Convenios_Detalles cd ON c.No_Convenio = cd.No_Convenio
WHERE --c.Estatus = 'PENDIENTE'
	--AND 
	--cd.Estatus = 'PAGADO'
	cd.campo_fecha_pago_simapag is not null


--------------------------------------------------------------------------------------------------

--inserta en la tabla pagos detalles

INSERT INTO Ope_Cor_Convenios_Pagos_Detalle(No_Pago,No_Convenio,No_Mensualidad)
SELECT No_Pago,No_Convenio, CONVERT(int, Comentarios)
from Ope_Cor_Convenios_Pagos 

--- inserta en la table recibos cobros

insert into Ope_Cor_Caj_Recibos_Cobros(RPU,CODIGO_BARRAS,RECIBOS_COBRAR,
TOTAL_RECIBOS,PAGO_EFECTIVO,FECHA,USUARIO_CREO,FECHA_CREO,estado_recibo,
no_pago_convenio,periodo,SALDO,CAMBIO)

SELECT  c.RPU,ISNULL( p.Codigo_Barras,'') as codigo_barras,
1 as recibos_cobrar, p.Total_Pago as [total_recibos],
p.Total_Pago as [efectivo],
p.Fecha_Creo as [fecha],'Migracion' as [usuario_creo],
p.Fecha_Creo as [fecha_creo],'ACTIVO' as [estatus],p.No_Pago as [no_pago_convenio],
'Pago No.' + p.Comentarios + '/' + CONVERT(VARCHAR, c.Mensualidades_Adeudo)
 as [periodo],0,0
from Ope_Cor_Convenios_Pagos p JOIN Ope_Cor_Convenios c
on p.No_Convenio=c.No_Convenio
WHERE p.Total_Pago is not NULL


-- insert en la tabla de moviemientos cobros
INSERT Ope_Cor_Caj_Movimientos_Cobros(NO_RECIBO,CONCEPTO_ID,IMPORTE,FECHA_MOVIMIENTO,Facturado,
COMENTARIOS,USUARIO_CREO,FECHA_CREO,estado_concepto_cobro,
impuesto,total,RPU)
SELECT rc.NO_RECIBO , cdd.Concepto_ID,(cp.Total_Pago * cdd.Porcentaje)/100 as [importe],
cp.Fecha_Creo as [fecha_movimiento],'N' as [factuado],'Convenio' as [comentario],'MIGRACION' AS USUARIO_CREO,
GETDATE() as [fecha_creo],'ACTIVO' AS [estado_concepto_cobro],0 as impuesto,0 as total,p.RPU
FROM Ope_Cor_Convenios p JOIN Ope_Cor_Convenios_Pagos cp
on cp.No_Convenio=p.No_Convenio JOIN Convenio_Desglosado cd
on cd.No_Convenio=p.No_Convenio JOIN Convenio_Desglosado_Detalles cdd
on cdd.Convenio_Desglosado_ID=cd.Convenio_Desglosado_ID JOIN Ope_Cor_Convenios_Pagos_Detalle pd
on pd.No_Pago=cp.No_Pago JOIN Ope_Cor_Caj_Recibos_Cobros rc
on rc.no_pago_convenio=cp.No_Pago
GROUP by pd.No_Mensualidad,cp.Total_Pago,cdd.Convenio_Desglosado_ID,cdd.Porcentaje,cdd.Concepto_ID,
rc.NO_RECIBO,cp.Fecha_Creo,p.RPU


-- actualzar el iva

UPDATE Ope_Cor_Caj_Recibos_Cobros set total_iva_cobrado=
(SELECT top 1 mc.IMPORTE from  Ope_Cor_Caj_Movimientos_Cobros mc WHERE mc.NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO
and mc.CONCEPTO_ID='00014')
WHERE NO_RECIBO IN (SELECT  NO_RECIBO from  Ope_Cor_Caj_Recibos_Cobros WHERE LEN(LTRIM(RTRIM(no_pago_convenio)))>0)

UPDATE Ope_Cor_Caj_Recibos_Cobros set importe_cobrado=
(SELECT sum( mc.IMPORTE) from  Ope_Cor_Caj_Movimientos_Cobros mc WHERE mc.NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO
and mc.CONCEPTO_ID <> '00014')
WHERE NO_RECIBO IN (SELECT  NO_RECIBO from  Ope_Cor_Caj_Recibos_Cobros WHERE LEN(LTRIM(RTRIM(no_pago_convenio)))>0)

-- pagar unos convenios

UPDATE Ope_Cor_Convenios set Estatus='PAGADO'
WHERE Folio_Convenio in (11,12,13)  AND Tipo_Convenio='PEC'


UPDATE Ope_Cor_Convenios SET Estatus='PAGADO'
WHERE Folio_Convenio IN (
1006,51,65,107,123,331,348,370,371,372,430,473,478,489,504,
522,549,559,634,637,639,641,642,643,648,654,662,668,669,682,683,
686,690,691,692,697,699,700,703,706,710,715,718,723,255,507) AND Tipo_Convenio='ADMINISTRATIVO'


-- actualiza los abanos que se dieron de alta

SELECT DISTINCT(c.Folio_Convenio),c.RPU,c.Tipo_Convenio
FROM Ope_Cor_Convenios c JOIN Ope_Cor_Convenios_Detalles cd
on c.No_Convenio=cd.No_Convenio
WHERE c.Estatus='PENDIENTE' and cd.Estatus='PENDIENTE' AND
cd.Abono>0 and cd.Saldo>0
ORDER by c.Tipo_Convenio DESC


--UPDATE Ope_Cor_Convenios_Detalles set Saldo=Monto- Abono WHERE
--Estatus='PENDIENTE' AND Abono>0 AND Saldo>0