INSERT INTO Simapag_20161015.dbo.Ope_Cor_Diversos_Detalles 
(Ope_Cor_Diversos.No_Diverso,Concepto_ID,Concepto,Cantidad,Precio_Unitario,Importe,Saldo,Estatus,Fecha_Limite_Pago)
SELECT
	d.No_Diverso
	,CASE
		WHEN d.Importe = 160 THEN '00104'
		ELSE '00105'
	END AS Consepto_ID
	,CASE
		WHEN d.Importe = 160 THEN 'RECONEX DE TOMA EN EL MEDIDOR O EN CUADRO DE MED'
		ELSE 'RECONEX DE TOMA EN LA RED DE DISTRIBUCIÓN'
	END AS Consepto
	,1 as Cantidad
	,d.Importe as precio_unitario
	,d.Importe as importes 
	,d.Importe as saldo
	,'PENDIENTE' as pendiente 
	,d.Fecha_Limite_Pago
	
FROM Simapag_20161015.dbo.Ope_Cor_Diversos AS d

--INSERT INTO Simapag_20161015.dbo.Ope_Cor_Diversos_Detalles 
--(Ope_Cor_Diversos.No_Diverso,Concepto_ID,Concepto,Cantidad,Precio_Unitario,Importe,Saldo,Estatus,Fecha_Limite_Pago)
--SELECT
--	d.No_Diverso
--	,'00014' AS Consepto_ID
--	,'IVA' AS Consepto
--	,1 as Cantidad
--	,d.IVA as precio_unitario
--	,d.IVA as importes 
--	,d.IVA as saldo
--	,'PENDIENTE' as pendiente 
--	,d.Fecha_Limite_Pago
	
--FROM Simapag_20161015.dbo.Ope_Cor_Diversos AS d
