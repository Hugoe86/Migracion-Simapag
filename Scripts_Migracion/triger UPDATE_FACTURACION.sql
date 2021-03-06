USE [SIMAPAG_20161015]
GO
/****** Object:  Trigger [dbo].[UPDATE_FACTURACION]    Script Date: 09/03/2016 14:11:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[UPDATE_FACTURACION]  ON  [dbo].[Ope_Cor_Facturacion_Recibos] 
   AFTER UPDATE
AS 

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	DECLARE @No_Factura char(10)
	DECLARE @No_Cuenta numeric(18, 0) 
	DECLARE @No_Recibo char(10) 
	DECLARE @Medidor_Detalle_ID char(10) 
	DECLARE @Region_ID char(5) 
	DECLARE @Sector_ID char(5)
	DECLARE @Predio_ID char(10) 
	DECLARE @Usuario_ID char(10) 
	DECLARE @Medidor_ID char(10) 
	DECLARE @Tarifa_ID char(5) 
	DECLARE @No_Convenio char(10) 
	DECLARE @Codigo_Barras varchar(23) 
	DECLARE @Lectura_Anterior numeric(18, 0) 
	DECLARE @Lectura_Actual numeric(18, 0) 
	DECLARE @Consumo numeric(18, 0) 
	DECLARE @Cuota_Base numeric(18, 2) 
	DECLARE @Cuota_Consumo numeric(18, 2) 
	DECLARE @Precio_Metro_Cubico numeric(18, 2) 
	DECLARE @Fecha_Inicio_Periodo datetime 
	DECLARE @Fecha_Termino_Periodo datetime 
	DECLARE @Fecha_Limite_Pago datetime 
	DECLARE @Fecha_Emision datetime 
	DECLARE @Periodo_Facturacion varchar(50) 
	DECLARE @Tasa_IVA numeric(18, 2) 
	DECLARE @Total_Importe numeric(18, 2) 
	DECLARE @Total_IVA numeric(18, 2) 
	DECLARE @Total_Pagar numeric(18, 2) 
	DECLARE @Total_Abono numeric(18, 2) 
	DECLARE @Saldo numeric(18, 2) 
	DECLARE @Estatus_Recibo varchar(20) 
	DECLARE @Estatus_Impresion char(2) 
	DECLARE @Comentarios varchar(255) 
	DECLARE @Usuario_Creo varchar(100) 
	DECLARE @Fecha_Creo datetime 
	DECLARE @Usuario_Modifico varchar(100) 
	DECLARE @Fecha_Modifico datetime 
	DECLARE @Tipo_Recibo varchar(50) 
	DECLARE @Anio int 
	DECLARE @Bimestre int 
	DECLARE @Ruta_Reparto_ID char(5) 
	DECLARE @Referencia_Bancaria varchar(50) 
	DECLARE @RPU varchar(20) 
	DECLARE @Estimado CHAR(2)
	DECLARE @Facturacion_Historico_Id INT
	DECLARE @Contador INT

SELECT @Contador = COUNT(*) FROM DELETED 

IF @Contador > 0

	begin 

	if  UPDATE(Estimado)
		BEGIN
			
		   ----------------------------------------------------------
			select @No_Factura = deleted.No_Factura_Recibo
			from deleted 
		   
		   --------------------------------------------------------
			select @No_Cuenta = deleted.No_Cuenta
			from deleted
			
			--------------------------------------------------------
			select @No_Recibo = deleted.No_Recibo
			from deleted
			
			--------------------------------------------------------
			select @Medidor_Detalle_ID = deleted.Medidor_Detalle_ID
			from deleted
			--------------------------------------------------------
			select @Region_ID = deleted.Region_ID
			from deleted
			
			--------------------------------------------------------
			select @Sector_ID = deleted.Sector_ID
			from deleted
			
			--------------------------------------------------------
			select @Predio_ID = deleted.Predio_ID
			from deleted
			
			--------------------------------------------------------
			select @Usuario_ID = deleted.Usuario_ID
			from deleted
			
			--------------------------------------------------------
			select @Medidor_ID = deleted.Medidor_ID
			from deleted
			
			--------------------------------------------------------
			select @Tarifa_ID = deleted.Tarifa_ID
			from deleted
			
			--------------------------------------------------------
			select @No_Convenio = deleted.No_Convenio
			from deleted
			
			--------------------------------------------------------
			select @Codigo_Barras = deleted.Codigo_Barras
			from deleted
			
			--------------------------------------------------------
			select @Lectura_Anterior = deleted.Lectura_Anterior
			from deleted

			--------------------------------------------------------
			select @Lectura_Actual = deleted.Lectura_Actual
			from deleted
			
			--------------------------------------------------------
			select @Consumo = deleted.Consumo
			from deleted
		   
			--------------------------------------------------------
			select @Cuota_Base = deleted.Cuota_Base
			from deleted
		   
			--------------------------------------------------------
			select @Cuota_Consumo = deleted.Cuota_Consumo
			from deleted
		   
			--------------------------------------------------------
			select @Precio_Metro_Cubico = deleted.Precio_Metro_Cubico
			from deleted
			
			--------------------------------------------------------
			select @Fecha_Inicio_Periodo = deleted.Fecha_Inicio_Periodo
			from deleted
		   
			--------------------------------------------------------
			select @Fecha_Termino_Periodo = deleted.Fecha_Termino_Periodo
			from deleted
			
			--------------------------------------------------------
			select @Fecha_Limite_Pago  = deleted.Fecha_Limite_Pago 
			from deleted
		   
			--------------------------------------------------------
			select  @Fecha_Emision  = deleted.Fecha_Emision 
			from deleted
			
			--------------------------------------------------------
			select  @Periodo_Facturacion  = deleted.Periodo_Facturacion 
			from deleted
			
			--------------------------------------------------------
			select  @Tasa_IVA  = deleted.Tasa_IVA 
			from deleted
			
			--------------------------------------------------------
			select  @Total_Importe  = deleted.Total_Importe 
			from deleted
		         
			--------------------------------------------------------
			select  @Total_IVA  = deleted.Total_IVA 
			from deleted
			
			--------------------------------------------------------
			select  @Total_Pagar  = deleted.Total_Pagar 
			from deleted   
		   
			--------------------------------------------------------
			select  @Total_Abono  = deleted.Total_Abono 
			from deleted   
		   
			--------------------------------------------------------
			select  @Saldo  = deleted.Saldo 
			from deleted   
		   
			--------------------------------------------------------
			select  @Estatus_Recibo  = deleted.Estatus_Recibo 
			from deleted   
			
			--------------------------------------------------------
			select  @Estatus_Impresion  = deleted.Estatus_Impresion 
			from deleted   
			
			--------------------------------------------------------
			select  @Comentarios  = deleted.Comentarios 
			from deleted   
			
			--------------------------------------------------------
			select  @Usuario_Creo  = deleted.Usuario_Creo 
			from deleted   
			
			--------------------------------------------------------
			select  @Fecha_Creo  = deleted.Fecha_Creo 
			from deleted   
			
			--------------------------------------------------------
			select  @Usuario_Modifico  = deleted.Usuario_Modifico 
			from deleted   
			
			--------------------------------------------------------
			select  @Fecha_Modifico  = deleted.Fecha_Modifico 
			from deleted   
			
			--------------------------------------------------------
			select  @Tipo_Recibo  = deleted.Tipo_Recibo 
			from deleted   
			
			--------------------------------------------------------
			select  @Anio  = deleted.Anio 
			from deleted   
			
			--------------------------------------------------------
			select  @Bimestre  = deleted.Bimestre 
			from deleted   
		   
			--------------------------------------------------------
			select  @Ruta_Reparto_ID  = deleted.Ruta_Reparto_ID 
			from deleted   
		   
			--------------------------------------------------------
			select  @Referencia_Bancaria  = deleted.Referencia_Bancaria 
			from deleted   
		   
			--------------------------------------------------------
			select  @RPU  = deleted.RPU 
			from deleted   
			
			--------------------------------------------------------
			select @Estimado = inserted.Estimado
			from inserted
			
			if @Estimado = 'SI'
			begin				
				--------------------------------------------------------
				--------------------------------------------------------
				--------------------------------------------------------
				insert into Ope_Cor_Facturacion_Recibos_Historicos
					values (@No_Factura
						, @No_Cuenta
						, @No_Recibo
						, @Medidor_Detalle_ID
						, @Region_ID
						, @Sector_ID
						, @Predio_ID
						, @Usuario_ID
						, @Medidor_ID
						, @Tarifa_ID
						, @No_Convenio
						, @Codigo_Barras
						, @Lectura_Anterior
						, @Lectura_Actual
						, @Consumo
						, @Cuota_Base
						, @Cuota_Consumo
						, @Precio_Metro_Cubico
						, @Fecha_Inicio_Periodo
						, @Fecha_Termino_Periodo
						, @Fecha_Limite_Pago 
						, @Fecha_Emision
						, @Periodo_Facturacion
						, @Tasa_IVA
						, @Total_Importe
						, @Total_IVA
						, @Total_Pagar
						, @Total_Abono
						, @Saldo
						, @Estatus_Recibo
						, @Estatus_Impresion
						, @Comentarios
						, @Usuario_Creo
						, @Fecha_Creo
						, @Usuario_Modifico
						, @Fecha_Modifico
						, @Tipo_Recibo
						, @Anio
						, @Bimestre
						, @Ruta_Reparto_ID
						, @Referencia_Bancaria
						, @RPU	
						,GETDATE()		
						)
						
					SELECT @Facturacion_Historico_Id = Facturacion_Historico_ID 
					FROM Ope_Cor_Facturacion_Recibos_Historicos
					
					insert into Ope_Cor_Facturacion_Recibos_Detalles_Historicos 
					(Facturacion_Historico_ID, 
					 No_Factura_Recibo,
					 Concepto_ID,
					 No_Movimiento,
					 Concepto,
					 Importe,
					 Impuesto,
					 Total,
					 Importe_Abonado,
					 Impuesto_Abonado,
					 Total_Abonado,
					 Importe_Saldo,
					 Impuesto_Saldo,
					 Total_Saldo,
					 Anio,
					 Bimestre,
					 Estatus,
					 Fecha_Ajuste)
					select @Facturacion_Historico_Id, 
					 No_Factura_Recibo,
					 Concepto_ID,
					 No_Movimiento,
					 Concepto,
					 Importe,
					 Impuesto,
					 Total,
					 Importe_Abonado,
					 Impuesto_Abonado,
					 Total_Saldo,
					 Importe_Saldo,
					 Impuesto_Saldo,
					 Total_Saldo,
					 Anio,
					 Bimestre,
					 Estatus,
					 GETDATE()
					from Ope_Cor_Facturacion_Recibos_Detalles
					where No_Factura_Recibo = @No_Factura
					
					
				end
		END
	END
END

