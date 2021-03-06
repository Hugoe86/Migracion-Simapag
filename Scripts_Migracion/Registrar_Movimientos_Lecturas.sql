

/****** Object:  Trigger [dbo].[Registrar_Movimientos_Lecturas]    Script Date: 09/10/2016 15:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create TRIGGER [dbo].[Registrar_Movimientos_Lecturas]
ON [dbo].[Cat_Cor_Medidores_Lecturas_Detalles]
AFTER UPDATE
AS 
BEGIN
	declare @Fecha_Modifico datetime
	declare @Valor_Anterior numeric(18,2)
	declare @Valor_Nuevo numeric(18,2)
	declare @No_Cuenta numeric(18,0)
	declare @Medidor_Detalle_ID varchar(10)
	declare @Usuario_Creo varchar(100)
	declare @Comentarios varchar(255)
	declare @Predio_ID char(10)
	
	if update(Consumo_Facturacion) 
	begin
		select @No_Cuenta=No_Cuenta,@Medidor_Detalle_ID=Medidor_Detalle_ID, 
		@Valor_Anterior=Consumo_Facturacion, @Comentarios=Comentario_Modifico 
		from deleted
	
		select @Fecha_Modifico= Fecha_Modifico,@Valor_Nuevo = Consumo_Facturacion,
		@Usuario_Creo=Usuario_Modifico,@Predio_ID=Predio_ID 
		from inserted
	
		IF @Valor_Anterior <> @Valor_Nuevo
		begin
			INSERT INTO [dbo].[Rpt_Bitacora_Movimientos_Lecturas]
			   ([Fecha_Modifico]
			   ,[Valor_Anterior]
			   ,[Valor_Nuevo]
			   ,[No_Cuenta]
			   ,[Medidor_Detalle_ID]
			   ,[Nombre_Campo]
			   ,[Usuario_Creo]
			   ,[Fecha_Creo]
			   ,[Comentario_Modifico]
			   ,[Predio_ID])
			VALUES
			   (@Fecha_Modifico
				,@Valor_Anterior
			   ,@Valor_Nuevo
			   ,@No_Cuenta
			   ,@Medidor_Detalle_ID
			   ,'Consumo'
			   ,@Usuario_Creo
			   ,GETDATE()
			   ,@Comentarios
			   ,@Predio_ID)
		  end
	end
	
	if update(Lectura_Anterior) 
	begin
		select @No_Cuenta=No_Cuenta,@Medidor_Detalle_ID=Medidor_Detalle_ID,
		@Valor_Anterior=Lectura_Anterior,@Comentarios=Comentario_Modifico  
		from deleted
	
		select @Fecha_Modifico= Fecha_Modifico,@Valor_Nuevo = Lectura_Anterior,
		@Usuario_Creo=Usuario_Modifico,@Predio_ID=Predio_ID 
		from inserted
	
		IF @Valor_Anterior <> @Valor_Nuevo
		begin
			INSERT INTO [dbo].[Rpt_Bitacora_Movimientos_Lecturas]
			   ([Fecha_Modifico]
			   ,[Valor_Anterior]
			   ,[Valor_Nuevo]
			   ,[No_Cuenta]
			   ,[Medidor_Detalle_ID]
			   ,[Nombre_Campo]
			   ,[Usuario_Creo]
			   ,[Fecha_Creo]
			   ,[Comentario_Modifico]
			   ,[Predio_ID])
			VALUES
			   (@Fecha_Modifico
				,@Valor_Anterior
			   ,@Valor_Nuevo
			   ,@No_Cuenta
			   ,@Medidor_Detalle_ID
			   ,'Lectura_Anterior'
			   ,@Usuario_Creo
			   ,GETDATE()
			   ,@Comentarios
			   ,@Predio_ID)
		   end
	end
	
	if update(Lectura_Facturacion) 
	begin
		select @No_Cuenta=No_Cuenta,@Medidor_Detalle_ID=Medidor_Detalle_ID,
		@Valor_Anterior=Lectura_Facturacion, @Comentarios = Comentario_Modifico
		from deleted
	
		select @Fecha_Modifico= Fecha_Modifico,@Valor_Nuevo = Lectura_Facturacion,
		@Usuario_Creo=Usuario_Modifico,@Predio_ID=Predio_ID 
		from inserted
		
		IF @Valor_Anterior <> @Valor_Nuevo
		begin
			INSERT INTO [dbo].[Rpt_Bitacora_Movimientos_Lecturas]
			   ([Fecha_Modifico]
			   ,[Valor_Anterior]
			   ,[Valor_Nuevo]
			   ,[No_Cuenta]
			   ,[Medidor_Detalle_ID]
			   ,[Nombre_Campo]
			   ,[Usuario_Creo]
			   ,[Fecha_Creo]
			   ,[Comentario_Modifico]
			   ,[Predio_ID])
			VALUES
			   (@Fecha_Modifico
				,@Valor_Anterior
			   ,@Valor_Nuevo
			   ,@No_Cuenta
			   ,@Medidor_Detalle_ID
			   ,'Lectura_Facturacion'
			   ,@Usuario_Creo
			   ,GETDATE()
			   ,@Comentarios
			   ,@Predio_ID)
		  end
	end
END