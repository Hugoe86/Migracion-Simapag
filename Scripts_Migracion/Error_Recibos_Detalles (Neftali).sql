--SELECT * FROM Ope_Cor_Facturacion_Recibos_Detalles where No_Factura_Recibo IN
--(SELECT No_Factura_Recibo from Ope_Cor_Facturacion_Recibos where rpu = '000951050326') --ejemplo

UPDATE Ope_Cor_Facturacion_Recibos_Detalles SET Total_Saldo = Total - Total_Abonado --WHERE No_Factura_Recibo IN
--(SELECT No_Factura_Recibo from Ope_Cor_Facturacion_Recibos where rpu = '000951050326')

UPDATE Ope_Cor_Facturacion_Recibos_Detalles SET Estatus = 'PAGADO' WHERE Total_Saldo BETWEEN 0 AND 0.1 AND Estatus = 'PENDIENTE' --AND No_Factura_Recibo IN
--(SELECT No_Factura_Recibo from Ope_Cor_Facturacion_Recibos where rpu = '000951050326')

UPDATE Ope_Cor_Facturacion_Recibos SET Estatus_Recibo = 'PAGADO' WHERE Saldo BETWEEN 0 AND 0.1 AND Estatus_Recibo = 'PENDIENTE' AND Usuario_Creo<>'PHANTOM'

--SELECT * FROM Ope_Cor_Facturacion_Recibos WHERE RPU ='000951050326'
--SELECT * FROM Ope_Cor_Facturacion_Recibos WHERE Usuario_Creo='phantom'

SELECT * FROM Cat_Cor_Medidores WHERE MARCA_ID IS NULL

INSERT INTO CAT_COM_MARCAS 
SELECT '00000','SIN MARCA','ACTIVO',NULL,'MIGRACION',GETDATE(),NULL,NULL

UPDATE Cat_Cor_Medidores SET MARCA_ID = '00000' WHERE MARCA_ID IS NULL
