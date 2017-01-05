----------------------------------------------------------------------------------------------------------
----------------------------- SCRIPT PARA INSERTAR DETALLES DE SANCIONES ---------------------------------
----------------------------------------------------------------------------------------------------------

--NOTAS: Correr la aplicacion para migrar Sanciones (Casos 1 y 2)
-- Enseguida actualizar el folio e insertar detalles.
-- Si cambia el usuario creo corregirlo en este script tambien.

USE Simapag_20161015

UPDATE Ope_Cor_Sanciones SET Folio = REPLACE(Folio,'F','') WHERE Folio LIKE 'F%'
UPDATE Ope_Cor_Sanciones SET Folio = REPLACE(Folio,'MANIPULACION','')WHERE Folio LIKE '%MANIPULACION%'

INSERT INTO Ope_Cor_Detalles_Sanciones(Estatus,Fk_Sanciones,Fecha_Creo,Precio_Normal,Precio_Reincidir,Precio_Descuento,Reincidio,Concepto_Cobro_ID,
	IVA_Normal,IVA_Reincidir,IVA_Descuento,Total_Normal,Total_Reincidir,Total_Descuento,Detalles_Lista_Sanciones_ID)
	
	SELECT 
			'ACTIVO'
		AS Estatus,
		Sanciones_ID,
		Fecha_Creo,
		0,0,0,
		CASE
			WHEN Usuario_Creo = 'Rose no' THEN 'NO'
			ELSE 'SI'
		END
		AS Reincidio,
			Observaciones
		AS Concepto_cobro,
		0,0,0,
		0,0,0,
		CASE
			WHEN	RFC_S <> ''
			THEN	(SELECT	SBCA.Sanciones_ID
				 FROM	Cat_Cor_Lista_Sanciones
				 AS		SBCA
				 WHERE	SBCA.Fk_Conceptos_Cobro = Observaciones
				 AND	SBCA.Fk_Tarifas = (SELECT	SBCA.GIRO_ID
					   FROM		Cat_Cor_Giros
					   AS		SBCA
					   WHERE	SBCA.Clave = dbo.fCAMBIAR_TARIFA(RFC_S, 'G')))
			ELSE	0
		END
		AS Lista_Sanciones
	FROM Ope_Cor_Sanciones WHERE Usuario_Creo LIKE 'Rose%'
