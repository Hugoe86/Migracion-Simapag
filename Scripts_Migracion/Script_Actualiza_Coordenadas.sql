-- ==============================================================
--					ACTUALIZAR COORDENADAS
-- ==============================================================

UPDATE Cat_Cor_Predios set Latitud = (SELECT latitud FROM Coordenadas$ where coordenadas$.RPU = Cat_cor_Predios.RPU)
UPDATE Cat_Cor_Predios set Longitud = (SELECT longitud FROM Coordenadas$ where coordenadas$.RPU = Cat_cor_Predios.RPU)


-- ==============================================================
--			AGREGAR CALLES Y ACTIVIDADES DESCONOCIDAS
-- ==============================================================

INSERT into CAT_COR_VIALIDADES
SELECT '00000','SIN VIALIDAD','SIN VIALIDAD','MIGRACION',GETDATE(),NULL,NULL

INSERT into Cat_Cor_Calles
SELECT '00000','00000','SIN CALLE','0000','MIGRACION',NULL,NULL,NULL,NULL

INSERT INTO Cat_Cor_Giros_Actividades
SELECT '0000000000','0000000008','00','SIN ACTIVIDAD','NA','MIGRACION',GETDATE(),NULL,NULL,NULL

--UPDATE Cat_Cor_Calles SET FECHA_CREO = GETDATE()


-- =============================================================
--					AGREGAR MEDIDOR GENERICO
-- =============================================================

INSERT INTO Cat_Cor_Medidores (MEDIDOR_ID, MARCA_ID, NO_MEDIDOR, DIAMETRO, USUARIO_CREO, FECHA_CREO, LIMITE_MEDIDOR, ESTATUS)
VALUES('0000000000', '00001', '00', '1/2', 'MIGRACION', GETDATE(), 99999, 'ACTIVO')

INSERT INTO
	Cat_Cor_Predios_Medidores (PREDIO_ID, MEDIDOR_ID, LECTURA_INICIAL, FECHA_INSTALACION, COLONIA_ID, CALLE_ID)
SELECT
	P.Predio_ID,
	'0000000000',
	0,
	CAST('1991-01-01' AS DATETIME),
	Col.COLONIA_ID,
	Cal.CALLE_ID
FROM
	Cat_Cor_Predios P
	LEFT JOIN Cat_Cor_Predios_Medidores PM ON p.Predio_ID = PM.PREDIO_ID
	JOIN Cat_Cor_Colonias Col ON Col.COLONIA_ID = P.Colonia_ID
	JOIN Cat_Cor_Calles Cal ON Cal.CALLE_ID = P.Calle_ID
WHERE
	PM.PREDIO_ID IS NULL


-- =========================================================
--		ACTUALIZAR CALLES, COLONIAS, ACTIVIDADES NULOS
-- =========================================================

UPDATE Cat_Cor_Predios SET Calle_ID = '00000' WHERE Calle_ID IS NULL
UPDATE Cat_Cor_Predios SET Colonia_ID = '00000' WHERE Colonia_ID IS NULL
UPDATE Cat_Cor_Predios SET Giro_Actividad_ID = '00000' WHERE Giro_Actividad_ID IS NULL


-- ==============================================================
--					ACTUALIZAR OTROS DATOS
-- ==============================================================

SELECT * FROM Cat_Cor_Usuarios where RPUM = '000880702254'
UPDATE Cat_Cor_Usuarios SET NOMBRE = 'I M S S' WHERE RPUM = '000880702254'
UPDATE Cat_Cor_Usuarios SET APELLIDO_PATERNO = '' WHERE RPUM = '000880702254'

SELECT * FROM CAT_COR_DOCUMENTOS_ACREDITACION
UPDATE CAT_COR_DOCUMENTOS_ACREDITACION SET DOCUMENTO_ACREDITACION_ID = '0000'+DOCUMENTO_ACREDITACION_ID

SELECT DISTINCT SUBSIDIO FROM Cat_Cor_Predios

UPDATE Cat_Cor_Predios SET SUBSIDIO = 'SI' WHERE SUBSIDIO IN ('2','3','5','6')