use Simapag_09_01_2016


-- crear las siguientes vistas
create view View_Detalle_Factura_Simapag_PEC
as
SELECT rpu
	,convenio
	,concepto
	,monto
FROM (
	SELECT rpu
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
	FROM convpec
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
			
------------------------------------------------------------------------------------

CREATE view View_Detalle_Factura_Simapag_Descuentos_Recargos_PEC
as
SELECT
	rpu
	,convenio
	,concepto
	,monto
FROM (
	SELECT
		rpu
		,convenio
		,CASE 
			WHEN  recagua>0 and recalcan>0 AND rezsanea>0
				THEN 
				     recagua *(((agua + alcan + sanea + rezagua +  rezalcan + rezsanea + recagua + recsanea + recalcan + iva)- total) /
				              (recagua + recalcan + recsanea)) * -1
			END AS descuento_recargo_agua
		,CASE 
			WHEN   recagua>0 and recalcan>0 AND rezsanea>0
				THEN 
				  recsanea *(((agua + alcan + sanea + rezagua +  rezalcan + rezsanea + recagua + recsanea + recalcan + iva)- total) /
				              (recagua + recalcan + recsanea)) *-1
			END AS descuento_recargo_sanamiento
		,CASE 
			WHEN   recagua>0 and recalcan>0 AND rezsanea>0
				THEN 
				   recalcan *(((agua + alcan + sanea + rezagua +  rezalcan + rezsanea + recagua + recsanea + recalcan + iva)- total) /
				              (recagua + recalcan + recsanea)) * -1
			END AS descuento_recargo_alcantarillado
	FROM convpec
	) p
UNPIVOT(monto FOR concepto IN (
			descuento_recargo_agua
			,descuento_recargo_sanamiento
			,descuento_recargo_alcantarillado
			)) AS unpvt;

--------------------------------------------------------------------------------------------

 create view View_Detalle_Completo_Simapag_PEC
 as     
   SELECT * FROM View_Detalle_Factura_Simapag_PEC
UNION
  SELECT * FROM View_Detalle_Factura_Simapag_Descuentos_Recargos_PEC

--------------------------------------------------------------------------------------------

--- inserte
INSERT INTO Simapag_20161015.dbo.Convenio_Desglosado_Detalles
			(Convenio_Desglosado_ID, Concepto_ID, Importe, Porcentaje)
SELECT 
cd.Convenio_Desglosado_ID,
 CASE
    WHEN vp.concepto= 'agua' THEN 
                               CASE
                                    WHEN ga.Descripcion = 'DO' THEN '00001'
                                    ELSE
                                     '00010'
                               end
    WHEN vp.concepto= 'alcan' THEN '00002'
    WHEN vp.concepto= 'sanea' THEN '00003'
   
 
    WHEN vp.concepto= 'rezagua' THEN '00004'
    WHEN vp.concepto= 'rezalcan' THEN '00005'
    WHEN vp.concepto= 'rezsanea' THEN '00006'
   
    WHEN vp.concepto= 'recagua' THEN '00007'
    WHEN vp.concepto= 'recalcan' THEN '00008'
    WHEN vp.concepto= 'recsanea' THEN '00009'
    
    WHEN vp.concepto= 'descuento_recargo_agua' THEN '00156'
    WHEN vp.concepto= 'descuento_recargo_alcantarillado' THEN '00157'
    WHEN vp.concepto= 'descuento_recargo_sanamiento' THEN '00158'
    WHEN vp.concepto='iva' THEN '00014'
    
 end  as [concepto_id],CONVERT(NUMERIC(18,2), vp.monto) as [monto],
 CASE
   WHEN cd.Total=0 THEN 0
   ELSE
   CONVERT( NUMERIC(18,2), (vp.monto * 100)/cd.Total) 
 end  as [porcentaje]
FROM View_Detalle_Completo_Simapag_PEC vp JOIN Simapag_20161015.dbo.Ope_Cor_Convenios c
on vp.convenio=c.Folio_Convenio JOIN Simapag_20161015.dbo.Convenio_Desglosado cd
on cd.No_Convenio=c.No_Convenio LEFT JOIN Simapag_20161015.dbo.Cat_Cor_Predios p
on p.Predio_ID=c.Predio_ID LEFT JOIN Simapag_20161015.dbo.Cat_Cor_Giros_Actividades ga
on ga.Actividad_Giro_ID= p.Giro_Actividad_ID
WHERE  vp.monto<>0 and c.Tipo_Convenio='PEC'
ORDER BY cd.Convenio_Desglosado_ID,vp.convenio