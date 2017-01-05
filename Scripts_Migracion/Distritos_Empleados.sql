insert into
	ope_cor_asignacion_distritos_empleados
select 
	right('00000' + 
	cast(row_number() over(order by Empleado_ID) as varchar(5)), 5),
	Empleado_ID,
	case 
		when Empleado_ID <= 44 then '00001'
		when Empleado_ID > 44 then '00002'
	end,
	'MIGRACION',
	GETDATE(),
	NULL,
	NULL
from 
	cat_empleados 
where 
	Empleado_ID <> '0000000070'