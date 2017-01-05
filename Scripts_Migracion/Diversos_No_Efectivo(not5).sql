
CREATE view View_Insert_Diverso_Linea_
 as 
SELECT  
 DISTINCT( c.rpu)
	,p.Usuario_ID
	,0 as [no_cuenta]
	, isnull (u.NOMBRE,'')+' '+isnull(u.APELLIDO_PATERNO,'')+' '+isnull(u.APELLIDO_MATERNO,'') as [nombre]
	,col.NOMBRE as [colonia]
	,ISNULL(cal.NOMBRE,'') as [direccion]
	,'01-01-2015' as [fecha_limite_pago]
	,'01-01-2015' as [fecha_emicion]
	,16.00 as [tasa_iva]
	,0 as [metros_cubicos]
	,0 as [precio_metros_cubicos]
	,160 as [importe]
	,25.60 as [iva]
	,185.60 as [total]
	,0 as [saldo_descarga]
	,0 as [saldo_economico]
	,'DIVERSOS' as [tipo_de_diverso]
	,'PENDIENTE' as [estatus]
	,'NO' as [convenio]
	
FROM Simapag_09_01_2016.dbo.corte as c
INNER JOIN Simapag_20161015.dbo.Cat_Cor_Predios as p ON p.RPU = c.rpu
JOIN Simapag_20161015.dbo.Cat_Cor_Usuarios as u on u.USUARIO_ID=p.Usuario_ID
JOIN Simapag_20161015.dbo.Cat_Cor_Colonias as Col on col.COLONIA_ID = p.Colonia_ID
JOIN Simapag_20161015.dbo.Cat_Cor_Calles as cal on cal.CALLE_ID= p.Calle_ID
WHERE  isnumeric(c.rpu) = 1  and c.cveejecucion NOT like '%5%' and c.rpu in (SELECT r.rpu from Simapag_09_01_2016.dbo.recibos as r WHERE r.corte=1)


INSERT INTO Simapag_20161015.dbo.Ope_Cor_Diversos
(No_Diverso,Codigo_Barras,RPU,Usuario_ID,No_Cuenta,Nombre,Colonia,Domicilio,Fecha_Limite_Pago,Fecha_Emision,Tasa_IVA,Metros_Cubicos,Precio_Metro_Cubico,Importe,IVA,Total,Saldo_Descarga,Saldo_Economico,Tipo_Diverso,Estatus,Convenio)

SELECT  RIGHT('0000000000' + 
	 CAST(ROW_NUMBER() OVER(ORDER BY vista.rpu) 
	 +(select max(No_Diverso) from Simapag_20161015.dbo.Ope_Cor_Diversos) AS VARCHAR(10)), 10)
	 ,RIGHT('0000000000' + 
	 CAST(ROW_NUMBER() OVER(ORDER BY vista.rpu) 
	 +(select max(No_Diverso) from Simapag_20161015.dbo.Ope_Cor_Diversos) AS VARCHAR(10)), 10)+'D',*
FROM View_Insert_Diverso_Linea_ vista




