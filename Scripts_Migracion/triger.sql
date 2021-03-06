USE [Simapag_20161015]
GO
/****** Object:  Trigger [dbo].[DELETE_PAGOS]    Script Date: 09/03/2016 14:10:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create TRIGGER [dbo].[DELETE_PAGOS] ON [dbo].[Ope_Cor_Caj_Movimientos_Cobros]
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	--DECLARE @NO_MOVIMIENTO INT
	--DECLARE @NO_RECIBO INT
	--DECLARE @CONCEPTO_ID CHAR(5)
	--DECLARE @IMPORTE MONEY
	--DECLARE @FECHA_MOVIMIENTO DATETIME
	--DECLARE @Facturado CHAR(1)
	--DECLARE @COMENTARIOS VARCHAR(255)
	--DECLARE @USUARIO_CREO VARCHAR(100)
	--DECLARE @FECHA_CREO DATETIME
	--DECLARE @USUARIO_MODIFICO VARCHAR(100)
	--DECLARE @FECHA_MODIFICO DATETIME
	--DECLARE @estado_concepto_cobro VARCHAR(50)
	--DECLARE @impuesto NUMERIC(18,2)
	--DECLARE @No_Movimiento_Facturacion INT
	--DECLARE @total NUMERIC(18,2)
	--DECLARE @RPU CHAR(12)
	
	INSERT INTO Ope_Cor_Caj_Movimientos_Cobro_Historico
	(NO_MOVIMIENTO,
	NO_RECIBO,
	CONCEPTO_ID,
	IMPORTE,
	FECHA_MOVIMIENTO,
	Facturado,
	COMENTARIOS,
	USUARIO_CREO,
	FECHA_CREO,
	FECHA_MODIFICO,
	estado_concepto_cobro,
	impuesto,
	No_Movimiento_Facturacion,
	total,
	RPU)	
	SELECT DELETED.NO_MOVIMIENTO,
		DELETED.NO_RECIBO,
		DELETED.CONCEPTO_ID,
		DELETED.IMPORTE,
		DELETED.FECHA_MOVIMIENTO,
		DELETED.Facturado,
		DELETED.COMENTARIOS,
		DELETED.USUARIO_CREO,
		DELETED.FECHA_CREO,
		DELETED.FECHA_MODIFICO,
		DELETED.estado_concepto_cobro,
		DELETED.impuesto,
		DELETED.No_Movimiento_Facturacion,
		DELETED.total,
		DELETED.RPU
	FROM DELETED
	
END
