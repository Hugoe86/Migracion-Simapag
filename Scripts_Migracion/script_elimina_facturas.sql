
--- Indices creados
ALTER DATABASE Simapag_20161015 SET RECOVERY SIMPLE WITH NO_WAIT
USE [Simapag_20161015]
GO
CREATE NONCLUSTERED INDEX [Idx_NO_RECIBO] ON [dbo].[Ope_Cor_Caj_Movimientos_Cobros] 
(
    [NO_RECIBO] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

--- Recibos_Detalle
USE [Simapag_20161015]
GO
CREATE NONCLUSTERED INDEX [Idx_Importe_Concepto] ON [dbo].[Ope_Cor_Facturacion_Recibos_Detalles] 
(
    [Importe] ASC,
    [Concepto_ID] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

USE [Simapag_20161015]
GO
CREATE NONCLUSTERED INDEX [Idx_RPU_Usuario_Creo] ON [dbo].[Ope_Cor_Caj_Movimientos_Cobros] 
(
    [RPU] ASC,
    [USUARIO_CREO] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


DBCC SHRINKFILE([Simapag_20161015_log], 1)

--Cambiar "select *" por "delete"
DELETE FROM Ope_Cor_Caj_Movimientos_Cobros WHERE IMPORTE = 0 AND USUARIO_CREO IN
('migracion caso 1','migracion caso 2','migracion caso 3',
 'migracion caso 4','migracion caso 5','migracion caso 6','migracion caso 7')
 
----------- PARA ELIMINAR LOS CEROS EN RECIBOS_COBROS ---------------

-- Agremos campo que sirve de bandera
alter table Ope_Cor_Caj_Recibos_Cobros add borrar varchar(2) null

--- Pasos para eliminar del caso 1
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 1' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 1' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 1')))
    ------ Resultado (16562 row(s) affected)

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 

    DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    -- (16560 row(s) affected) 7 seg



--- Pasos para eliminar del caso 2
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 2' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 2' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 2')))
    ------ Resultado (3225 row(s) affected) 5 seg

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 

    DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    -- (3225 row(s) affected)



--- Pasos para eliminar del caso 3
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 3' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 3' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 3')))
    ------ Resultado (77968 row(s) affected) 1 seg

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    -- Resultado (31 row(s) affected) 1 seg
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    -- (31 row(s) affected) 1 seg
    
    select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    
    DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    -- (77937 row(s) affected) 8 seg



--- El caso 4 no existe 

--- Pasos para eliminar del caso 5
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 5' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 5' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 5')))
    ------ Resultado (50 row(s) affected) 1 seg

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    
select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    
DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    -- (50 row(s) affected) 3 seg

--- Pasos para eliminar del caso 6
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 6' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 6' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 6')))
    ------ Resultado (1452 row(s) affected) 1 seg

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    
    
    select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    
    DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    -- (1452 row(s) affected) 3 seg

------------------------------------------------------------------------------------------------
--===========================================================================================
-- El caso 7 no existe  (EXPERIMENTAL)

--- Pasos para eliminar del caso 7
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 7' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 7' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 7')))
    ------ Resultado (50 row(s) affected) 1 seg

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    
select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    
DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 

--===========================================================================================
------------------------------------------------------------------------------------------------

--- Pasos para eliminar del caso 8
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'SI' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros where TOTAL_RECIBOS =0 AND USUARIO_CREO = 'migracion caso 4' AND NO_RECIBO NOT IN
    (SELECT no_recibo from Ope_Cor_Caj_Recibos_Cobros WHERE usuario_creo='migracion caso 4' AND NO_RECIBO in 
        (select no_recibo from Ope_Cor_Caj_Movimientos_Cobros where usuario_creo='migracion caso 4')))
    ------ Resultado (1541 row(s) affected) 1 seg

    --Verificamos que no haya relación con los movimientos cobro
    SELECT NO_RECIBO,(select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) 
    from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL
    
    
    --Cambiamos la bandera, para solo dejar los SI y sean los que se eliminen
    UPDATE Ope_Cor_Caj_Recibos_Cobros SET borrar = 'NO' WHERE NO_RECIBO IN
    (SELECT NO_RECIBO from Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si'
    AND  (select top 1 no_recibo from Ope_Cor_Caj_Movimientos_Cobros where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) is NOT NULL)
    
    
    select count(*) FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    
    DELETE FROM Ope_Cor_Caj_Recibos_Cobros WHERE borrar = 'si' 
    -- (3213 row(s) affected) 3 seg


----------------------------
----------------------------
----------------------------
----------------------------

alter table ope_cor_facturacion_recibos_detalles add borrar varchar(2) null

USE [Simapag_20161015]
GO
ALTER INDEX [Idx_Importe_Concepto] ON [dbo].[Ope_Cor_Facturacion_Recibos_Detalles] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO


UPDATE ope_cor_facturacion_recibos_detalles set borrar ='SI' where Importe = 0 and Concepto_ID <> '00014' AND
(select top 1 No_Movimiento_Facturacion from Ope_Cor_Caj_Movimientos_Cobros where No_Movimiento_Facturacion=ope_cor_facturacion_recibos_detalles.no_movimiento) is NULL
-- (12571424 row(s) affected)

------- Aplica para caso  1 3 5 y 6
    -- Caso 1
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 1')
        and borrar = 'SI'
        -- 74687 2 seg

    delete FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 1')
        and borrar = 'SI'
        -- 74687 24 seg

    -- Caso 3
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 3')
        and borrar = 'SI'
        -- 81786 7 seg

    delete FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 3')
        and borrar = 'SI'
        -- (81786 row(s) affected) 1min 11 seg

    -- Caso 5
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 5')
        and borrar = 'SI'
        -- 269 1 seg

    delete FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 5')
        and borrar = 'SI'
        -- (269 row(s) affected) 1 seg
        
    -- Caso 6
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 6')
        and borrar = 'SI'
        -- 6255 1 seg

    delete FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 6')
        and borrar = 'SI'
        -- (6255 row(s) affected) 1 seg



    -- Caso 2    
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT top 1000000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')    
         and borrar = 'SI'
    -- 2 538 683   2seg
    
    DELETE FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT top 1000000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')    
        AND borrar = 'SI' 
    -- (2538683 row(s) affected) 5min 55 seg
    

    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT top 2000000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')    
         and borrar = 'SI'
    -- 2606621 4 seg
    DELETE FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT top 2000000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')        
        AND borrar = 'SI' 
    -- (2606621 row(s) affected) 6min 15 seg

    select count(*) FROM Ope_Cor_Facturacion_Recibos_Detalles  
        WHERE No_Factura_Recibo IN (SELECT top 3000000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')            
        AND borrar = 'SI' 
    -- 2631192 8 seg
    DELETE FROM Ope_Cor_Facturacion_Recibos_Detalles  
        WHERE No_Factura_Recibo IN (SELECT top 3000000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')            
        AND borrar = 'SI' 
    -- (2631192 row(s) affected) 6min 27
        
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')        
        AND borrar = 'SI' 
    -- 1 789 372 4 seg
                
    DELETE FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 2')        
        AND borrar = 'SI' 
 -- (1789372 row(s) affected)    4min 20 seg    
        

-- Caso 8
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT top 500000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 4')    
        AND borrar = 'SI'     
    -- 1008268 2 seg
    DELETE FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT top 500000 no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 4')    
        AND borrar = 'SI' 
    -- (1008268 row(s) affected) 2 min 58 seg
    
    select count(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT  no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 4')    
        AND borrar = 'SI' 
    -- 1834291   3seg
    DELETE FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT  no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 4')    
        AND borrar = 'SI' 
    -- (1834291 row(s) affected) 5min 26 seg

    -- Caso 7
    select COUNT(*) FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 7')
        and borrar = 'SI'
        -- 7639600 9 seg

    delete FROM Ope_Cor_Facturacion_Recibos_Detalles 
        WHERE No_Factura_Recibo IN (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo = 'migracion caso 7')
        and borrar = 'SI'
        -- (6255 row(s) affected) 1 seg

--------------------------



------ Aqui comienza el plan malvado de borrar los registros con diferencia en saldos (ñaca ñaca...)
----Se consulta y se eliminan los movimientos cobros
USE [Simapag_20161015]
GO
ALTER INDEX [Index_RPU] ON [dbo].[Ope_Cor_Caj_Movimientos_Cobros] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO

USE [Simapag_20161015]
GO
ALTER INDEX [Idx_Usuario_Creo] ON [dbo].[Ope_Cor_Facturacion_Recibos] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO

select count(*) FROM Ope_Cor_Caj_Movimientos_Cobros WHERE RPU IN
(SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo='Migracion caso 6')
 and USUARIO_CREO IN 
    ('migracion caso 1','migracion caso 3','migracion caso 5')
-- 50337 1 seg
delete FROM Ope_Cor_Caj_Movimientos_Cobros WHERE RPU IN
(SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo='Migracion caso 6')
AND USUARIO_CREO IN 
    ('migracion caso 1','migracion caso 3','migracion caso 5')
-- (50337 row(s) affected) 28 seg


-------------- Script que faltan

---Para eliminar de recibos cobros
USE [Simapag_20161015]
GO
ALTER INDEX [Idx_Ope_Cor_Caj_Recibos_Cobro_RPU] ON [dbo].[Ope_Cor_Caj_Recibos_Cobros] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO

USE [Simapag_20161015]
GO
ALTER INDEX [Idx_NO_RECIBO] ON [dbo].[Ope_Cor_Caj_Movimientos_Cobros] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO


    SELECT count(*) FROM Ope_Cor_Caj_Recibos_Cobros 
        WHERE RPU IN(SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos 
                                    WHERE Usuario_Creo='Migracion caso 6')
        AND USUARIO_CREO IN ('migracion caso 1','MIGRACION CASO 3','migracion caso 5')
        AND  (select top 1 NO_RECIBO from Ope_Cor_Caj_Movimientos_Cobros 
                                    where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) IS NULL
-- 10530  1 seg
SELECT count(*) from Ope_Cor_Caj_Recibos_Cobros where borrar = 'SI'

UPDATE Ope_Cor_Caj_Recibos_Cobros set borrar = 'SI'
    WHERE RPU IN(SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos 
                                WHERE Usuario_Creo='Migracion caso 6')
    AND USUARIO_CREO IN ('migracion caso 1','MIGRACION CASO 3','migracion caso 5')
    AND  (select top 1 NO_RECIBO from Ope_Cor_Caj_Movimientos_Cobros 
                                where NO_RECIBO=Ope_Cor_Caj_Recibos_Cobros.NO_RECIBO) IS NULL

delete from Ope_Cor_Caj_Recibos_Cobros where borrar = 'SI'




-- Elimina Recibos con saldo mal en recibos_detalles
UPDATE Ope_Cor_Facturacion_Recibos_Detalles SET borrar = 'SI' WHERE No_Factura_Recibo IN
    (SELECT no_factura_recibo FROM Ope_Cor_Facturacion_Recibos WHERE USUARIO_CREO IN 
        ('migracion caso 1','migracion caso 3','migracion caso 5') AND RPU IN
    (SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo='Migracion caso 6')
        AND  (select top 1 No_Movimiento_Facturacion from Ope_Cor_Caj_Movimientos_Cobros 
            where No_Movimiento_Facturacion=Ope_Cor_Facturacion_Recibos_Detalles.No_Movimiento) IS NULL)

delete from Ope_Cor_Facturacion_Recibos_Detalles where borrar = 'SI'

-- Elimina Recibos con saldo mal en recibos
USE [Simapag_20161015]
GO
ALTER INDEX [Idx_Usuario_Creo] ON [dbo].[Ope_Cor_Facturacion_Recibos] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO
USE [Simapag_20161015]
GO
ALTER INDEX [Rpu] ON [dbo].[Ope_Cor_Facturacion_Recibos] REBUILD PARTITION = ALL WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, ONLINE = OFF, SORT_IN_TEMPDB = OFF )
GO
-- 25 seg

SELECT count(*) FROM Ope_Cor_Facturacion_Recibos WHERE USUARIO_CREO IN 
        ('migracion caso 1','migracion caso 3','migracion caso 5') AND RPU IN
    (SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo='Migracion caso 6')
-- 38324

delete FROM Ope_Cor_Facturacion_Recibos WHERE USUARIO_CREO IN 
        ('migracion caso 1','migracion caso 3','migracion caso 5') AND RPU IN
    (SELECT DISTINCT rpu FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo='Migracion caso 6')
-- (38324 row(s) affected) 16 seg

-- Eliminar pagos adelantados en ceros
select COUNT(*) FROM Ope_Cor_Pagos_Adelantados WHERE Importe = 0
-- 6204759 1 seg
DELETE FROM Ope_Cor_Pagos_Adelantados WHERE Importe = 0
-- 6204759 18 seg

--Comprobación de pagos duplicados
SELECT COUNT(Rpu) from Ope_Cor_Pagos_Adelantados GROUP BY Rpu HAVING COUNT(Rpu) > 1