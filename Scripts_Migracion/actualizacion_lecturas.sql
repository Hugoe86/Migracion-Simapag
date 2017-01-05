update ope_cor_ordenes_trabajo
set lectura = 
	(select top 1 lectura from simapag_09_01_2016.dbo.corte c where c.rpu = (
			select rpu from cat_cor_predios pp where pp.Predio_ID = ope_cor_ordenes_trabajo.Predio_ID
		)
		and c.cveejecucion = ope_cor_ordenes_trabajo.cveejecucion 
		and c.fecha = cast(ope_cor_ordenes_trabajo.Fecha as date) order by c.fecha desc)
where
	Predio_ID IN (
		select p.Predio_ID from cat_cor_predios p where p.CORTADO = 'SI'
	)
	and Tipo_Falla_ID IN ('00005', '00006')