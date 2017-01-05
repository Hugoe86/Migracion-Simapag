
--*************************************************
----- Buscar fecha_creo de lecturas y borrarlas
--*************************************************

select fecha_creo, count(*) from Cat_Cor_Medidores_Lecturas_Detalles group by Fecha_Creo

select count(*) from Cat_Cor_Medidores_Lecturas_Detalles where Fecha_Creo = '2016-11-10 13:02:31.903'
union 
select count(*) from Cat_Cor_Medidores_Lecturas_Detalles where Fecha_Creo = '2016-16-10 01:03:16.067'

--2016-10-02 01:12:44.073

delete from Cat_Cor_Medidores_Lecturas_Detalles where Fecha_Creo = '2016-16-10 16:26:03.999'


--*****************************************
-------- Inserta lecturas actuales
--*****************************************

--INSERT INTO Cat_Cor_Medidores_Lecturas_Detalles
--			(Medidor_Detalle_ID, Predio_ID, No_Cuenta, Clave_Anomalia_ID_1,
--			 Estatus, Facturado, Lectura, Lectura_Anterior, Lectura_Facturacion,
--			 Consumo_Facturacion, Medidor_ID, Bimestre, Fecha_Lectura, Anio, No_Medidor,
--			 Usuario_Creo, Fecha_Creo,Periodo_Inicial,Periodo_Final)

--SELECT RIGHT('0000000000' + CAST(ROW_NUMBER() OVER(ORDER BY R.rpu) 
--		+(select isnull(max(Medidor_Detalle_ID), 0) from Cat_Cor_Medidores_Lecturas_Detalles) AS VARCHAR(10)), 10),
--		P.Predio_ID,R.cuenta,null,'APLICADA','NO',R.lecactual,R.lecanterior,R.lecactual,R.consumo,
--		(SELECT TOP 1 SBCA.MEDIDOR_ID FROM Cat_Cor_Predios_Medidores AS SBCA WHERE SBCA.PREDIO_ID = P.Predio_ID),
--		MONTH(dateadd(MONTH,-1,R.flecactual)),R.flecactual,year(dateadd(MONTH,-1,R.flecactual)),R.nummedidor,'MIGRACION LECACTUAL',GETDATE(),
--		CASE WHEN   ISDATE( LEFT(periodo, 10))=1 THEN convert(date, LEFT(periodo, 10), 103)
--					         ELSE dateadd(MONTH,-1,R.flecactual) END,
--		CASE WHEN   ISDATE( RIGHT(periodo, 10))=1 THEN convert(date, RIGHT(periodo, 10), 103)
--					         ELSE R.flecactual END			         
--FROM Simapag_09_01_2016.dbo.recibos R JOIN Cat_Cor_Predios P ON
--	R.rpu = P.rpu 
--WHERE R.rpu NOT IN
--	(SELECT SBCA.rpu FROM 
--		(SELECT rpu,MAX(fechalect) fecha_lec FROM Simapag_09_01_2016.dbo.hislecturas GROUP BY rpu) AS SBCA
--	WHERE YEAR(SBCA.fecha_lec)=2016 AND (MONTH(SBCA.fecha_lec)=9 OR MONTH(SBCA.fecha_lec)=10) )
--AND year(R.flecactual)=2016 AND (MONTH(R.flecactual)=9 OR MONTH(R.flecactual)=10) -- <===---------------------- MESES ACTUALES
	
	
--**********************************************
----- Actualizar campo Estimado en Facturas
--**********************************************

--select * from ope_cor_facturacion_recibos

update ope_cor_facturacion_recibos set estimado = 'NO'

update ope_cor_facturacion_recibos set estimado = 'SI' 
where no_recibo IN
	(select cast(foliorecib as varchar(10)) from simapag_09_01_2016.dbo.recibos where estimado = 1)
		
----- Para el historico ---------------

update ope_cor_facturacion_recibos set estimado = 'SI' 
where no_recibo IN
	(select cast(foliorecib as varchar(10)) from simapag_09_01_2016.dbo.hisrecibos where estimado = 1)


--*************************************
--------- Bandera "2 facturas" 
--*************************************

update cat_cor_predios set Sin_Historial = 'SI' where rpu IN
(select distinct rpu from ope_cor_facturacion_recibos
	where usuario_creo = 'Migracion Caso 6')
	
	
--*************************************
------ Redondeo en Recibos Cobros
--*************************************

update ope_cor_caj_recibos_cobros set redondeo = 0