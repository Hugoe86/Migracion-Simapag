--	NO SCRIPT AJEC:		17
--	REGISTROS MIGRADOS: 3297
--	TIEMPO DE EJEC:		00.00.00

INSERT INTO Simapag_20161015.dbo.Convenio_Desglosado
			(Convenio_Desglosado_ID, No_Convenio, Tipo_Convenio, 
			RPU, Total, Usuario_Creo, Fecha_Creo)
			
			SELECT		RIGHT('000000000'+ LTRIM(RTRIM(STR(ROW_NUMBER() OVER(ORDER BY No_Convenio )))), 10) 
					AS ID,
					C1.No_Convenio,
					C1.Tipo_Convenio,
					C1.RPU,
						--Total_Descuento
						(SELECT	 SBCA.Adeudo
						 FROM	Simapag_20161015.dbo.Ope_Cor_Convenios
						 AS		SBCA
						 WHERE	SBCA.No_Convenio = C1.No_Convenio)
					AS TOTAL,
						'MIGRACION'
					AS U_CREO,
						GETDATE()
					AS F_CREO
			FROM	Simapag_20161015.dbo.Ope_Cor_Convenios
			AS		C1
            