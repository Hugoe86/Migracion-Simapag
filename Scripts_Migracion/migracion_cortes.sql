INSERT INTO OPE_COR_ORDENES_TRABAJO(
	No_Orden_Trabajo, Folio, Predio_ID, No_Cuenta, Fecha,
	Tipo_Falla_ID, Brigada_ID, Distrito_ID, Calle_ID, Colonia_ID,
	Giro_ID, Numero, Trabajo_Realizado, Estatus, Empleado_ID,cveejecucion,Observaciones
)
SELECT
	RIGHT('0000000000' + 
	CAST(ROW_NUMBER() OVER(ORDER BY c.fecha) 
	+ (select max(No_Orden_Trabajo) from ope_cor_ordenes_trabajo) AS VARCHAR(10)), 10),
	RIGHT('0000000000' + 
	CAST(ROW_NUMBER() OVER(ORDER BY c.fecha) 
	+ (select max(No_Orden_Trabajo) from ope_cor_ordenes_trabajo) AS VARCHAR(10)), 10), -- 0000254608
	p.Predio_ID,
	c.rpu,
	case 
		when c.fecha = '0203-06-02' then '2003-06-02'
		when c.fecha = '0200-06-30' then '2000-06-02'
		when c.fecha = '0201-11-10' then '2001-06-02'
		when c.fecha = '0442-04-07' then '2000-06-02'
		when c.fecha = '0460-05-08' then '2000-06-02'
		when c.fecha = '0576-01-10' then '2000-06-02'
		when c.fecha = '0610-01-11' then '2000-06-02'
		when c.fecha = '0630-01-11' then '2000-06-02'
		else c.fecha
	end fecha,
	'00005' AS Tipo_Falla, -- <<=== ID 5 para 5 y ID 6 para not 5
	'00003' AS Brigada,
	'00001',
	p.Calle_ID,
	p.Colonia_ID,
	p.Giro_actividad_id,
	p.Numero_Exterior,
	'SI',
	'TERMINADA',
	'0000000021'
	,c.cveejecucion
	,c.inspeccion
FROM
	Simapag_09_01_2016.dbo.corte c
	join CAT_COR_PREDIOS p on p.rpu = c.rpu
WHERE
	c.rpu <> ''
	and c.cveejecucion like '%5%';   ---- corte definitivo 5 corte otro <>5 
	
	
-----------------------------------------------------------------------------------------------------------
	

INSERT INTO OPE_COR_ORDENES_TRABAJO(
	No_Orden_Trabajo, Folio, Predio_ID, No_Cuenta, Fecha,
	Tipo_Falla_ID, Brigada_ID, Distrito_ID, Calle_ID, Colonia_ID,
	Giro_ID, Numero, Trabajo_Realizado, Estatus, Empleado_ID,cveejecucion,Observaciones
)
SELECT
	RIGHT('0000000000' + 
	CAST(ROW_NUMBER() OVER(ORDER BY c.fecha) 
	+ (select max(No_Orden_Trabajo) from ope_cor_ordenes_trabajo) AS VARCHAR(10)), 10),
	RIGHT('0000000000' + 
	CAST(ROW_NUMBER() OVER(ORDER BY c.fecha) 
	+ (select max(No_Orden_Trabajo) from ope_cor_ordenes_trabajo) AS VARCHAR(10)), 10), -- 0000254608
	p.Predio_ID,
	c.rpu,
	case 
		when c.fecha = '0203-06-02' then '2003-06-02'
		when c.fecha = '0200-06-30' then '2000-06-02'
		when c.fecha = '0201-11-10' then '2001-06-02'
		when c.fecha = '0442-04-07' then '2000-06-02'
		when c.fecha = '0460-05-08' then '2000-06-02'
		when c.fecha = '0576-01-10' then '2000-06-02'
		when c.fecha = '0610-01-11' then '2000-06-02'
		when c.fecha = '0630-01-11' then '2000-06-02'
		else c.fecha
	end fecha,
	'00006' AS Tipo_Falla, -- <<=== ID 5 para 5 y ID 6 para not 5
	'00003' AS Brigada,
	'00001',
	p.Calle_ID,
	p.Colonia_ID,
	p.Giro_actividad_id,
	p.Numero_Exterior,
	'SI',
	'TERMINADA',
	'0000000021'
	,c.cveejecucion
	,c.inspeccion
FROM
	Simapag_09_01_2016.dbo.corte c
	join CAT_COR_PREDIOS p on p.rpu = c.rpu
WHERE
	c.rpu <> ''
	and c.cveejecucion NOT like '%5%';