INSERT INTO Simapag_20161015.dbo.OPE_COR_ORDENES_TRABAJO(
	No_Orden_Trabajo, Folio, Predio_ID, No_Cuenta, Fecha,
	Tipo_Falla_ID, Brigada_ID, Distrito_ID, Calle_ID, Colonia_ID,
	Giro_ID, Numero, Trabajo_Realizado, Estatus, Empleado_ID,Observaciones
)
SELECT
	RIGHT('0000000000' + 
	CAST(ROW_NUMBER() OVER(ORDER BY c.fecha) 
	+ (select max(No_Orden_Trabajo) from Simapag_20161015.dbo.ope_cor_ordenes_trabajo) AS VARCHAR(10)), 10),
	RIGHT('0000000000' + 
	CAST(ROW_NUMBER() OVER(ORDER BY c.fecha) 
	+ (select max(No_Orden_Trabajo) from Simapag_20161015.dbo.ope_cor_ordenes_trabajo) AS VARCHAR(10)), 10), -- 0000254608
	p.Predio_ID,
	c.rpu,
	case 
		when c.fecha = '0014-08-04' then '2014-08-04'
		when c.fecha = '0204-08-05' then '2004-08-05'
		when c.fecha = '0209-02-16' then '2009-02-16'
		when c.fecha = '3016-02-12' then '2016-02-12'
		when c.fecha = '0201-11-10' then '2001-11-10'
		when c.fecha = '0442-04-07' then '2002-04-07'
		when c.fecha = '0610-01-11' then '2000-06-02'
		when c.fecha = '0630-01-11' then '2000-06-02'
		when c.fecha = '3016-02-12' then '2016-02-12'
		when c.fecha = '0460-05-08' then '2000-06-02'
		when c.fecha = '0576-01-10' then '2000-06-02'
		when c.fecha = '0216-08-12' then '2000-06-02'
			when c.fecha = '0576-01-10' then '2000-06-02'
		when c.fecha = '0016-08-26' then '2000-06-02'
			when c.fecha = '0216-08-26' then '2000-06-02'
	
		else c.fecha
	end fecha,
	'00012' AS Tipo_Falla,
	'00002' AS Brigada,
	'00001' as DIstrito,
	p.Calle_ID,
	p.Colonia_ID,
	p.Giro_actividad_id,
	p.Numero_Exterior,
	'SI',
	'TERMINADA',
	'0000000021'
	,c.inspeccion
FROM
	Simapag_09_01_2016.dbo.inspeccion c
	join Simapag_20161015.dbo.Cat_Cor_Predios p on p.rpu = c.rpu
WHERE
	c.rpu <> ''
	