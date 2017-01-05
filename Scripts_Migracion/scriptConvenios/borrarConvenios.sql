

use Simapag_Praga

SELECT COUNT() FROM Ope_Cor_Caj_Recibos_Cobros

DELETE FROM Ope_Cor_Caj_Movimientos_Cobros WHERE NO_RECIBO IN(
SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE RTRIM( LTRIM(no_pago_convenio))>0)

DELETE from Ope_Cor_Caj_Recibos_Cobros WHERE RTRIM(LTRIM(no_pago_convenio))>0

DELETE FROM Ope_Cor_Caj_Recibos_Cobros where NO_RECIBO=3981104

DELETE from Ope_Cor_Convenios_Pagos_Detalle
DELETE FROM Ope_Cor_Convenios_Pagos


DELETE FROM Convenio_Desglosado_Detalles
DELETE from Convenio_Desglosado

DELETE FROM Ope_Cor_Convenios_Detalles
DELETE from Convenio_Estatus_Predios
DELETE FROM Ope_Cor_Convenio_Factura
DELETE from Ope_Cor_Convenios


ALTER table Ope_Cor_Convenios_Detalles add importe_pago_simpag numeric(15,2)


SELECT * FROM convadmvo WHERE convenio=749
SELECT * from pagadmvo WHERE convenio=749	
SELECT * from pagopagadmvo WHERE convenio=749

SELECT DISTINCT(convenio),estado FROM convadmvo WHERE pago_inici=0


