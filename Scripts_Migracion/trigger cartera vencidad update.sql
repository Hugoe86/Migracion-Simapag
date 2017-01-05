-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create TRIGGER UPDATE_Cartera_Vencida  ON  Ope_Cor_Cc_Cartera_Vencidad_Historico 
   for update, insert
AS 
   
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	declare @ID DOUBLE PRECISION
	declare @Enero DOUBLE PRECISION
	declare @Febrero DOUBLE PRECISION
	declare @Marzo DOUBLE PRECISION
	declare @Abril DOUBLE PRECISION
	declare @Mayo DOUBLE PRECISION
	declare @Junio DOUBLE PRECISION
	declare @Julio DOUBLE PRECISION
	declare @Agosto DOUBLE PRECISION
	declare @Septiembre DOUBLE PRECISION
	declare @Octubre DOUBLE PRECISION
	declare @Noviembre DOUBLE PRECISION
	declare @Diciembre DOUBLE PRECISION

	declare @Enero_Cantidad DOUBLE PRECISION
	declare @Febrero_Cantidad DOUBLE PRECISION
	declare @Marzo_Cantidad DOUBLE PRECISION
	declare @Abril_Cantidad DOUBLE PRECISION
	declare @Mayo_Cantidad DOUBLE PRECISION
	declare @Junio_Cantidad DOUBLE PRECISION
	declare @Julio_Cantidad DOUBLE PRECISION
	declare @Agosto_Cantidad DOUBLE PRECISION
	declare @Septiembre_Cantidad DOUBLE PRECISION
	declare @Octubre_Cantidad DOUBLE PRECISION
	declare @Noviembre_Cantidad DOUBLE PRECISION
	declare @Diciembre_Cantidad DOUBLE PRECISION


	----------------------------------------------------------
	select @ID = inserted.Id
	from INSERTed 


	----------------------------------------------------------
	select @Enero = inserted.enero
	from INSERTed 
	
	----------------------------------------------------------
	select @Febrero = inserted.febrero
	from INSERTed 
	
	----------------------------------------------------------
	select @Marzo = inserted.marzo
	from INSERTed 
	
	----------------------------------------------------------
	select @Abril = inserted.abril
	from INSERTed 
	
	----------------------------------------------------------
	select @Mayo = inserted.mayo
	from INSERTed 
	
	----------------------------------------------------------
	select @Junio = inserted.junio
	from INSERTed 
	
	----------------------------------------------------------
	select @Julio = inserted.julio
	from INSERTed 
	
	----------------------------------------------------------
	select @Agosto  = inserted.agosto
	from INSERTed 
	
	----------------------------------------------------------
	select @Septiembre = inserted.septiembre
	from INSERTed 
	
	----------------------------------------------------------
	select @Octubre = inserted.octubre
	from INSERTed 
	
	----------------------------------------------------------
	select @Noviembre = inserted.noviembre
	from INSERTed 
		   
	
	----------------------------------------------------------
	select @Diciembre = inserted.diciembre
	from INSERTed 
		   
----------------------------------------------------------
	select @Enero_Cantidad = inserted.enero_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Febrero_Cantidad = inserted.febrero_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Marzo_Cantidad = inserted.marzo_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Abril_Cantidad = inserted.abril_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Mayo_Cantidad = inserted.mayo_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Junio_Cantidad = inserted.junio_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Julio_Cantidad = inserted.julio_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Agosto_Cantidad  = inserted.agosto_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Septiembre_Cantidad = inserted.septiembre_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Octubre_Cantidad = inserted.octubre_Cuentas
	from INSERTed 
	
	----------------------------------------------------------
	select @Noviembre_Cantidad = inserted.noviembre_Cuentas
	from INSERTed 
		   
	
	----------------------------------------------------------
	select @Diciembre_Cantidad = inserted.diciembre_Cuentas
	from INSERTed 
		   	
	
	update Ope_Cor_Cc_Cartera_Vencidad_Historico set
		Total_Importe  = (@Enero) + (@Febrero) + (@Marzo) + (@Abril)
					+ (@Mayo) + (@Junio) + (@Julio) + (@Agosto)
					+ (@Septiembre) + (@Octubre) + (@Noviembre) + (@Diciembre)
					
		, Total_cantidad  = (@Enero_Cantidad) + (@Febrero_Cantidad) + (@Marzo_Cantidad) + (@Abril_Cantidad)
					+ (@Mayo_Cantidad) + (@Junio_Cantidad) + (@Julio_Cantidad) + (@Agosto_Cantidad)
					+ (@Septiembre_Cantidad) + (@Octubre_Cantidad) + (@Noviembre_Cantidad) + (@Diciembre_Cantidad)
					
	where Id = @ID
	

END
GO
