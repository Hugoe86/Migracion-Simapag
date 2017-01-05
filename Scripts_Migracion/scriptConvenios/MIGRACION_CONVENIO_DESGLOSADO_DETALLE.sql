use Simapag_09_01_2016

-- crear las siguientes vistas
CREATE view  View_Detalle_Factura_Simapag
as
SELECT dcto
	,rpu
	,convenio
	,concepto
	,monto
FROM (
	SELECT dcto
		,rpu
		,convenio
		,agua
		,alcan
		,sanea
		,rezagua
		,rezalcan
		,rezsanea
		,recagua
		,recalcan
		,recsanea
		,iva
	FROM convadmvo
	) p
UNPIVOT(monto FOR concepto IN (
			agua
			,alcan
			,sanea
			,rezagua
			,rezalcan
			,rezsanea
			,recagua
			,recalcan
			,recsanea
			,iva
			)) AS unpvt;

----------------------------------------------------------------------------------

CREATE view View_Detalle_Factura_Simapag_Descuentos_Recargos
as
SELECT dcto
	,rpu
	,convenio
	,concepto
	,monto
FROM (
	SELECT dcto
		,rpu
		,convenio
		,CASE 
			WHEN dcto > 0 and recagua>0 and recalcan>0 AND rezsanea>0
				THEN 
				     recagua *(((agua + alcan + sanea + rezagua +  rezalcan + rezsanea + recagua + recsanea + recalcan + iva)- total) /
				              (recagua + recalcan + recsanea)) * -1
			END AS descuento_recargo_agua
		,CASE 
			WHEN dcto > 0 and recagua>0 and recalcan>0 AND rezsanea>0
				THEN 
				  recsanea *(((agua + alcan + sanea + rezagua +  rezalcan + rezsanea + recagua + recsanea + recalcan + iva)- total) /
				              (recagua + recalcan + recsanea)) *-1
			END AS descuento_recargo_sanamiento
		,CASE 
			WHEN dcto > 0 and recagua>0 and recalcan>0 AND rezsanea>0
				THEN 
				   recalcan *(((agua + alcan + sanea + rezagua +  rezalcan + rezsanea + recagua + recsanea + recalcan + iva)- total) /
				              (recagua + recalcan + recsanea)) * -1
			END AS descuento_recargo_alcantarillado
	FROM convadmvo
	) p
UNPIVOT(monto FOR concepto IN (
			descuento_recargo_agua
			,descuento_recargo_sanamiento
			,descuento_recargo_alcantarillado
			)) AS unpvt;


--------------------------------------------------------------------------------------------------

 create view View_Detalle_Completo_Simapag
 as     
SELECT * FROM View_Detalle_Factura_Simapag
UNION
SELECT * FROM View_Detalle_Factura_Simapag_Descuentos_Recargos

------------------------------------------------------------------------------------------------

--crear el insert
INSERT INTO Simapag_20161015.dbo.Convenio_Desglosado_Detalles
			(Convenio_Desglosado_ID, Concepto_ID, Importe, Porcentaje)
SELECT  cd.Convenio_Desglosado_ID,
 CASE
    WHEN v.concepto= 'agua' THEN 
                               CASE
                                    WHEN ga.Descripcion = 'DO' THEN '00001'
                                    ELSE
                                     '00010'
                               end
    WHEN v.concepto= 'alcan' THEN '00002'
    WHEN v.concepto= 'sanea' THEN '00003'
   
 
    WHEN v.concepto= 'rezagua' THEN '00004'
    WHEN v.concepto= 'rezalcan' THEN '00005'
    WHEN v.concepto= 'rezsanea' THEN '00006'
   
    WHEN v.concepto= 'recagua' THEN '00007'
    WHEN v.concepto= 'recalcan' THEN '00008'
    WHEN v.concepto= 'recsanea' THEN '00009'
    
    WHEN v.concepto= 'descuento_recargo_agua' THEN '00156'
    WHEN v.concepto= 'descuento_recargo_alcantarillado' THEN '00157'
    WHEN v.concepto= 'descuento_recargo_sanamiento' THEN '00158'
    WHEN v.concepto='iva' THEN '00014'
    
 end  as [concepto_id],CONVERT(NUMERIC(18,2), v.monto) as [monto],
 CASE
   WHEN cd.Total=0 THEN 0
   ELSE
   CONVERT( NUMERIC(18,2), (v.monto * 100)/cd.Total) 
 end  as [porcentaje]
from View_Detalle_Completo_Simapag v JOIN Simapag_20161015.dbo.Ope_Cor_Convenios c
on v.convenio=c.Folio_Convenio JOIN Simapag_20161015.dbo.Convenio_Desglosado cd
on cd.No_Convenio=c.No_Convenio LEFT JOIN Simapag_20161015.dbo.Cat_Cor_Predios p
on p.Predio_ID=c.Predio_ID LEFT JOIN Simapag_20161015.dbo.Cat_Cor_Giros_Actividades ga
on ga.Actividad_Giro_ID= p.Giro_Actividad_ID 
WHERE c.Tipo_Convenio='ADMINISTRATIVO' and v.monto<>0
ORDER BY cd.Convenio_Desglosado_ID,v.convenio

