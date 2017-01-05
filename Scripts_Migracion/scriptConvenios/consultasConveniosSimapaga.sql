use Simpag_09_10_2016

SELECT estado,* FROM convadmvo WHERE convenio=3327
SELECT * from pagadmvo WHERE convenio=3327
SELECT * from pagopagadmvo WHERE convenio=3327

SELECT * from convpec ORDER by convenio
SELECT  intereses,pagointeres,fechapago,* from pagpec WHERE convenio=12
SELECT sum(intereses + importe) from pagopagpec WHERE convenio=12

SELECT * from sancion WHERE rpu='000851001956'

SELECT * from solicitud ORDER by fecha_alta DESC

SELECT suspendido,* from recibos WHERE rpu='000870200161'

SELECT Estatus,Tipo_Convenio,No_Convenio,Folio_Convenio from  Simapag_Praga.dbo.Ope_Cor_Convenios WHERE Tipo_Convenio='PEC'
AND Folio_Convenio=12

SELECT * from Simapag_Praga.dbo.Ope_Cor_Convenios_Detalles WHERE No_Convenio='0000000428'
SELECT * from Simapag_Praga.dbo.Ope_Cor_Convenios_Pagos WHERE No_Pago='0000000850'
SELECT * from Simapag_Praga.dbo.Ope_Cor_Convenios_Pagos_Detalle WHERE No_Pago='0000000850'

DELETE  FROM Simapag_Praga.dbo.Ope_Cor_Convenios_Pagos_Detalle

SELECT c.Folio_Convenio AS [no_convenio]
	,p.RPU AS [rpu]
	,p.No_Cuenta AS [no_cuenta]
	,(u.APELLIDO_PATERNO + ' ' + u.APELLIDO_MATERNO + ' ' + u.NOMBRE) AS [usuario]
	,col.NOMBRE AS [colonia]
	,l.NOMBRE AS [calle]
	,p.Numero_Exterior AS [numero_exterior]
	,p.Numero_Interior AS [numero_interior]
	,p.Manzana AS [manzana]
	,p.Lote AS [lote]
	,c.Tipo_Convenio AS [tipo_convenio]
	,c.Estatus AS [estatus]
	,CONVERT(VARCHAR(10), c.Fecha_Creo, 103) AS [fecha_elaboracion]
	,CONVERT(NUMERIC(15, 2), rc.TOTAL_RECIBOS) AS [pago]
	,CONVERT(VARCHAR(10), rc.FECHA, 103) AS [fecha_pago]
	,pcd.No_Mensualidad AS [no_pago]
	,ISNULL(pc.Interes_Pago_Vencido, 0) AS [intereses_moratorios]
	,CONVERT(NUMERIC(15, 2), ISNULL((
				SELECT SUM(cd.Saldo)
				FROM Ope_Cor_Convenios_Detalles cd
				WHERE cd.No_Convenio = c.No_Convenio
				), 0)) AS [saldo]
FROM Ope_Cor_Convenios c
JOIN Ope_Cor_Convenios_Pagos pc ON c.No_Convenio = pc.No_Convenio
JOIN Ope_Cor_Caj_Recibos_Cobros rc ON rc.no_pago_convenio = pc.No_Pago
JOIN Cat_Cor_Predios p ON p.Predio_ID = c.Predio_ID
JOIN Cat_Cor_Usuarios u ON u.USUARIO_ID = p.Usuario_ID
JOIN Cat_Cor_Colonias col ON col.COLONIA_ID = p.Colonia_ID
JOIN Cat_Cor_Calles l ON l.CALLE_ID = p.Calle_ID
JOIN Ope_Cor_Convenios_Pagos_Detalle pcd ON pcd.No_Pago = pc.No_Pago
WHERE LEN(c.No_Convenio) > 0
	AND p.RPU = '000860800837'
ORDER BY p.RPU
	,rc.FECHA_CREO
